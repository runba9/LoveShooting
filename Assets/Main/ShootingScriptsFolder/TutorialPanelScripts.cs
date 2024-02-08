using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TutorialPanelScripts : MonoBehaviour
{

    [SerializeField]
    private GameObject TutorialPanel;                                        //開閉したいパネルの箱

    //ボスのセリフをループ
    [SerializeField]
    public Image BossTextImage;                                              //ボスのセリフを配置する場所
    [SerializeField]
    private List<Sprite> _choicesBossTextSpriteBoxs = new List<Sprite>();    //ボスのセリフのリスト
    private int index = 0;

    //クリティカル攻撃テキストのセリフをループ
    [SerializeField]
    public Image LoveTextImage;                                              //クリティカル攻撃ボタンのセリフを配置する場所
    [SerializeField]
    private List<Sprite> _choicesloveTextSpriteBoxs = new List<Sprite>();    //クリティカル攻撃用セリフのリスト

    //プレイヤーの回復テキストのセリフをループ
    [SerializeField]
    public Image BadTextImage;                                              //プレイヤーの回復ボタンのセリフを配置する場所
    [SerializeField]
    private List<Sprite> _choicesBadTextImageSpriteBoxs = new List<Sprite>();    //プレイヤーの回復用セリフのリスト

    //何も起こらないテキストのセリフをループ
    [SerializeField]
    public Image NormalTextImage;                                              //何も起こらないボタンのセリフを配置する場所
    [SerializeField]
    private List<Sprite> _choicesNormalTextImageSpriteBoxs = new List<Sprite>();    //何も起こらない用セリフのリスト



    /*テキストフォントバージョン
    //選択肢ボタンテキスト
    [SerializeField]
    private TextMeshProUGUI _loveText;                             //ボタン選択肢テキスト（クリティカル攻撃）
    [SerializeField]
    private TextMeshProUGUI _badText;                              //ボタン選択肢テキスト（プレイヤー回復）
    [SerializeField]
    private TextMeshProUGUI _normalText;                           //ボタン選択肢テキスト（何もしない）
    */

    public GameObject _demoscript;                                 //Unity上で作ったGameObjectである名前ChoicesgameObjを入れる変数

    private GameObject _playerLifeExpectancyRestored;　            //呼び出し用ScoreScriptsを格納する為の箱

    public static TutorialPanelScripts _tutorialPanelScripts;      //どこでもスクリプトを呼び出すため
    public void Start()
    {
        //Unity上で作ったGameManagerを取得
        _demoscript = GameObject.Find("GameManager");
        _playerLifeExpectancyRestored = GameObject.Find("GameManager");

        /*テキストフォントバージョン
        //画面に貰った数値を表示
        _loveText.text = "Love";
        _badText.text = "Bad";
        _normalText.text = "Normal";
        */
    }


    /// <summary>
    ///選択肢ループ
    /// </summary>
    public void ChoicesBossTextLoop()
    {
        //数を増やしていって
        index++;

        //スコア　/ リストの全体の数
        index %= _choicesBossTextSpriteBoxs.Count;         
        
        index %= _choicesloveTextSpriteBoxs.Count;
        index %= _choicesBadTextImageSpriteBoxs.Count;
        index %= _choicesNormalTextImageSpriteBoxs.Count;

        //ループ処理
        BossTextImage.sprite = _choicesBossTextSpriteBoxs[index];

        LoveTextImage.sprite = _choicesloveTextSpriteBoxs[index];
        BadTextImage.sprite = _choicesBadTextImageSpriteBoxs[index];
        NormalTextImage.sprite = _choicesNormalTextImageSpriteBoxs[index];
    }



    //選択肢攻撃でボタンを選んだら行う機能３つ

    /// <summary>
    /// ボス敵にクリティカル告白攻撃
    /// </summary>
    public void CrickButtonMovingTimeForward_Love()
    {
        //時を進める
        Time.timeScale = 1;

        //EnemyBossスクリプトの中を呼び出す
        _demoscript.GetComponent<ShootingSceneManagerGameStage>()._enemyBoss.Criticalattack();

        //ボスのセリフをカウントしてループさせる処理呼び出し
        ChoicesBossTextLoop();

        /*テキストフォントバージョン
        //画面に貰った数値を表示
        _loveText.text = "Yes";
        _badText.text = "No";
        _normalText.text = "I don't Know";
        */

        //パネルを非表示
        TutorialPanel.SetActive(false);

    }

    /// <summary>
    /// プレイヤー回復機能
    /// </summary>
    public void CrickButtonMovingTimeForward_Bad()
    {
        //時を進める
        Time.timeScale = 1;

        //ScoreScriptsを呼び出し、プレイヤーのhpを回復
        _playerLifeExpectancyRestored.GetComponent<ScoreScripts>().PlayerLifeExpectancyRestored();

        //ボスのセリフをカウントしてループさせる処理呼び出し
        ChoicesBossTextLoop();

        /*テキストフォントバージョン
        //画面に貰った数値を表示
        _loveText.text = "Love";
        _badText.text = "Bad";
        _normalText.text = "Normal";
        */

        //パネルを非表示
        TutorialPanel.SetActive(false);
    }

    /// <summary>
    /// 何もしない
    /// </summary>
    public void CrickButtonMovingTimeForward_Nomal()
    {
        //時を進める
        Time.timeScale = 1;

        //ボスのセリフをカウントしてループさせる処理呼び出し
        ChoicesBossTextLoop();

        /*テキストフォントバージョン
        //画面に貰った数値を表示
        _loveText.text = "Cute";
        _badText.text = "Cool";
        _normalText.text = "Destroy";
        */

        //パネルを非表示
        TutorialPanel.SetActive(false);
    }

}
