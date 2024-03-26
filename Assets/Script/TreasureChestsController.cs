using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestsController : MonoBehaviour
{
    GameSceneDirector sceneDirector;

    // ������
    public void Init(GameSceneDirector sceneDirector)
    {
        this.sceneDirector = sceneDirector;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �v���C���[����Ȃ�
        if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

        sceneDirector.DispPanelTreasureChest();
        Destroy(gameObject);
    }
}
