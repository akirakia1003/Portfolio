using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpawnerController : BaseWeaponSpawner
{
    // Update is called once per frame
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;

        // ���̃^�C�}�[
        spawnTimer = Stats.GetRandomSpawnTimer();

        // �G�����Ȃ�
        if (1 > enemySpawner.GetEnemies().Count) return;

        // �\����ʃT�C�Y�̍��W
        Vector2 posMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 posMax = Camera.main.ViewportToWorldPoint(Vector2.one);


        for (int i = 0; i < (int)Stats.SpawnCount; i++)
        {
            // �����_���Ń^�[�Q�b�g������
            List<EnemyController> enemies = enemySpawner.GetEnemies();
            EnemyController target = GetTargetEenemy(enemies, posMin,posMax);

            // �ʒu����
            target.transform.position = target.transform.root.position + new Vector3(0, 0.75f, 0);

            createWeapon(target.transform.position);
        }
    }

    // ��ʓ��̓G�������_���ɑI��
    EnemyController GetTargetEenemy(List<EnemyController> enemies, Vector2 posMin, Vector2 posMax)
    {
        int rnd = Random.Range(0, enemies.Count);

        for (int i = 0; i < 100; i++)
        {
            // ��ʔ͈͓��̓G�Ȃ�擾
            if (posMin.x < enemies[rnd].transform.position.x && enemies[rnd].transform.position.x < posMax.x &&
                posMin.y < enemies[rnd].transform.position.y && enemies[rnd].transform.position.y < posMax.y)
            {
                return enemies[rnd];
            }
            // �����̍Ď擾
            rnd = Random.Range(0, enemies.Count);
        }

        // 100��Ō�����Ȃ���΂Ƃ肠�����Ԃ�
        return enemies[rnd];
    }
}
