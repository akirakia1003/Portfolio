using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    private AudioSource audioSource; //BGM�̉���

    void Start()
    {
        audioSource = GameObject.Find("Sound").GetComponent<AudioSource>();

        gameObject.GetComponent<Slider>().value = audioSource.volume;    //�X���C�_�[�Ɖ��ʂ𓯊�
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(SetVolume);   // �X���C�_�[�̒l���ύX�������̏�����o�^
    }


    //���ʂ�0.5�𒴂���ƍ����B�ȉ����ƐԎ��B
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
