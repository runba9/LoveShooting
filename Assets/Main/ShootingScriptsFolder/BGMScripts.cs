using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// "AudioSource"コンポーネントがアタッチされていない場合アタッチ
[RequireComponent(typeof(AudioSource))]
public class BGMScripts : MonoBehaviour
{

    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;
    public AudioClip sound04;
    AudioSource audioSource;


    private AudioSource BGMaudioSource; //BGM用
    void Start()
    {
        // "BGMAudioSource"コンポーネントを取得
        BGMaudioSource = gameObject.GetComponent<AudioSource>();

        //Componentを取得
        audioSource = GetComponent<AudioSource>();

        //シーン移動してもデータ保存され、鳴り続ける
       // DontDestroyOnLoad(this);
    }


    public void OnClickDiscSe1()
    {
        audioSource.PlayOneShot(sound01);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe2()
    {
        audioSource.PlayOneShot(sound02);
        //Debug.Log("click se2");
    }
    public void OnClickDiscSe3()
    {
        audioSource.PlayOneShot(sound03);
        //Debug.Log("click se3");
    }

    public void OnClickDiscSe4()
    {
        audioSource.PlayOneShot(sound04);
        //Debug.Log("click se3");
    }

    public void BGMSoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        BGMaudioSource.volume = newSliderValue;
    }

}