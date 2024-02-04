using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShootingSceneManager : MonoBehaviour
{
    //�G�l�~�[�̃v���n�u
    [SerializeField]
    private GameObject _enemyPrefabs;
    //�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject _ItemPrefabs;
    //�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject _PlayerPrefabs;
    //�v���C���[�̃v���n�u
    [SerializeField]
    private PlayerScripts _PlayerPrefabsScripts;
    //�X�R�A�v���n�u
    [SerializeField]
    public TextMeshProUGUI textScore;               //�X�R�A�e�L�X�g

    //�������̃��X�g
    [SerializeField]
    private List<Sprite> _instructionManualList;    //�ς������摜�̃��X�g(������)
    public float _instructionManualListNumber = 2;  //�������̐�
    private int _instructionManua = 0;              //���������߂������y�[�W�̌�

    //�\���������p�l���̔��i����������j
    [SerializeField]
    public GameObject _instructionManuaPanel;       //�\���������摜�B
    public Image ExplainPoint;                      //�\���������摜�B��ݒu����
    public TMP_Text _nextText;                      //���փ{�^��

    //�\���������p�l���̔��i���@�������j
    [SerializeField]
    public GameObject TryEnemy;                     //�`���[�g���A����ɕ\�������e�L�X�g

    public bool Preface;                            //����������I��������ǂ���

    //�X�L�b�v�{�^��
    [SerializeField]
    public GameObject SkipButton;

    //�J�E���g�_�E��
    [SerializeField]
    private float timerscore = 20;           //�o�ߎ���

    private float _timer = 0;                //�o�ߎ���
    private float _limitTimer = 0;           //�o������
    private float _enemuLeftPos = 10;        //�G�̏o�����W�̂���X���W�E�Œ�

    void Start()
    {
        //����������I�������false�ɂ��Ă���
        Preface = true;

        //�`���[�g���A������
        StartCoroutine(Tutorial());
    }

    void Update()
    {
        //1�ԏ��text�\��
        RefreshScoreText();

        //����������I�������
        if (Preface == false)
        {
            _limitTimer = Random.Range(0.5f, 2.0f);

            _timer += Time.deltaTime;     //���Ԃ�ǉ�
                                          //�\�莞���𒴂�����
            if (_timer >= _limitTimer)
            {
                _timer = 0;               //���Ԃ����Z�b�g
                                          //�\�莞�Ԃ��Đݒ肷��
                _limitTimer = Random.Range(0.5f, 2.0f);
                InstantiateEnemy();       //�G�𐶐�

                //�摜�\��
                TryEnemy.SetActive(true);
            }
        }


        //20�b���班���������ĂO�ɂȂ�����
        timerscore -= Time.deltaTime;
        if (timerscore <= 0)
        {
            StartCoroutine(MovingScene());//�V�[���ړ�
        }

    }

    //�X�R�A�e�L�X�g�X�V
    public void RefreshScoreText()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text score_text = textScore.GetComponent<Text>();
        //��ʂɖ�������l��\��
        textScore.text = "TutorialTime : " + timerscore.ToString("00");//ToString("00")��("F�P")�����AF�Q�ŏ����_�ǉ��ȂǏo����
    }

    /// <summary>
    /// �`���[�g���A������(�������)
    /// </summary>
    /// <returns></returns>
    private IEnumerator Tutorial()
    {
        // 1�b�ҋ@
        yield return new WaitForSeconds(1);

        //�����~�߂�
        Time.timeScale = 0;

        //��������\��
        ExplainPoint.sprite = _instructionManualList[_instructionManua];
        _instructionManuaPanel.SetActive(true);

    }

    //�G�l�~�[�ƃA�C�e�����E���獶�։�����v���n�u�Ń����_���ɏo��
    private void InstantiateEnemy()
    {
        //�G�l�~�[
        if (Random.Range(0, 2) == 0)
        {
            var item = Instantiate(_enemyPrefabs);
            item.transform.position = new Vector3(_enemuLeftPos, Random.Range(-5, 5f), 0);
        }
    }



    //�����������̃y�[�W�ɁA���[�v������
    public void OnButtonExplainNext()
    {
        if (_instructionManua == _instructionManualListNumber)
        {
            //�ǂݏI����������
            _instructionManuaPanel.SetActive(false);
            //��������I���
            Preface = false;
            //����i�߂�
            Time.timeScale = 1;
            //�X�L�b�v�{�^���\��
            SkipButton.SetActive(true);
            PlayerAdmission();
            return;
        }
        _instructionManua++;
        ExplainPoint.sprite = _instructionManualList[_instructionManua];

        if (_instructionManua == _instructionManualListNumber) _nextText.SetText("OK");
    }

    /// <summary>
    /// �v���C���[�o��
    /// </summary>
    private void PlayerAdmission()
    {
        Instantiate(_PlayerPrefabs, new Vector3(-7, 0, 0), Quaternion.identity);
    }

    //�V�[���ړ�
    private IEnumerator MovingScene()
    {
        timerscore = 0;     //���Ԃ����Z�b�g

        //�t�F�[�h�p�̃L�����o�X���o���΃t�F�[�h�C���A�E�g���o����

        //4�b�҂�
        yield return new WaitForSeconds(4);

        //�t�F�[�g�C���A�E�g������X�e�[�W��ʂɔ��
        SceneManager.LoadScene("ShootingGameSceneStage_Mein");
        //SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSceneStage_Mein");
    }
}
