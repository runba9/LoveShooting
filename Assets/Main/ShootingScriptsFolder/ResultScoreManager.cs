using TMPro;
using UnityEngine;


public class ResultScoreManager : MonoBehaviour
{
    public ScoreScripts _scoreScripts;        //スクリプト呼び出し

    public TextMeshProUGUI TitmertextScore;   //タイムスコアテキスト
    public TextMeshProUGUI TextScore;         //スコアテキスト
    public TextMeshProUGUI TotalScoreText;    //全体のスコア
    public TextMeshProUGUI RankText;          //ランクテキスト

    ////デバッグ用
    //[SerializeField]
    //public float TotalScore;
    /*　メモ
     * 上を解除したら下のこれを探してコメントアウト
     
       //スコアと時間を計算
        float TotalScore = Score + Timerscore;
     */


    void Start()
    {
        //ScoreScripts持ってくる
        _scoreScripts = GetComponent<ScoreScripts>();

        SaveCalculation();


        //StartCoroutine(Ranking());

    }

    /// <summary>
    /// スコア
    /// </summary>
    public void SaveCalculation()
    {
        //セーブ読み込み（セーブデータが無かったら０を返す）
        var Score = PlayerPrefs.GetInt("SCORE", 0);
        /*メモ---------
         var　で　ScoreScripts　のスクリプトにある　Score　の値を持ってこれる   
         */

    //時間セーブデータ（セーブデータが無かったら０を返す）
    var Timerscore = PlayerPrefs.GetFloat("TIME", 0);


        //画面に貰った数値を表示
        TextScore.text = "LoveScore : " + Score;
        TitmertextScore.text = "TimerScore: " + Timerscore.ToString("F1");

        //スコアと時間を計算
        float TotalScore = Score + Timerscore;


        //トータルスコアとして出力
        TotalScoreText.text = "TotalScore : " + TotalScore.ToString("F1");

        //トータルスコア
        if (TotalScore > 400)
        {
            RankText.text = "S";
        }

        else if (TotalScore <= 400 && TotalScore >= 200)
        {
            RankText.text = "A";
        }
        else if(TotalScore < 200)
        {
            RankText.text = "B";
        }


    }


    //private IEnumerator Ranking()
    //{
    //    //0.5秒待って
    //    yield return new WaitForSeconds(0.5f);

    //    RankText.text = "A";
    //}
}
