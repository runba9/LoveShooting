using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFixationCatScripts : MonoBehaviour
{
    [SerializeField]
    public GameObject _CatEnemy;                  //このオブジェを入れる箱
    [SerializeField]
    private GameObject _blackLoveBullet;          //攻撃用の箱           
    [SerializeField]
    private float _Speed = 1f;                    //弾のスピード

    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;
    private GameObject SEgameObj;                 //Unity上で作ったGameObjectである名前SEを入れる変数
    void Start()
    {

        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得

        //BulletsCat(銃弾発射)を3秒おきに呼び出す
        InvokeRepeating("BulletsCat", 1, 3);
    }

    private void Update()
    {
        //敵は左から来る
        transform.position += Vector3.left * (Time.deltaTime * 6);
        //画面外に出たらエネミー死ぬ
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);

    }

    /// <summary>
    /// 銃弾発射
    /// </summary>
    public void BulletsCat()
    {
        //弾を生成する
        var bullet = Instantiate(_blackLoveBullet);
        //弾の位置情報
        bullet.transform.position = transform.position + Vector3.right * (Time.deltaTime * _Speed);
    }

    //弾が何かに当たって弾消滅
    public void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーか銃弾,ガードアイテムに当たったら
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Bullet") || other.gameObject.tag.Equals("Option"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();

            //重力を与えて落下させる
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;

        }
    }

}
