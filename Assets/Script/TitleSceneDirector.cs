using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneDirector : MonoBehaviour
{

    // スタートボタン
    [SerializeField] Button buttonStart;
    // キャラクターID
    public static int CharacterId;


    // Start is called before the first frame update
    void Start()
    {
        int idx = 0;

        // キャラクターデータ
        CharacterId = idx;
        CharacterStats charStats = CharacterSettings.Instance.Get(CharacterId);

        // 装備可能な一つ目のデータを表示
        int weaponId = charStats.DefaultWeaponIds[0];
        WeaponSpawnerStats weaponStats = WeaponSpawnerSettings.Instance.Get(weaponId, 1);
                
        // ボタンを選択状態にする
        buttonStart.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // STARTボタン
    public void onClickStart()
    {
        // ゲームシーンへ
        SceneManager.LoadScene("GameScene");

        // ボタンクリック音
        SoundController.Instance.PlaySE(0);
    }
}
