using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFixationCatScripts : MonoBehaviour
{
    [SerializeField]
    public GameObject _CatEnemy;                  //この猫オブジェを入れる箱
    [SerializeField]
    private GameObject _blackLoveBullet;          //攻撃用の箱           
    [SerializeField]
    private float _Speed = 1f;                    //弾のスピード

    private GameObject SEgameObj;                 //Unity上で作ったGameObjectである名前SEを入れる変数         
    
    void Start()
    {

        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得

        //BulletsCat(銃弾発射)を3秒おきに呼び出す
        InvokeRepeating("BulletsCat", 1, 3);
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
        //プレイヤーか銃弾に当たったら
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Bullet"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();

            //ダメージが入ったらオブジェを非表示
            _CatEnemy.SetActive(false);
            /// 死んだら0.1秒後に呼び出し当たったことを分かりやすくする演出
            Invoke(("CatrevivalOn"), 0.1f);

            //Destroy(gameObject);//弾消滅
        }
    }

    /// <summary>
    /// 死んだら0.1秒後に呼び出す
    /// </summary>
    public void CatrevivalOn()
    {
        //画像を表示
        _CatEnemy.SetActive(true);
    }

}
