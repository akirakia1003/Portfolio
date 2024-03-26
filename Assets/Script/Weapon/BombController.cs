using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BombController : BaseWeapon
{
    // ���
    enum State
    {
        Bomb,
        Explosion,
        DamageFloor,
        Destroy,
    }
    State state;

    // �A�j���[�^�[
    Animator animator;
    // �A�j���[�V�������̃^�C�}�[
    Dictionary<State, float> animationTimer;
    // �_���[�W�t���A�؍ݎ���
    float damageFloorCoolDownTime = 0.5f;
    // �G���̃^�C�}�[�i�_���[�W�t���A���j
    Dictionary<EnemyController, float> damageFloorTimer;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        animationTimer = new Dictionary<State, float>();
        damageFloorTimer = new Dictionary<EnemyController, float>();
        animator = GetComponent<Animator>();

        // ���e��
        animationTimer.Add(State.Bomb, Random.Range(0.5f, 1.5f));
        // ������
        animationTimer.Add(State.Explosion, 0.66f);
        // �_���[�W�t���A
        animationTimer.Add(State.DamageFloor, 30f);

        // �������
        state = State.Bomb;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�����Ŏ��̏�Ԃ�
        if (animationTimer.ContainsKey(state))
        {
            animationTimer[state] -= Time.deltaTime;
            if (0 > animationTimer[state])
            {
                changeState(++state);
            }
        }
    }

    // ���e�̏�Ԃ�ς���
    void changeState(State next)
    {
        // ����
        if (State.Explosion == next)
        {
            animator.SetTrigger("isExplosion");
            rigidbody2d.gravityScale = 0;
            rigidbody2d.velocity = Vector2.zero;
        }
        // �_���[�W�t���A�[
        else if (State.DamageFloor == next)
        {
            animator.SetTrigger("isDamageFloor");
            // �����ɓ����
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        // �I��
        else if (State.Destroy == next)
        {
            Destroy(gameObject);
        }

        // ��Ԃ̍X�V
        state = next;
    }

    // �Փˎ��̊֐�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        // ���e
        if (State.Bomb == state)
        {
            attackEnemy(collision);
            changeState(State.Explosion);
        }
        // ������
        else if (State.Explosion == state)
        {
            attackEnemy(collision);
        }
    }

    // �Փ˂��Ă����
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �_���[�W�t���A����Ȃ�
        if (State.DamageFloor != state) return;

        // �G�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        // �^�[�Q�b�g�̃^�C�}�[���Z�b�g
        damageFloorTimer.TryAdd(enemy, damageFloorCoolDownTime);
        // �G���Ƀ^�C�}�[������
        damageFloorTimer[enemy] -= Time.deltaTime;

        // ���Ԋu�Ń_���[�W
        if (0 > damageFloorTimer[enemy])
        {
            attackEnemy(collision, stats.Attack / 3);
            damageFloorTimer[enemy] = damageFloorCoolDownTime;
        }

    }
}