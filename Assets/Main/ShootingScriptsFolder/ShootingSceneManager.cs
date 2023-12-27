using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ShootingSceneManager : MonoBehaviour
{
    //�G�l�~�[�̃v���n�u
    [SerializeField]
    private GameObject _enemyPrefabs;
    //�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject _ItemPrefabs;
    //�v���C���[�̃v���n�u
    [SerializeField]
    private PlayerScripts _PlayerPrefabs;
    //�X�R�A�v���n�u
    [SerializeField]
    public TextMeshProUGUI textScore;        //�X�R�A�e�L�X�g
    private float timerscore = 20;           //�o�ߎ���

    private float _timer        = 0; //�o�ߎ���
    private float _limitTimer   = 0; //�o������
    private float _enemuLeftPos = 10;//�G�̏o�����W�̂���X���W�E�Œ�

    void Start()
    {
        _limitTimer = Random.Range(0.5f, 2.0f);
    }

    void Update()
    {
        //1�ԏ��text�\��
        RefreshScoreText();

        //20�b���班���������ĂO�ɂȂ�����
        timerscore -= Time.deltaTime;
        if (timerscore <= 0)
        {

            seenr();//�V�[���ړ�
        }


        _timer += Time.deltaTime;     //���Ԃ�ǉ�
            //�\�莞���𒴂�����
        if(_timer >= _limitTimer)
        {
            _timer = 0;               //���Ԃ����Z�b�g
            //�\�莞�Ԃ��Đݒ肷��
            _limitTimer = Random.Range(0.5f, 2.0f);
            InstantiateEnemy();       //�G�𐶐�
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

    //�G�l�~�[�ƃA�C�e�����E���獶�ւƉ�����o�Ă���̂��v���n�u�Ń����_���ɏo��
    private void InstantiateEnemy()
    {
        //�A�C�e��
        if(Random.Range(0, 4) == 0)
        {
            var enemy = Instantiate(_ItemPrefabs);
            enemy.transform.position = new Vector3(_enemuLeftPos, Random.Range(-5, 5f), 0);
        }
        //�G�l�~�[
        if (Random.Range(0, 2) == 0)
        {
            var item = Instantiate(_enemyPrefabs);
            item.transform.position = new Vector3(_enemuLeftPos, Random.Range(-5, 5f), 0);
        }
    }

    private void seenr()
    {
        timerscore = 0;               //���Ԃ����Z�b�g
        //�t�F�[�g�C���A�E�g������X�e�[�W��ʂɔ��
        SceneManager.LoadScene("ShootingGameSeenStage_Mein");
    }

}
