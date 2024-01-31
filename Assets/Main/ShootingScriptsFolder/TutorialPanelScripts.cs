using UnityEngine;

public class TutorialPanelScripts : MonoBehaviour
{
    [SerializeField] 
    private GameObject TutorialPanel;       //開閉したいパネルの箱

    private EnemyBoss demoscript;           //EnemyBossスクリプトの中を呼び出す為の箱

    //選択肢攻撃でボタンを選んだら行う機能３つ

    /// <summary>
    /// ボス敵にクリティカル告白攻撃
    /// </summary>
    public void CrickButtonMovingTimeForward_Love()
    {
        //時を進める
        Time.timeScale = 1;
        //パネルを非表示
        TutorialPanel.SetActive(false);
        //EnemyBossスクリプトの中を呼び出す
        demoscript.GetComponent<EnemyBoss>().Criticalattack();
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
