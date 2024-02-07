using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
    private GameObject _bossHpSlider;           //ボスのhpスライダーを入れる箱

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
    public int Life = 3;                        //プレイヤーの残機数
    private int Score = 0;                      //現在のスコア
    private float Timerscore = 0;               //経過時間

    //セーブ用データ
    private const string KEY_SCORE = "SCORE";      //スコア
    private const string KEY_TIME = "TIME";       //時間

    //呼び出し
    private GameObject SEgameObj;               //Unity上で作ったGameObjectである名前SEを入れる変数
    private GameObject PlayerObj;               //Unity上で作ったGameObjectである名前PlayerObjを入れる変数
    private GameObject BossgameObj;             //Unity上で作ったGameObjectである名前GameManagerを入れる変数
    public static ScoreScripts _scoreScripts;   //どこでもスクリプトを呼び出すため

    public void Awake()
    {
        // 保存されているすべてのデータを消す
        PlayerPrefs.DeleteAll();
    }

    public void Start()
    {
        SEgameObj   = GameObject.Find("SE");              //Unity上で作ったSEを取得
        BossgameObj = GameObject.Find("GameManager");     //Unity上で作ったGameManagerを取得
        PlayerObj   = GameObject.Find("GameManager");     //Unity上で作ったGameManagerを取得
    }


    public void Update()
    {
        //時間を加算させていく
        Timerscore += Time.deltaTime;
        TimerRefreshScoreText();
    }

    /// <summary>
    /// 弾とエネミーが相殺したらスコアを加算させる
    /// </summary>
    public void RefreshScoreText()
    {
        //弾とエネミーが相殺したらスコアを加算させる
        Score++;

        //スコア加算したらスコアデータ保存呼び出し
        SaveGameData();

        // オブジェクトからTextコンポーネントを取得
        Text score_text = textScore.GetComponent<Text>();
        //画面に貰った数値を表示
        textScore.text = "LoveScore : " + Score;

        //スコアがMaxになったらホワイトストロベリーを表示させる
        if (Score == whitestrawberryScoreMax)
        {
            whitestrawberry.SetActive(true);
        }
        //スコアがMaxになったらブラックミントを表示させる
        if (Score == blackmintScoreMax)
        {
            blackmint.SetActive(true);
        }
        //スコアがMaxになったらボスとボスのhpバーを登場させる
        if (Score == BossenemyScoreMax)
        {
            _bossHpSlider.SetActive(true);
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
        titmertextScore.text = "Timer        : " + Timerscore.ToString("F1");//ToString("00")＝("F１")同じ、F２で小数点追加など出来る
    }

    /// <summary>
    /// プレイヤーの残機テキスト
    /// </summary>
    public void PlayerRemainingLivesText()
    {
        //ダメージ用SE再生
        SEgameObj.GetComponent<SEScripts>().damageSE();

        //エネミーや障害物などに当たったら残機を減らす
        Life--;
        // オブジェクトからTextコンポーネントを取得
        Text Life_text = playerRemainingLives.GetComponent<Text>();
        //画面に貰った数値を表示
        playerRemainingLives.text = "×" + Life;

        //残機０でゲームオーバー画面へ移動
        if (Life == 0)
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().playerSE();
            //タグ持ち身代わり表示（タグが見つからないとエラーをはかなくさせる）
            _damePlayer.SetActive(true);
            //フェートインアウト処理後ゲームオーバー画面に飛ぶ
            SceneChangr.scenechangrInstance._fade.SceneFade("GameOverScene");
        }
        else
        {
            //残機数がまだあればプレイヤーの残機追加
            PlayerObj.GetComponent<ShootingSceneManagerGameStage>().Playerrevival();

        }
    }



    //ゲームデータをセーブする
    public void SaveGameData()
    {
        PlayerPrefs.SetInt(KEY_SCORE, Score);//スコア
        PlayerPrefs.SetFloat(KEY_TIME, Timerscore);//時間
        
        PlayerPrefs.Save();//保存
    }

}
