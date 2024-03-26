using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NormalShotSpawnerController : BaseWeaponSpawner
{
    // 一度の生成に時差をつける
    int onceSpawnCount;
    float onceSpawnTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        onceSpawnCount = (int)Stats.SpawnCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;

        // ランダムでターゲットを決定
        List<EnemyController> enemies = enemySpawner.GetEnemies();
        EnemyController target = GetTargetNearestEenemy(enemies);

        // 範囲内に敵がいない
        if (target == null) return;

        // 武器生成
        NormalShotController ctrl = (NormalShotController)createWeapon(transform.position);
        ctrl.Target = target;

        // 次の生成タイマー
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;

        // 1回の生成が終わったらリセット
        if (1 > onceSpawnCount)
        {
            spawnTimer = Stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)Stats.SpawnCount;
        }
    }

    // 最も近い敵を選択
    EnemyController GetTargetNearestEenemy(List<EnemyController> enemies)
    {
        // 最も近い敵格納用
        EnemyController nearestEnemy = null;
        // オートエイム範囲
        float minDis = Camera.main.orthographicSize * 2;

        foreach (EnemyController enemy in enemies)
        {
            // 存在しない or 死んでいればスキップ
            if (enemy == null || enemy.GetIsDead() == true) continue;

            // 敵との距離
            float dis = Vector3.Distance(transform.position, enemy.transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
