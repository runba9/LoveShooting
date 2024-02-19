using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class ShootingSceneManagerGameStage : MonoBehaviour
{
    [SerializeField]
    private GameObject      _enemyPrefabs;            //�G�l�~�[�̃v���n�u
    [SerializeField]
    private GameObject      _enemytracingPrefabs;     //�G�l�~�[�ǐՌ^�̃v���n�u
    [SerializeField]
    private GameObject      _enemyBosPrefabs;         //�G�l�~�[Bos�̃v���n�u
    [SerializeField]
    private GameObject      _ChoicesBulletPrefabs;    //�G�l�~�[Bos�U���p�̃v���n�u
    [SerializeField]
    private GameObject      _EnemyFixationCat;        //�G�l�~�[�t���v���n�u
    [SerializeField]
    public GameObject       _choicesObjPanel;         //�\�����������I�����p�̃p�l����
    [SerializeField]
    private GameObject      _ItemPrefabs;             //�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject      _InvincibleItemPrefabs;   //���G�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject      _playerPrefabs;           //�v���C���[�̃v���n�u
    [SerializeField]
    public PlayerScripts   _playerScripts;            //�v���C���[�X�N���v�g

    Transform playerTransform;                        //�v���C���[�̍��WTransform
    private float _timer = 0;                         //�o�ߎ���
    private float _timerCat = 0;                      //�t���G�l�~�[�p�̌o�ߎ���
    private float _limitTimer = 0;                    //�o������
    private float _streamPosx = 10;                   //�o�����W�̂���X���W�E�Œ�
    public EnemyBoss _enemyBoss;                      //�{�X�G�l�~�[�X�N���v�g���Ăяo��

    //�����_���ɏo������G�l�~�[�̍��W��,���̂ӂ蕝���l
    public float EnemyPosx = -4;
    public float EnemyPosy =  4;


    public static ShootingSceneManagerGameStage _shootingSceneManagerGameStage;//�ǂ��ł��X�N���v�g���Ăяo������
    void Start()
    {
        _limitTimer = Random.Range(0.5f, 2.0f);

        //�ŏ��ɌĂяo���I�u�W�F�B
        Playerrevival();
        EnemyCat();

    }

    void Update()
    {
        //�^�C��
        _timer += Time.deltaTime;     //���Ԃ�ǉ�

        //�\�莞���𒴂�����
        if (_timer >= _limitTimer)
        {
            _timer = 0;               //���Ԃ����Z�b�g
            //�\�莞�Ԃ��Đݒ肷��
            _limitTimer = Random.Range(0.5f, 2.0f);

            //�G�𐶐�
            InstantiateEnemy();       
        }

        //5�b�����ɋt���G�l�~�[�̓G�𐶐�
        _timerCat += Time.deltaTime;     //���Ԃ�ǉ�
        if (_timerCat >= 5)
        {
            _timerCat = 0;               //���Ԃ����Z�b�g

            //�G�𐶐�
            EnemyCat();
        }
    }

    /// <summary>
    /// �v���C���[�c�@�ǉ�����
    /// </summary>
    public void Playerrevival()
    {
        // �v���C���[�𐶐�
        var player = Instantiate(_playerPrefabs);
        //�o�����W�ݒ�
        player.transform.position = new(-7, 0, 7);

        //�v���C���[���G�^�C�����Ăяo��
        _playerScripts = player.GetComponent<PlayerScripts>();
        _playerScripts.GetComponent<PlayerScripts>().InvincibilityTimeBetweenRevivals();      

    }

    /// <summary>
    /// �t���G�l�~�[����
    /// </summary>
    public void EnemyCat()
    {
        //�t���̃G�l�~�[����
        Instantiate(_EnemyFixationCat);
        //�G�͍����痈��
        _EnemyFixationCat.transform.position = new(9, 3.41f, 0);
    }

    //�G�l�~�[�ƃA�C�e�����E���獶�ւƉ�����o�Ă���̂��v���n�u�Ń����_���ɏo��
    private void InstantiateEnemy()
    {
        if (Random.Range(0, 20) == 0)
        {
            var item = Instantiate(_ItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }  //�K�[�h�A�C�e��
        if (Random.Range(0, 10) == 0)
        {
            var item = Instantiate(_InvincibleItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }  //���G�A�C�e��
        if (Random.Range(0, 1) == 0)
        {
            var enemy = Instantiate(_enemyPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }   //�G�l�~�[
        if (Random.Range(0, 5) == 0)
        {
            var enemy = Instantiate(_enemytracingPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }   //�G�l�~�[�ǐՌ^
    }

    /// <summary>
    /// Bos�G�l�~�[���v���n�u�ŏo��
    /// </summary>
    public void BosEnemy()
    {
        // �{�X�𐶐�
        var enemy = Instantiate(_enemyBosPrefabs);
        //�o�����W�ݒ�
        enemy.transform.position = new (_streamPosx, 1, 7);
        _enemyBoss = enemy.GetComponent<EnemyBoss>();
        /*�@����--------
         * 
         * GetComponent�Ō�����ꂸnull�ɂȂ�
         * �����j
         * public EnemyBoss _enemyBoss;�@
         * _enemyBoss = enemy.GetComponent<EnemyBoss>();
         * �X�N���v�g���Ăяo����ݒ肵����
         */
    }

    /// <summary>
    /// Bos�G�l�~�[�̍U�����I�����o��
    /// </summary>
    public void TimeChoicesPanel()
    {
        //�v���C���[�^�O�����邩null�Ŋm�F���Ă���������s
        if (playerTransform == null)
        {
            //�Ăяo��
            TimeChoicesPanel_On();
        }
        else { }

    }

    public void TimeChoicesPanel_On()
    {

        //�p�l���\��
        _choicesObjPanel.SetActive(true);
        //�����~�߂�
        Time.timeScale = 0;

    }

}
