using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpawnerController : BaseWeaponSpawner
{
    // Update is called once per frame
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;

        // 次のタイマー
        spawnTimer = Stats.GetRandomSpawnTimer();

        // 敵がいない
        if (1 > enemySpawner.GetEnemies().Count) return;

        // 表示画面サイズの座標
        Vector2 posMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 posMax = Camera.main.ViewportToWorldPoint(Vector2.one);


        for (int i = 0; i < (int)Stats.SpawnCount; i++)
        {
            // ランダムでターゲットを決定
            List<EnemyController> enemies = enemySpawner.GetEnemies();
            EnemyController target = GetTargetEenemy(enemies, posMin,posMax);

            // 位置調整
            target.transform.position = target.transform.root.position + new Vector3(0, 0.75f, 0);

            createWeapon(target.transform.position);
        }
    }

    // 画面内の敵をランダムに選択
    EnemyController GetTargetEenemy(List<EnemyController> enemies, Vector2 posMin, Vector2 posMax)
    {
        int rnd = Random.Range(0, enemies.Count);

        for (int i = 0; i < 100; i++)
        {
            // 画面範囲内の敵なら取得
            if (posMin.x < enemies[rnd].transform.position.x && enemies[rnd].transform.position.x < posMax.x &&
                posMin.y < enemies[rnd].transform.position.y && enemies[rnd].transform.position.y < posMax.y)
            {
                return enemies[rnd];
            }
            // 乱数の再取得
            rnd = Random.Range(0, enemies.Count);
        }

        // 100回で見つからなければとりあえず返す
        return enemies[rnd];
    }
}
