using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : BaseWeapon
{

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        attackEnemy(collision);
    }
}
