using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesBulletView : MonoBehaviour
{
    //弾のスピード
    [SerializeField]
    private float _Speed = 1f;
    private GameObject SEgameObj;       //Unity上で作ったGameObjectである名前SEを入れる変数
    public static ChoicesBulletView _choicesBulletView;   //どこでもスクリプトを呼び出すため
    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;
    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity上で作ったSEを取得
    }
        void Update()
    {
        transform.position -= Vector3.right * (Time.deltaTime * _Speed);

        //画面左端に出たらオブジェクト破壊
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //弾がエネミーに当たったら
        if (collision.gameObject.tag.Equals("Player"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().ItemSE();

            Destroy(this.gameObject);     //敵消滅
        }

    }
}
