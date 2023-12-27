using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 6f;          //エネミーのスピード
    public EffectScripts _effectEnemy;  // 爆発エフェクトのプレハブ
    private System.Action _deadCallback;//死んだときに死んだことを伝える
    private GameObject SEgameObj;       //Unity上で作ったGameObjectである名前SEを入れる変数
    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得
    }
    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed        = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {
        //敵は左から来る
        transform.position += Vector3.left * (Time.deltaTime * _Speed);
        //画面外に出たらエネミー死ぬ
        if(transform.position.x < -Screen.width / _screenRatio)
        Destroy(gameObject);

    }

    //敵消滅
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //弾かプレイヤーがエネミーに当たったら
        if(collision.gameObject.tag.Equals("Bullet") || collision.gameObject.tag.Equals("Player"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();

            // 弾が当たった場所に爆発エフェクトを生成する
            Instantiate(
                _effectEnemy,
                collision.transform.localPosition,
                Quaternion.identity);

            _deadCallback?.Invoke();  //メモ：？は_deadCallbackがnullじゃないときに関数を呼び出す
                                      //敵消滅
            Destroy(gameObject);
        }

    }


}
