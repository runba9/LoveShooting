using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvincibleScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 6f;          //エネミーのスピード
    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    void Update()
    {
        //敵は左から来る
        transform.position += Vector3.left * (Time.deltaTime * _Speed);
        //画面外に出たらエネミー死ぬ
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);

    }

    //プレイヤーに当たったら取得
    public void OnTriggerEnter2D(Collider2D other)
    {
        //エネミーに当たったら
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);//弾消滅
        }
    }
}
