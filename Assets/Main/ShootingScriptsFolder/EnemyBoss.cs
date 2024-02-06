using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    public GameObject _enemy;                       //���̃{�X�I�u�W�F�����锠
    private SpriteRenderer _enemyboss;              //�{�X�̃X�v���C�g�����_���[�p�̔�
    [SerializeField]
    private float _Speed = 3f;                      //�G�l�~�[�̃X�s�[�h
    [SerializeField]
    private int _EnemyBosshp = 100;                 //�{�X�̗̑�
    private int Critical;                           //�N���e�B�J���U������
    private int Damage;                             //�v���C���[�̒ʏ�U��
    private bool criticalCount = false;             //�N���e�B�J���U�������̗L��
    private Slider BossHp_slider;                   //�{�X��hp���������邽�߂ɃX���C�_�[�ŕ\��
                                                    
    private Animator animator;                      //�A�j���[�V����
    public GameObject[] _choicesBullet;             //�U���̑I�����ƃ_�~�[�������_���ŏo���ׂ̂̃��X�g

    private Vector2 pos;                            //���W
    public int _enemyBoss = 1;                      //�G�l�~�[�̐�
    public EffectScripts _effectEnemy;              //�����G�t�F�N�g�̃v���n�u
    private System.Action _deadCallback;            //���񂾂Ƃ��Ɏ��񂾂��Ƃ�`����
    private GameObject SEgameObj;                   //Unity��ō����GameObject�ł��閼�OSE������ϐ�

    public static EnemyBoss _enemyBossScripts;      //�ǂ��ł��X�N���v�g���Ăяo������
    void Start()
    {
        SEgameObj           = GameObject.Find("SE");                                      //Unity��ō����SE���擾
        BossHp_slider       = GameObject.Find("BossHpSlider").GetComponent<Slider>();     //Unity��ō����BossHpSlider���擾
        _enemyboss          = GetComponent<SpriteRenderer>(); //�{�X�̃X�v���C�g�����_���[�擾
        animator = GetComponent<Animator>();                �@//�A�j���[�V�����擾
        //5�b���o�ŊJ���ČĂяo���N���e�B�J���U���e�𔭎˂�����
        InvokeRepeating("InputiateChoicesbullet", 1.0f, 5.0f);
    }       

    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {

        //���݂̃{�X��hp
        //Debug.Log("����hp" + �ő�hp);
        //�{�X�������̃_���[�W��H�������̏���
        if (_EnemyBosshp <= 50 )
        {
            animator.SetBool("BossLoveMotion", true);
        }
        //�{�X�̗̑͂��O���O�𒴂�����I�u�W�F�N�g�j��
        if (_EnemyBosshp <= 0 || _EnemyBosshp <= -1)
        {
            StartCoroutine(Deadboss());
        }

        //�{�X�̈ړ��͈́i���E�ړ��j
        pos = transform.position;
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

    /// <summary>
    /// �N���e�B�J���U���e���ˑ��u
    /// </summary>
    private void InputiateChoicesbullet()
    {
        //�A�C�e���pSE�Đ�
        SEgameObj.GetComponent<SEScripts>().bulletSE();

        //���X�g���������̂������_���ŏo�͂��Ă����A���̃����̊ȈՃo�[�W����
        var Choicesbullet = Random.Range(0, _choicesBullet.Length);
        Instantiate(_choicesBullet[Choicesbullet], transform.position, transform.rotation);
        /*����----------
           �@�ȈՃo�[�W��������ꂽ�̂������������̂Ń����Ɏc���Ă܂�
        //�I����
        if (Random.Range(0, 1) == 0)
        {
            //�e�𐶐�����
            var Choicesbullet = Instantiate(_choicesBullet);
            //�e�̈ʒu���
            Choicesbullet.transform.position = transform.position;
        }
        //�_�~�[�I����
        if (Random.Range(0, 1) == 0)
        {
            //�e�𐶐�����
            var Choicesbullet = Instantiate(_choicesNoBullet);
            //�e�̈ʒu���
            Choicesbullet.transform.position = transform.position;
        }
        */

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

        // HP�Q�[�W�ɒl��ݒ�
        BossHp_slider.value = _EnemyBosshp;

        //�_���[�W����������I�u�W�F�������Ȃ�����0.1��ɕ���
        StartCoroutine(EnemyrevivalOn());
        /*����-------
        Invoke(("EnemyrevivalOn"), 0.1f);�ŌĂяo���Ă�����
        �R���[�`���̕������|�I�Ɏg���₷��
        */
    }

    /// <summary>
    /// �N���e�B�J�������U��
    /// </summary>
    public void Criticalattack()
    {
        Debug.Log("�Ăяo��OK");
        //�N���e�B�J�������U����true�ɂ���
        criticalCount = true;
        //�U���Ăяo��
        Conventionalattack();
    }


    /// <summary>
    /// �U�������I�u�W�F��0.1�b��ɌĂяo�����o�R���[�`��
    /// </summary>
    private IEnumerator EnemyrevivalOn()
    {
        //�_���[�W����������I�u�W�F�������Ȃ�������
        _enemyboss.enabled = false;
        yield return new WaitForSeconds(0.1f); // 0.1�b�ԑҋ@
        _enemyboss.enabled = true;
        /*����----------------
         _enemy.SetActive(true);�ŏ����ƑS��������̂ŉ摜������
        �X�v���C�g�����_���[�̂�on�Aoff���������֗�
         */

    }

    /// <summary>
    /// �{�X�̎��񂾏���
    /// </summary>
    private IEnumerator Deadboss()
    {
        yield return new WaitForSeconds(1);

        // �e�����������ꏊ�ɔ����G�t�F�N�g�𐶐�����
        Instantiate(_effectEnemy,transform.localPosition,Quaternion.identity);

        //HP���O�ɂȂ����玀��
        _deadCallback?.Invoke();        //�����F�H��_deadCallback��null����Ȃ��Ƃ��Ɋ֐����Ăяo��
        _enemyboss.enabled = false;     //�G����

        yield return new WaitForSeconds(3);

        //�t�F�[�g�C���A�E�g�����ナ�U���g��ʂɔ��
        SceneManager.LoadScene("ResultScene");
        //SceneChangr.scenechangrInstance._fade.SceneFade("ResultScene");

    }

}
