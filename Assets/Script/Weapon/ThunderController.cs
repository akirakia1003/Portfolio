using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : BaseWeapon
{

    // ƒgƒŠƒK[‚ªÕ“Ë‚µ‚½
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // “GˆÈŠO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        attackEnemy(collision);
    }
}
