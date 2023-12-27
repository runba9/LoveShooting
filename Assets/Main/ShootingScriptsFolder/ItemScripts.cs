using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemScripts : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private float                      _Speed       = 6;      //速度
    private bool                       _isAttached;           //アイテムがくっついてるかどうか
    private int                        _hp          = 2;      //体力
    private float                      _angleSpeed  = 7f;     //回転速度
    private System.Action<GameObject>  _attachCallBack;       //プレイヤーとぶつかる判定とプレイヤーの手下になる
    private float                      _angle       = 0;      //今何度か
    private float                     Angle         => _angle;//外から拾える
    private readonly float _barrierLoseCount        = 1;      //ガードの数


    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    //初期化
    public void SetUp(float speed = 6)
    {
        _Speed = speed;
    }


    /// <summary>
    /// プレイヤーにアタッチする
    /// </summary>
    /// <param name="parent">プレイヤー</param>
    /// <param name="angle">初期角度</param>
    /// <param name="attachCallBack">接続のコールバック</param>
    public void Attach(Transform parent, float angle, System.Action<GameObject> attachCallBack)
    {
        _attachCallBack = attachCallBack;
        _angle = angle;
        _isAttached = true;
        transform.parent = parent;
        //アイテムにアニメーションを実行させる
        _animator.gameObject.SetActive(true);

    }

    //角度の再設定
    public void ResetAngle(float angle)
    {
        _angle = angle;
    }
    void Update()
    {
        if (_isAttached) 
        {
            //アイテムを取得し＋で右回転させる
            _angle += Time.deltaTime * _angleSpeed;
            if(_angle >= Mathf.PI * 2)_angle -= Mathf.PI * 2;
            transform.localPosition = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0);

        }

        else
        {
            //左から来る
            transform.position += Vector3.left * (Time.deltaTime * _Speed);
            if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);
        }

 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //アイテムを持っていてエネミーか壁に当たったら実行
        if(other.gameObject.tag.Equals("Enemy") && _isAttached || other.gameObject.tag.Equals("BulletEnemy") && _isAttached)
        {
            _hp--;
            //アイテムの体力が０になったらオブジェクト破壊
            if(_hp <= 0)
            {
                //HPが０になったら死ぬ
                _attachCallBack ? .Invoke(gameObject);
                Destroy(gameObject);
            }
            else if (_hp == _barrierLoseCount)
            {
                //バリアの耐久度に達したら1度だけ呼び出しバリアを非表示
                _animator.gameObject.SetActive(false);
            }
        }

    }
}
    