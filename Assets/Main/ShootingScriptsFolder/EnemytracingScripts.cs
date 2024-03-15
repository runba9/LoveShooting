using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytracingScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed;                   //エネミーのスピード
    private bool EnemyThrobbing;            //死んだら追いかける機能無くす為の判定

    Transform playerTransform;              //プレイヤー（追いかけたい対象）の座標Transform
    [SerializeField]
    private float Acceleration_speed = 5;                    //プレイヤーの座標を見つけて追いかけるスピード
    public EffectScripts _effectEnemy;      //爆発エフェクトのプレハブ
    private System.Action _deadCallback;    //死んだときに死んだことを伝える
    private GameObject SEgameObj;           //Unity上で作ったGameObjectである名前SEを入れる変数

    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得

        EnemyThrobbing = true;            //エネミー生きている判定
    }
    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;//キックバック
    }

    void Update()
    {
        //敵は左から来る
        transform.position += Vector3.left * (Time.deltaTime * _Speed);

        //プレイヤータグを探し座標取得させる
        GameObject playerObj = GameObject.Find("Player(Clone)");

        // ゲームオブジェクトが存在するか確認
        if (playerObj != null)
        {
            //Debug.Log("オブジェクト名が見つかりました：" + playerObj.name);

            //エネミー生きてる判定
            if(EnemyThrobbing == true)
            {
                //プレイヤーの座標を見つけて追いかける行動開始
                playerTransform = playerObj.transform;
                // プレイヤーとの距離がAcceleration_speed未満になったら
                if (Vector2.Distance(this.transform.position, playerTransform.position) <= Acceleration_speed)
                {
                    transform.position = Vector2.Lerp(transform.position, playerTransform.transform.position, _Speed * Time.deltaTime);
                    
                }
                else 
                {
                }
            }


        }
        // ゲームオブジェクトが存在しなかったらログを出す
        else
        {
            //Debug.LogWarning("オブジェクト名： 'ObjectName'が見つかりません");
        }

        //画面外に出たらエネミー死ぬ
        if (transform.position.x < -Screen.width / _screenRatio)
        Destroy(gameObject);
        }

        //敵消滅
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //弾,プレイヤー、ガードアイテムがエネミーに当たったら
            if (collision.gameObject.tag.Equals("Bullet") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Option"))
            {
                //エネミー死んだ判定
                EnemyThrobbing = false;

                //ダメージ用SE再生
                SEgameObj.GetComponent<SEScripts>().damageSE();

                // 弾が当たった場所に爆発エフェクトを生成する
                Instantiate(_effectEnemy,collision.transform.localPosition,Quaternion.identity);

                _deadCallback?.Invoke();  //メモ：？は_deadCallbackがnullじゃないときに関数を呼び出す
   
                //重力を与えて落下させる
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;

                //敵消滅
                //Destroy(gameObject);
        }

    }

    } 
