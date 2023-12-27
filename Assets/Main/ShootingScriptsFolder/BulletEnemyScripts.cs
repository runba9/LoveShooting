using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 0.1f;
    private GameObject SEgameObj;       //Unity上で作ったGameObjectである名前SEを入れる変数
    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得
    }
    void Update()
    {
        //弾左へ
        transform.position -= Vector3.right * (Time.deltaTime * _Speed);
        //画面左端に出たらオブジェクト破壊
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);
    }

    //弾が何かに当たって弾消滅
    public void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーに当たったら
        if (other.gameObject.tag.Equals("Player") ||  other.gameObject.tag.Equals("Bullet"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();
            Destroy(gameObject);//弾消滅
        }
    }
}
