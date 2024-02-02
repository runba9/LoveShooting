using UnityEngine;

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
    public GameObject       _choicesObjPanel;         //�\�����������I�����p�̃p�l����
    [SerializeField]
    private GameObject      _ItemPrefabs;             //�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject      _InvincibleItemPrefabs;   //���G�A�C�e���̃v���n�u
    [SerializeField]
    private GameObject      _playerPrefabs;           //�v���C���[�̃v���n�u
    [SerializeField]
    private PlayerScripts   _playerSCripts;           //�v���C���[�X�N���v�g

    Transform playerTransform;                        //�v���C���[�̍��WTransform
    private float _timer = 0;                         //�o�ߎ���
    private float _limitTimer = 0;                    //�o������
    private float _streamPosx = 10;                   //�o�����W�̂���X���W�E�Œ�
    public static ShootingSceneManagerGameStage _shootingSceneManagerGameStage;//�ǂ��ł��X�N���v�g���Ăяo������
    void Start()
    {
        _limitTimer = Random.Range(0.5f, 2.0f);
        Playerrevival();
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
            InstantiateEnemy();       //�G�𐶐�
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
    }

    //�G�l�~�[�ƃA�C�e�����E���獶�ւƉ�����o�Ă���̂��v���n�u�Ń����_���ɏo��
    private void InstantiateEnemy()
    {
        if (Random.Range(0, 20) == 0)
        {
            var item = Instantiate(_ItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }  //�K�[�h�A�C�e��
        if (Random.Range(0, 10) == 0)
        {
            var item = Instantiate(_InvincibleItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }  //���G�A�C�e��
        if (Random.Range(0, 1) == 0)
        {
            var enemy = Instantiate(_enemyPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }   //�G�l�~�[
        if (Random.Range(0, 5) == 0)
        {
            var enemy = Instantiate(_enemytracingPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
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
        //�����~�߂�
        Time.timeScale = 0;
        //�p�l���\��
        _choicesObjPanel.SetActive(true);
    }

}
