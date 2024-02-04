using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// "AudioSource"コンポーネントがアタッチされていない場合アタッチ
[RequireComponent(typeof(AudioSource))]
public class SEScripts : MonoBehaviour
{
    public AudioClip YesButton;
    public AudioClip NoButton;

    public AudioClip bulletClip;     //弾丸発射時
    public AudioClip damageClip;     //ダメージを受けた時
    public AudioClip itemdamageClip; //アイテムを取得した時
    public AudioClip playerClip;     //プレイヤーが攻撃された時

    private AudioSource audioSource;  //SE用

    public static SEScripts _seaudioSource;//どこでもスクリプトを呼び出すため


    private void Awake()
    {
        // 同じ処理を2回しないようにする
        if (_seaudioSource == null)
        {
            // 変数の中にスクリプトを格納
            _seaudioSource = GetComponent<SEScripts>();
            _seaudioSource = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // "SEAudioSource"コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        _seaudioSource = GetComponent<SEScripts>();

    }

    private void Update()
    {

        //シーン移動してもデータ保存され、鳴り続ける
        DontDestroyOnLoad(this);
    }


    public void OnClickDiscSe1()
    {
        audioSource.PlayOneShot(YesButton);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe2()
    {
        audioSource.PlayOneShot(NoButton);
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
        audioSource.volume = newSliderValue;
    }

}