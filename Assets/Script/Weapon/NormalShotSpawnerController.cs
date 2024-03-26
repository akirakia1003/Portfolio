using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NormalShotSpawnerController : BaseWeaponSpawner
{
    // ��x�̐����Ɏ���������
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

        // �����_���Ń^�[�Q�b�g������
        List<EnemyController> enemies = enemySpawner.GetEnemies();
        EnemyController target = GetTargetNearestEenemy(enemies);

        // �͈͓��ɓG�����Ȃ�
        if (target == null) return;

        // ���퐶��
        NormalShotController ctrl = (NormalShotController)createWeapon(transform.position);
        ctrl.Target = target;

        // ���̐����^�C�}�[
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;

        // 1��̐������I������烊�Z�b�g
        if (1 > onceSpawnCount)
        {
            spawnTimer = Stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)Stats.SpawnCount;
        }
    }

    // �ł��߂��G��I��
    EnemyController GetTargetNearestEenemy(List<EnemyController> enemies)
    {
        // �ł��߂��G�i�[�p
        EnemyController nearestEnemy = null;
        // �I�[�g�G�C���͈�
        float minDis = Camera.main.orthographicSize * 2;

        foreach (EnemyController enemy in enemies)
        {
            // ���݂��Ȃ� or ����ł���΃X�L�b�v
            if (enemy == null || enemy.GetIsDead() == true) continue;

            // �G�Ƃ̋���
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
