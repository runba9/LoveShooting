using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytracingScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed;                   //エネミーのスピード

    Transform playerTransform;              //プレイヤーの座標Transform
    private GameObject playerObj;           //Unity上で作ったGameObjectである名前playerObjを入れる変数

    public EffectScripts _effectEnemy;      //爆発エフェクトのプレハブ
    private System.Action _deadCallback;    //死んだときに死んだことを伝える
    private GameObject SEgameObj;           //Unity上で作ったGameObjectである名前SEを入れる変数

    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得

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

            //プレイヤーの座標を見つけて追いかける行動開始
            playerTransform = playerObj.transform;
            // プレイヤーとの距離が5未満になったら
            if (Vector2.Distance(this.transform.position, playerTransform.position) <= 5f)
            {
                transform.position = Vector2.Lerp(transform.position, playerTransform.transform.position, _Speed * Time.deltaTime);
                //transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, _Speed * Time.deltaTime);
            }
            else 
            {
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
            //弾がエネミーに当たったら
            if (collision.gameObject.tag.Equals("Bullet") || collision.gameObject.tag.Equals("Player"))
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
