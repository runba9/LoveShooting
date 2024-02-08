using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TutorialPanelScripts : MonoBehaviour
{

    [SerializeField]
    private GameObject TutorialPanel;                                        //�J�������p�l���̔�

    //�{�X�̃Z���t�����[�v
    [SerializeField]
    public Image BossTextImage;                                              //�{�X�̃Z���t��z�u����ꏊ
    [SerializeField]
    private List<Sprite> _choicesBossTextSpriteBoxs = new List<Sprite>();    //�{�X�̃Z���t�̃��X�g
    private int index = 0;

    //�N���e�B�J���U���e�L�X�g�̃Z���t�����[�v
    [SerializeField]
    public Image LoveTextImage;                                              //�N���e�B�J���U���{�^���̃Z���t��z�u����ꏊ
    [SerializeField]
    private List<Sprite> _choicesloveTextSpriteBoxs = new List<Sprite>();    //�N���e�B�J���U���p�Z���t�̃��X�g

    //�v���C���[�̉񕜃e�L�X�g�̃Z���t�����[�v
    [SerializeField]
    public Image BadTextImage;                                              //�v���C���[�̉񕜃{�^���̃Z���t��z�u����ꏊ
    [SerializeField]
    private List<Sprite> _choicesBadTextImageSpriteBoxs = new List<Sprite>();    //�v���C���[�̉񕜗p�Z���t�̃��X�g

    //�����N����Ȃ��e�L�X�g�̃Z���t�����[�v
    [SerializeField]
    public Image NormalTextImage;                                              //�����N����Ȃ��{�^���̃Z���t��z�u����ꏊ
    [SerializeField]
    private List<Sprite> _choicesNormalTextImageSpriteBoxs = new List<Sprite>();    //�����N����Ȃ��p�Z���t�̃��X�g



    /*�e�L�X�g�t�H���g�o�[�W����
    //�I�����{�^���e�L�X�g
    [SerializeField]
    private TextMeshProUGUI _loveText;                             //�{�^���I�����e�L�X�g�i�N���e�B�J���U���j
    [SerializeField]
    private TextMeshProUGUI _badText;                              //�{�^���I�����e�L�X�g�i�v���C���[�񕜁j
    [SerializeField]
    private TextMeshProUGUI _normalText;                           //�{�^���I�����e�L�X�g�i�������Ȃ��j
    */

    public GameObject _demoscript;                                 //Unity��ō����GameObject�ł��閼�OChoicesgameObj������ϐ�

    private GameObject _playerLifeExpectancyRestored;�@            //�Ăяo���pScoreScripts���i�[����ׂ̔�

    public static TutorialPanelScripts _tutorialPanelScripts;      //�ǂ��ł��X�N���v�g���Ăяo������
    public void Start()
    {
        //Unity��ō����GameManager���擾
        _demoscript = GameObject.Find("GameManager");
        _playerLifeExpectancyRestored = GameObject.Find("GameManager");

        /*�e�L�X�g�t�H���g�o�[�W����
        //��ʂɖ�������l��\��
        _loveText.text = "Love";
        _badText.text = "Bad";
        _normalText.text = "Normal";
        */
    }


    /// <summary>
    ///�I�������[�v
    /// </summary>
    public void ChoicesBossTextLoop()
    {
        //���𑝂₵�Ă�����
        index++;

        //�X�R�A�@/ ���X�g�̑S�̂̐�
        index %= _choicesBossTextSpriteBoxs.Count;         
        
        index %= _choicesloveTextSpriteBoxs.Count;
        index %= _choicesBadTextImageSpriteBoxs.Count;
        index %= _choicesNormalTextImageSpriteBoxs.Count;

        //���[�v����
        BossTextImage.sprite = _choicesBossTextSpriteBoxs[index];

        LoveTextImage.sprite = _choicesloveTextSpriteBoxs[index];
        BadTextImage.sprite = _choicesBadTextImageSpriteBoxs[index];
        NormalTextImage.sprite = _choicesNormalTextImageSpriteBoxs[index];
    }



    //�I�����U���Ń{�^����I�񂾂�s���@�\�R��

    /// <summary>
    /// �{�X�G�ɃN���e�B�J�������U��
    /// </summary>
    public void CrickButtonMovingTimeForward_Love()
    {
        //����i�߂�
        Time.timeScale = 1;

        //EnemyBoss�X�N���v�g�̒����Ăяo��
        _demoscript.GetComponent<ShootingSceneManagerGameStage>()._enemyBoss.Criticalattack();

        //�{�X�̃Z���t���J�E���g���ă��[�v�����鏈���Ăяo��
        ChoicesBossTextLoop();

        /*�e�L�X�g�t�H���g�o�[�W����
        //��ʂɖ�������l��\��
        _loveText.text = "Yes";
        _badText.text = "No";
        _normalText.text = "I don't Know";
        */

        //�p�l�����\��
        TutorialPanel.SetActive(false);

    }

    /// <summary>
    /// �v���C���[�񕜋@�\
    /// </summary>
    public void CrickButtonMovingTimeForward_Bad()
    {
        //����i�߂�
        Time.timeScale = 1;

        //ScoreScripts���Ăяo���A�v���C���[��hp����
        _playerLifeExpectancyRestored.GetComponent<ScoreScripts>().PlayerLifeExpectancyRestored();

        //�{�X�̃Z���t���J�E���g���ă��[�v�����鏈���Ăяo��
        ChoicesBossTextLoop();

        /*�e�L�X�g�t�H���g�o�[�W����
        //��ʂɖ�������l��\��
        _loveText.text = "Love";
        _badText.text = "Bad";
        _normalText.text = "Normal";
        */

        //�p�l�����\��
        TutorialPanel.SetActive(false);
    }

    /// <summary>
    /// �������Ȃ�
    /// </summary>
    public void CrickButtonMovingTimeForward_Nomal()
    {
        //����i�߂�
        Time.timeScale = 1;

        //�{�X�̃Z���t���J�E���g���ă��[�v�����鏈���Ăяo��
        ChoicesBossTextLoop();

        /*�e�L�X�g�t�H���g�o�[�W����
        //��ʂɖ�������l��\��
        _loveText.text = "Cute";
        _badText.text = "Cool";
        _normalText.text = "Destroy";
        */

        //�p�l�����\��
        TutorialPanel.SetActive(false);
    }

}
