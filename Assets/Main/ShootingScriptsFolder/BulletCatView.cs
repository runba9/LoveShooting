using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCatView : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 1f;
    //[SerializeField]
    //public GameObject playerTrans;         // プレイヤーのTransform
    public EffectScripts _effectEnemy;  // 爆発エフェクトのプレハブ
    private System.Action _deadCallback;//死んだときに死んだことを伝える
    private GameObject SEgameObj;       //Unity上で作ったGameObjectである名前SEを入れる変数

    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;
    Transform playerTr;                 // プレイヤーのTransform

    void Start()
    {
        SEgameObj = GameObject.Find("SE"); //Unity上で作ったSEを取得

    }

    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {
        //弾上へ
        transform.position -= Vector3.up * (Time.deltaTime * _Speed);

        //画面下端に出たらオブジェクト破壊
        if (transform.position.y < -Screen.height / _screenRatio)
            Destroy(gameObject);

    }

    //弾が何かに当たって弾消滅
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーに当たったら
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Bullet"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();

            // 弾が当たった場所に爆発エフェクトを生成する
            Instantiate(
                _effectEnemy,
                collision.transform.localPosition,
                Quaternion.identity);

            _deadCallback?.Invoke();  //メモ：？は_deadCallbackがnullじゃないときに関数を呼び出す
            Destroy(gameObject);//弾消滅
        }
    }
}
