using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShotController : BaseWeapon
{
    // �^�[�Q�b�g
    public EnemyController Target;

    // Start is called before the first frame update
    void Start()
    {
        // �i�s�����i�x�N�g���j
        Vector2 forward = Target.transform.position - transform.position;
        // �p�x�ɕϊ�����
        float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        // �p�x����
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        // �^�[�Q�b�g�����Ȃ�
        if (!Target)
        {
            Destroy(gameObject);
            return;
        }

        // �ړ�
        Vector2 forward = Target.transform.position - transform.position;
        rigidbody2d.position += forward.normalized * stats.MoveSpeed * Time.deltaTime;
    }

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        //// �ʏ�_���[�W
        //float attack = stats.Attack;

        // �^�[�Q�b�g�ƏՓ�
        if (Target == enemy)
        {
            Target = null;
        }
        //// �^�[�Q�b�g�ȊO�̓G�͎O���̈�̃_���[�W
        //else
        //{
        //    attack /= 3;
        //}

        attackEnemy(collision);

        // �폜
        //Destroy(gameObject);
    }
}
