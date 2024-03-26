using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    // �V���O���g��
    public static SoundController Instance;

    // �Đ����u
    AudioSource audioSource;
    // BGM
    [SerializeField] List<AudioClip> audioClipsBGM;
    // SE
    [SerializeField] List<AudioClip> audioClipsSE;

    private void Awake()
    {
        // �����Ȃ���΃Z�b�g����
        if (null == Instance)
        {
            // �I�[�f�B�I�ݒ�
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;

            // �I�u�W�F�N�g���Z�b�g���� 
            Instance = this;
            // �V�[�����܂����ł��I�u�W�F�N�g���폜���Ȃ�
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // BGM�Đ�
    public void PlayBGM(int index)
    {
        if (audioClipsBGM.Count-1 < index) return;

        audioSource.clip = audioClipsBGM[index];
        audioSource.Play();
    }
    // BGM��~
    public void StopBGM(int index)
    {
        if (audioClipsBGM.Count - 1 < index) return;

        audioSource.clip = audioClipsBGM[index];
        audioSource.Stop();
    }

    // SE�Đ�
    public void PlaySE(int index)
    {
        if (audioClipsSE.Count - 1 < index) return;

        audioSource.PlayOneShot(audioClipsSE[index]);
    }

}
