using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    // シングルトン
    public static SoundController Instance;

    // 再生装置
    AudioSource audioSource;
    // BGM
    [SerializeField] List<AudioClip> audioClipsBGM;
    // SE
    [SerializeField] List<AudioClip> audioClipsSE;

    private void Awake()
    {
        // もしなければセットする
        if (null == Instance)
        {
            // オーディオ設定
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;

            // オブジェクトをセットする 
            Instance = this;
            // シーンをまたいでもオブジェクトを削除しない
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // BGM再生
    public void PlayBGM(int index)
    {
        if (audioClipsBGM.Count-1 < index) return;

        audioSource.clip = audioClipsBGM[index];
        audioSource.Play();
    }
    // BGM停止
    public void StopBGM(int index)
    {
        if (audioClipsBGM.Count - 1 < index) return;

        audioSource.clip = audioClipsBGM[index];
        audioSource.Stop();
    }

    // SE再生
    public void PlaySE(int index)
    {
        if (audioClipsSE.Count - 1 < index) return;

        audioSource.PlayOneShot(audioClipsSE[index]);
    }

}
