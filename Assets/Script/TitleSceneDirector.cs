using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneDirector : MonoBehaviour
{

    // �X�^�[�g�{�^��
    [SerializeField] Button buttonStart;
    // �L�����N�^�[ID
    public static int CharacterId;


    // Start is called before the first frame update
    void Start()
    {
        int idx = 0;

        // �L�����N�^�[�f�[�^
        CharacterId = idx;
        CharacterStats charStats = CharacterSettings.Instance.Get(CharacterId);

        // �����\�Ȉ�ڂ̃f�[�^��\��
        int weaponId = charStats.DefaultWeaponIds[0];
        WeaponSpawnerStats weaponStats = WeaponSpawnerSettings.Instance.Get(weaponId, 1);
                
        // �{�^����I����Ԃɂ���
        buttonStart.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // START�{�^��
    public void onClickStart()
    {
        // �Q�[���V�[����
        SceneManager.LoadScene("GameScene");

        // �{�^���N���b�N��
        SoundController.Instance.PlaySE(0);
    }
}
