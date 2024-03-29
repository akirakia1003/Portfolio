using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : BaseWeapon
{

    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵以外
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        attackEnemy(collision);
    }
}
