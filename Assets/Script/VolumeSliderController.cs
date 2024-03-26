using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    private AudioSource audioSource; //BGMの音量

    void Start()
    {
        audioSource = GameObject.Find("Sound").GetComponent<AudioSource>();

        gameObject.GetComponent<Slider>().value = audioSource.volume;    //スライダーと音量を同期
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(SetVolume);   // スライダーの値が変更した時の処理を登録
    }


    //音量が0.5を超えると黒字。以下だと赤字。
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
