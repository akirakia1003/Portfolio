using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterStats Stats;
    Animator animator;

    [SerializeField] GameSceneDirector sceneDirector;
    Rigidbody2D rigidbody2d;

    // ダメージ時点滅用
    [SerializeField] SpriteRenderer _renderer;
    private Material _material;
    //マテリアルの加算色パラメータのID
    private static readonly int PROPERTY_ADDITIVE_COLOR = Shader.PropertyToID("_AdditiveColor");

    // 攻撃のクールダウン
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;
    // 向き
    Vector2 forward;

    private void Awake()
    {
        // materialにアクセスして自動生成されるマテリアルを保持
        _material = _renderer.material;
    }

    // 状態
    enum State
    {
        Alive,
        Dead
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();

        moveEnemy();
    }

    // 初期化
    public void Init(GameSceneDirector sceneDirector, CharacterStats characterStats)
    {
        this.sceneDirector = sceneDirector;
        this.Stats = characterStats;

        rigidbody2d = GetComponent<Rigidbody2D>();

        // 進む方向
        PlayerController player = sceneDirector.Player;
        Vector2 dir = player.transform.position - transform.position;
        forward = dir;

        state = State.Alive;
    }

    // プレイヤーを追いかける
    void moveEnemy()
    {
        if (State.Alive != state) return;

        // 目的がプレイヤーなら進む方向を更新する
        if (MoveType.TargetPlayer == Stats.MoveType)
        {
            PlayerController player = sceneDirector.Player;
            Vector2 dir = player.transform.position - transform.position;
            forward = dir;

            if (forward.x > 0)  transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        // 移動
        rigidbody2d.position += forward.normalized * Stats.MoveSpeed * Time.deltaTime;

    }

    // 各種タイマー更新
    void updateTimer()
    {
        if (0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }

        // 生存時間が設定されていたらタイマー消化
        if (0 < Stats.AliveTime)
        {
            Stats.AliveTime -= Time.deltaTime;
            if (0 > Stats.AliveTime) setDead(false);
        }
    }

    // 敵が死んだ時に呼び出される
    void setDead(bool createXP = true)
    {
        if (State.Alive != state) return;

        // 物理挙動を停止
        rigidbody2d.simulated = false;

        // アニメーションを停止
        //transform.DOKill();

        animator.SetTrigger("isDeath");

        // 〇秒後に実行
        StartCoroutine(Utils.DelayCoroutine(0.5f, () =>
        {
            sceneDirector.updateTreasureChestSpawner(this);
            Destroy(gameObject);
        }));

        // 縦に潰れるアニメーション
        //transform.DOScaleY(0, 0.5f).OnComplete(() => Destroy(gameObject));

        // 経験値を作成
        if (createXP)
        {
            // 経験値生成
            sceneDirector.CreateXP(this);
        }

        state = State.Dead;
    }

    // 衝突した時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        attackPlayer(collision);
    }

    // 衝突している間
    private void OnCollisionStay2D(Collision2D collision)
    {
        attackPlayer(collision);
    }

    // 衝突が終わった時
    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    // プレイヤーへ攻撃する
    void attackPlayer(Collision2D collision)
    {
        // プレイヤー以外
        if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;
        // タイマー未消化
        if (0 < attackCoolDownTimer) return;
        // 非アクティブ
        if (State.Alive != state) return;

        player.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }

    // ダメージ
    public float Damage(float attack)
    {
        // 非アクティブ
        if (State.Alive != state) return 0;

        StartCoroutine(DelayDamageFlash(UnityEngine.Color.red, 0.1f));

        float damage = Mathf.Max(0, attack - Stats.Defense);
        Stats.HP -= damage;

        // ダメージ表示
        sceneDirector.DispDamage(gameObject, damage);

        // 消滅
        if(0>Stats.HP)
        {
            sceneDirector.AddDefeatedEnemy();
            setDead();
        }

        // 計算後のダメージを返す
        return damage;
    }

    // 被ダメージ時の点滅
    private IEnumerator DelayDamageFlash(UnityEngine.Color color, float sec)
    {
        // 指定色を加算（赤）
        _material.SetColor(PROPERTY_ADDITIVE_COLOR, color);

        // sec待つ
        yield return new WaitForSeconds(sec);

        // 加算色を元に戻す
        _material.SetColor(PROPERTY_ADDITIVE_COLOR, UnityEngine.Color.black);
    }

    // 死んでいるかチェック
    public bool GetIsDead()
    {
        if (state == State.Dead) return true;
        
        return false;
    }
}
