using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BulletView : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 0.1f;
    private GameObject gameObjScore; //Unity上で作ったGameObjectである名前GameManagerを入れる変数

    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    void Start()
    {
        gameObjScore = GameObject.Find("GameManager");//Unity上で作ったGameManagerを取得
    }
    void Update()
    {
        transform.position += Vector3.right * (Time.deltaTime * _Speed);
        //画面の右端に出たのかの判定、出たらオブジェクト消去
        if (transform.position.x > Screen.width / _screenRatio)
            Destroy(gameObject);
    }

    //弾が何かに当たって弾消滅
    public void OnTriggerEnter2D(Collider2D other)
    {
        //エネミーに当たったら
        if (other.gameObject.tag.Equals("Enemy"))
        {
            gameObjScore.GetComponent<ScoreScripts>().RefreshScoreText();//RefreshScoreText()を実行して加点
            Destroy(gameObject);//弾消滅
        }

        //チュートリアル用エネミーに当たったら
        if (other.gameObject.tag.Equals("Ring"))
        {
            Destroy(gameObject);//弾消滅
        }

    }

}
