using TMPro;
using UnityEngine;


public class ResultScoreManager : MonoBehaviour
{
    public ScoreScripts _scoreScripts;        //�X�N���v�g�Ăяo��

    public TextMeshProUGUI TitmertextScore;   //�^�C���X�R�A�e�L�X�g
    public TextMeshProUGUI TextScore;         //�X�R�A�e�L�X�g
    public TextMeshProUGUI TotalScoreText;    //�S�̂̃X�R�A
    public TextMeshProUGUI RankText;          //�����N�e�L�X�g

    ////�f�o�b�O�p
    //[SerializeField]
    //public float TotalScore;
    /*�@����
     * ������������牺�̂����T���ăR�����g�A�E�g
     
       //�X�R�A�Ǝ��Ԃ��v�Z
        float TotalScore = Score + Timerscore;
     */


    void Start()
    {
        //ScoreScripts�����Ă���
        _scoreScripts = GetComponent<ScoreScripts>();

        SaveCalculation();


        //StartCoroutine(Ranking());

    }

    /// <summary>
    /// �X�R�A
    /// </summary>
    public void SaveCalculation()
    {
        //�Z�[�u�ǂݍ��݁i�Z�[�u�f�[�^������������O��Ԃ��j
        var Score = PlayerPrefs.GetInt("SCORE", 0);
        /*����---------
         var�@�Ł@ScoreScripts�@�̃X�N���v�g�ɂ���@Score�@�̒l�������Ă����   
         */

    //���ԃZ�[�u�f�[�^�i�Z�[�u�f�[�^������������O��Ԃ��j
    var Timerscore = PlayerPrefs.GetFloat("TIME", 0);


        //��ʂɖ�������l��\��
        TextScore.text = "LoveScore : " + Score;
        TitmertextScore.text = "TimerScore: " + Timerscore.ToString("F1");

        //�X�R�A�Ǝ��Ԃ��v�Z
        float TotalScore = Score + Timerscore;


        //�g�[�^���X�R�A�Ƃ��ďo��
        TotalScoreText.text = "TotalScore : " + TotalScore.ToString("F1");

        //�g�[�^���X�R�A
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
    //    //0.5�b�҂���
    //    yield return new WaitForSeconds(0.5f);

    //    RankText.text = "A";
    //}
}
