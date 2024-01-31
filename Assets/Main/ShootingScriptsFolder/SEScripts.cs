using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// "AudioSource"コンポーネントがアタッチされていない場合アタッチ
[RequireComponent(typeof(AudioSource))]
public class SEScripts : MonoBehaviour
{
    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;
    public AudioClip sound04;

    public AudioClip bulletClip;     //弾丸発射時
    public AudioClip damageClip;     //ダメージを受けた時
    public AudioClip itemdamageClip; //アイテムを取得した時
    public AudioClip playerClip;     //プレイヤーが攻撃された時

    AudioSource audioSource;

    public static SEScripts _seaudioSource;//どこでもスクリプトを呼び出すため

    private AudioSource SEaudioSource; //SE用
    void Start()
    {
        // "SEAudioSource"コンポーネントを取得
        SEaudioSource = gameObject.GetComponent<AudioSource>();

        //Componentを取得
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        
        //シーン移動してもデータ保存され、鳴り続ける
        DontDestroyOnLoad(this);
    }


    public void OnClickDiscSe1()
    {
        audioSource.PlayOneShot(sound01);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe2()
    {
        audioSource.PlayOneShot(sound02);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe3()
    {
        audioSource.PlayOneShot(sound03);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe4()
    {
        audioSource.PlayOneShot(sound04);
        //Debug.Log("click se1");
    }
    /// <summary>
    /// アイテム用SE
    /// </summary>
    public void ItemSE()
    {
        audioSource.PlayOneShot(itemdamageClip);
    }
    /// <summary>
    /// ダメージ用SE
    /// </summary>
    public void damageSE()
    {
        audioSource.PlayOneShot(damageClip);
    }
    /// <summary>
    /// 弾丸発射SE
    /// </summary>
    public void bulletSE()
    {
        audioSource.PlayOneShot(bulletClip);
    }
    /// <summary>
    /// プレイヤー攻撃時SE
    /// </summary>
    public void playerSE()
    {
        audioSource.PlayOneShot(playerClip);
    }

    public void SESoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        SEaudioSource.volume = newSliderValue;
    }

}