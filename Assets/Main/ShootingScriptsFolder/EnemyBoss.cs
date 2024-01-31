using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    public GameObject _enemy;                       //���̃{�X�I�u�W�F�����锠
    [SerializeField]
    private float _Speed = 3f;                      //�G�l�~�[�̃X�s�[�h
    [SerializeField]
    private int _EnemyBosshp = 30;                  //�{�X�̗̑�
    private int Critical;                           //�N���e�B�J���U������
    private int Damage;                             //�v���C���[�̒ʏ�U��
    private bool criticalCount = false;             //�N���e�B�J���U�������̗L��
    public bool _criticalconfession = false;

    public GameObject[] _choicesBullet;             //�U���̑I�����ƃ_�~�[�������_���ŏo���ׂ̂̃��X�g

    private Vector2 pos;                            //���W
    public int _enemyBoss = 1;                      //�G�l�~�[�̐�
    public EffectScripts _effectEnemy;              //�����G�t�F�N�g�̃v���n�u
    private System.Action _deadCallback;            //���񂾂Ƃ��Ɏ��񂾂��Ƃ�`����
    private GameObject SEgameObj;                   //Unity��ō����GameObject�ł��閼�OSE������ϐ�

    public static EnemyBoss _enemyBossScripts;        //�ǂ��ł��X�N���v�g���Ăяo������
    void Start()
    {
        SEgameObj = GameObject.Find("SE");          //Unity��ō����SE���擾

        //5�b��InputiateBullet()���Ăяo���e�𔭎˂�����
        InvokeRepeating("InputiateChoicesbullet", 1, 5);
    }
    /// <summary>
    /// �e���ˑ��u
    /// </summary>
    public void InputiateChoicesbullet()
    {
        //�A�C�e���pSE�Đ�
        SEgameObj.GetComponent<SEScripts>().bulletSE();

        //���X�g���������̂������_���ŏo�͂��Ă����A���̃����̊ȈՃo�[�W����
        var Choicesbullet = Random.Range(0, _choicesBullet.Length);
        Instantiate(_choicesBullet[Choicesbullet], transform.position, transform.rotation);

        //////����(����͂����̋L�O�Ŏc���Ă邾���ł�)
        ////�I����
        //if (Random.Range(0, 1) == 0)
        //{
        //    //�e�𐶐�����
        //    var Choicesbullet = Instantiate(_choicesBullet);
        //    //�e�̈ʒu���
        //    Choicesbullet.transform.position = transform.position;
        //}
        ////�_�~�[�I����
        //if (Random.Range(0, 1) == 0)
        //{
        //    //�e�𐶐�����
        //    var Choicesbullet = Instantiate(_choicesNoBullet);
        //    //�e�̈ʒu���
        //    Choicesbullet.transform.position = transform.position;
        //}

    }

    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {
        //���݂̃{�X��hp
        Debug.Log("����hp" + _EnemyBosshp);
        //�{�X�̗̑͂��O���O�𒴂�����I�u�W�F�N�g�j��
        if (_EnemyBosshp <= 0)
        {
            Deadboss();
        }

        pos = transform.position;

        //���E�ړ�
        transform.Translate(transform.right * Time.deltaTime * _Speed * _enemyBoss);
        if (pos.x > 7)//��
        {
            _enemyBoss = -1;//����
        }
        if (pos.x < 6)
        {
            _enemyBoss = 1;
        }

    }

    //�G����
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //�e���G�l�~�[�ɓ���������
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();
            // �{�X�������Ă鎞�̏���
            Conventionalattack();
        }
    }

    /// <summary>
    /// �ʏ�U��
    /// </summary>
    public void Conventionalattack()
    {

        //�_���[�W����������I�u�W�F�������Ȃ�������
        //_enemy.SetActive(false);

        //�ʏ�U��
        Damage = 1;

        //�t���O��true�Ȃ�N���e�B�J���U���������N��������
        if (criticalCount == true)
        {
            //�N���e�B�J���_���[�W
            Critical = 10;

            //�v�Z����
            _EnemyBosshp -= Critical;
            Debug.Log("�N���e�B�J��hp" + _EnemyBosshp);
            //�S�ďI�������t���O��߂�
            criticalCount = false;
        }

        //�v�Z����
        _EnemyBosshp -= Damage;
        Debug.Log("�v�Z������hp" + _EnemyBosshp);
        //�_���[�W����������I�u�W�F�������Ȃ�����0.1��ɕ���
        Invoke(("EnemyrevivalOn"), 0.1f);

    }

    /// <summary>
    /// �N���e�B�J�������U��
    /// </summary>
    public void Criticalattack()
    {
        //�N���e�B�J�������U����true�ɂ���
        criticalCount = true;
        //�U���Ăяo��
        Conventionalattack();
    }

    /// <summary>
    /// �������I�u�W�F��0.1�b��ɌĂяo�����o
    /// </summary>
    public void EnemyrevivalOn()
    {
        //_enemy.SetActive(true);
    }

    /// <summary>
    /// �{�X�̎��񂾏���
    /// </summary>
    public void Deadboss()
    {
        // �e�����������ꏊ�ɔ����G�t�F�N�g�𐶐�����
        Instantiate(
            _effectEnemy,
            transform.localPosition,
            Quaternion.identity);

        //HP���O�ɂȂ����玀��
        _deadCallback?.Invoke();  //�����F�H��_deadCallback��null����Ȃ��Ƃ��Ɋ֐����Ăяo��
        Destroy(gameObject);     //�G����
                                 //�t�F�[�g�C���A�E�g�����ナ�U���g��ʂɔ��
        SceneChangr.scenechangrInstance._fade.SceneFade("ResultSeen");
    }

}
