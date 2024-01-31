using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreScripts : MonoBehaviour
{
    public TextMeshProUGUI textScore;           //スコアテキスト
    public TextMeshProUGUI titmertextScore;     //タイムスコアテキスト
    public TextMeshProUGUI playerRemainingLives;//プレイヤー残機テキスト

    //プレイヤータグが消えてもエラーをはかなくさせる為のプレイヤータグ持ちオブジェ用の箱
    [SerializeField]
    public GameObject _damePlayer;

    [SerializeField]
    GameObject enemy;                           //エネミーを入れる箱
    [SerializeField]
    public GameObject blackmint;                //チョコミント障害物
    [SerializeField]
    public GameObject whitestrawberry;          //ホワイトストロベリー障害物

    [SerializeField]
    public float whitestrawberryScoreMax = 20f; //エネミーをwhitestrawberryScoreMax回倒したらホワイトストロベリー障害物登場数
    [SerializeField]
    public float blackmintScoreMax = 40f;       //エネミーをblackmintScoreMax回倒したらチョコミント障害物登場数
    [SerializeField]
    public float BossenemyScoreMax = 70f;        //エネミーをBossenemyScoreMax回倒したらボス登場数

    [SerializeField]
    public int life = 3;                        //プレイヤーの残機数
    private int score = 0;                      //現在のスコア
    private float timerscore = 0;               //経過時間

    //呼び出し
    private GameObject SEgameObj;               //Unity上で作ったGameObjectである名前SEを入れる変数
    private GameObject PlayerObj;               //Unity上で作ったGameObjectである名前PlayerObjを入れる変数
    private GameObject BossgameObj;             //Unity上で作ったGameObjectである名前GameManagerを入れる変数
    public static ScoreScripts _scoreScripts;   //どこでもスクリプトを呼び出すため

    public void Start()
    {
        SEgameObj = GameObject.Find("SE");              //Unity上で作ったSEを取得
        BossgameObj = GameObject.Find("GameManager");   //Unity上で作ったGameManagerを取得
        PlayerObj = GameObject.Find("GameManager");     //Unity上で作ったGameManagerを取得
    }

    public void Update()
    {
        //時間を加算させていく
        timerscore += Time.deltaTime;
        TimerRefreshScoreText();
    }

    /// <summary>
    /// 弾とエネミーが相殺したらスコアを加算させる
    /// </summary>
    public void RefreshScoreText()
    {
        //弾とエネミーが相殺したらスコアを加算させる
        score++;

        // オブジェクトからTextコンポーネントを取得
        Text score_text = textScore.GetComponent<Text>();
        //画面に貰った数値を表示
        textScore.text = "LoveScore : " + score;

        //スコアがMaxになったらホワイトストロベリーを表示させる
        if (score == whitestrawberryScoreMax)
        {
            whitestrawberry.SetActive(true);
        }
        //スコアがMaxになったらブラックミントを表示させる
        if (score == blackmintScoreMax)
        {
            blackmint.SetActive(true);
        }
        //スコアがMaxになったらボスを登場させる
        if (score == BossenemyScoreMax)
        {
            BossgameObj.GetComponent<ShootingSceneManagerGameStage>().BosEnemy();
        }
    }

    /// <summary>
    /// スコアテキスト更新
    /// </summary>
    public void TimerRefreshScoreText()
    {
        // オブジェクトからTextコンポーネントを取得
        Text Time_text = titmertextScore.GetComponent<Text>();
        //画面に貰った数値を表示
        titmertextScore.text = "Timer        : " + timerscore.ToString("F2");//ToString("00")＝("F１")同じ、F２で小数点追加など出来る
    }

    /// <summary>
    /// プレイヤーの残機テキスト
    /// </summary>
    public void PlayerRemainingLivesText()
    {
        //ダメージ用SE再生
        SEgameObj.GetComponent<SEScripts>().damageSE();

        //エネミーや障害物などに当たったら残機を減らす
        life--;
        // オブジェクトからTextコンポーネントを取得
        Text Life_text = playerRemainingLives.GetComponent<Text>();
        //画面に貰った数値を表示
        playerRemainingLives.text = "×" + life;

        //残機０でゲームオーバー画面へ移動
        if (life == 0)
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().playerSE();
            //タグ持ち身代わり表示（タグが見つからないとエラーをはかなくさせる）
            _damePlayer.SetActive(true);
            //フェートインアウト処理後ゲームオーバー画面に飛ぶ
            SceneChangr.scenechangrInstance._fade.SceneFade("GameOverSeen");
        }
        else
        {
            //残機数がまだあればプレイヤーの残機追加
            PlayerObj.GetComponent<ShootingSceneManagerGameStage>().Playerrevival();

        }
    }
}
