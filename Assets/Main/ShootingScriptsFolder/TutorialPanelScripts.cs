using System.Threading;
using UnityEngine;

public class TutorialPanelScripts : MonoBehaviour
{

    [SerializeField] 
    private GameObject TutorialPanel;                              //開閉したいパネルの箱

    public GameObject _demoscript;                                 //Unity上で作ったGameObjectである名前ChoicesgameObjを入れる変数
    public static TutorialPanelScripts _tutorialPanelScripts;      //どこでもスクリプトを呼び出すため
    public void Start()
    {
        _demoscript = GameObject.Find("GameManager");     　       //Unity上で作ったGameManagerを取得
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
        //パネルを非表示
        TutorialPanel.SetActive(false);

    }

    /// <summary>
    /// プレイヤーに攻撃する機能（未実装）
    /// </summary>
    public void CrickButtonMovingTimeForward_Bad()
    {
        //時を進める
        Time.timeScale = 1;
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
        //パネルを非表示
        TutorialPanel.SetActive(false);
    }

}
