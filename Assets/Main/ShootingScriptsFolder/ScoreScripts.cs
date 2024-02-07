using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreScripts : MonoBehaviour
{
    public TextMeshProUGUI textScore;           //�X�R�A�e�L�X�g
    public TextMeshProUGUI titmertextScore;     //�^�C���X�R�A�e�L�X�g
    public TextMeshProUGUI playerRemainingLives;//�v���C���[�c�@�e�L�X�g

    //�v���C���[�^�O�������Ă��G���[���͂��Ȃ�������ׂ̃v���C���[�^�O�����I�u�W�F�p�̔�
    [SerializeField]
    public GameObject _damePlayer;

    [SerializeField]
    GameObject enemy;                           //�G�l�~�[�����锠
    [SerializeField]
    private GameObject _bossHpSlider;           //�{�X��hp�X���C�_�[�����锠

    [SerializeField]
    public GameObject blackmint;                //�`���R�~���g��Q��
    [SerializeField]
    public GameObject whitestrawberry;          //�z���C�g�X�g���x���[��Q��

    [SerializeField]
    public float whitestrawberryScoreMax = 20f; //�G�l�~�[��whitestrawberryScoreMax��|������z���C�g�X�g���x���[��Q���o�ꐔ
    [SerializeField]
    public float blackmintScoreMax = 40f;       //�G�l�~�[��blackmintScoreMax��|������`���R�~���g��Q���o�ꐔ
    [SerializeField]
    public float BossenemyScoreMax = 70f;        //�G�l�~�[��BossenemyScoreMax��|������{�X�o�ꐔ

    [SerializeField]
    public int Life = 3;                        //�v���C���[�̎c�@��
    private int Score = 0;                      //���݂̃X�R�A
    private float Timerscore = 0;               //�o�ߎ���

    //�Z�[�u�p�f�[�^
    private const string KEY_SCORE = "SCORE";      //�X�R�A
    private const string KEY_TIME = "TIME";       //����

    //�Ăяo��
    private GameObject SEgameObj;               //Unity��ō����GameObject�ł��閼�OSE������ϐ�
    private GameObject PlayerObj;               //Unity��ō����GameObject�ł��閼�OPlayerObj������ϐ�
    private GameObject BossgameObj;             //Unity��ō����GameObject�ł��閼�OGameManager������ϐ�
    public static ScoreScripts _scoreScripts;   //�ǂ��ł��X�N���v�g���Ăяo������

    public void Awake()
    {
        // �ۑ�����Ă��邷�ׂẴf�[�^������
        PlayerPrefs.DeleteAll();
    }

    public void Start()
    {
        SEgameObj   = GameObject.Find("SE");              //Unity��ō����SE���擾
        BossgameObj = GameObject.Find("GameManager");     //Unity��ō����GameManager���擾
        PlayerObj   = GameObject.Find("GameManager");     //Unity��ō����GameManager���擾
    }


    public void Update()
    {
        //���Ԃ����Z�����Ă���
        Timerscore += Time.deltaTime;
        TimerRefreshScoreText();
    }

    /// <summary>
    /// �e�ƃG�l�~�[�����E������X�R�A�����Z������
    /// </summary>
    public void RefreshScoreText()
    {
        //�e�ƃG�l�~�[�����E������X�R�A�����Z������
        Score++;

        //�X�R�A���Z������X�R�A�f�[�^�ۑ��Ăяo��
        SaveGameData();

        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text score_text = textScore.GetComponent<Text>();
        //��ʂɖ�������l��\��
        textScore.text = "LoveScore : " + Score;

        //�X�R�A��Max�ɂȂ�����z���C�g�X�g���x���[��\��������
        if (Score == whitestrawberryScoreMax)
        {
            whitestrawberry.SetActive(true);
        }
        //�X�R�A��Max�ɂȂ�����u���b�N�~���g��\��������
        if (Score == blackmintScoreMax)
        {
            blackmint.SetActive(true);
        }
        //�X�R�A��Max�ɂȂ�����{�X�ƃ{�X��hp�o�[��o�ꂳ����
        if (Score == BossenemyScoreMax)
        {
            _bossHpSlider.SetActive(true);
            BossgameObj.GetComponent<ShootingSceneManagerGameStage>().BosEnemy();
        }
    }

    /// <summary>
    /// �X�R�A�e�L�X�g�X�V
    /// </summary>
    public void TimerRefreshScoreText()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text Time_text = titmertextScore.GetComponent<Text>();
        //��ʂɖ�������l��\��
        titmertextScore.text = "Timer        : " + Timerscore.ToString("F1");//ToString("00")��("F�P")�����AF�Q�ŏ����_�ǉ��ȂǏo����
    }

    /// <summary>
    /// �v���C���[�̎c�@�e�L�X�g
    /// </summary>
    public void PlayerRemainingLivesText()
    {
        //�_���[�W�pSE�Đ�
        SEgameObj.GetComponent<SEScripts>().damageSE();

        //�G�l�~�[���Q���Ȃǂɓ���������c�@�����炷
        Life--;
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text Life_text = playerRemainingLives.GetComponent<Text>();
        //��ʂɖ�������l��\��
        playerRemainingLives.text = "�~" + Life;

        //�c�@�O�ŃQ�[���I�[�o�[��ʂֈړ�
        if (Life == 0)
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().playerSE();
            //�^�O�����g����\���i�^�O��������Ȃ��ƃG���[���͂��Ȃ�������j
            _damePlayer.SetActive(true);
            //�t�F�[�g�C���A�E�g������Q�[���I�[�o�[��ʂɔ��
            SceneChangr.scenechangrInstance._fade.SceneFade("GameOverScene");
        }
        else
        {
            //�c�@�����܂�����΃v���C���[�̎c�@�ǉ�
            PlayerObj.GetComponent<ShootingSceneManagerGameStage>().Playerrevival();

        }
    }



    //�Q�[���f�[�^���Z�[�u����
    public void SaveGameData()
    {
        PlayerPrefs.SetInt(KEY_SCORE, Score);//�X�R�A
        PlayerPrefs.SetFloat(KEY_TIME, Timerscore);//����
        
        PlayerPrefs.Save();//�ۑ�
    }

}
