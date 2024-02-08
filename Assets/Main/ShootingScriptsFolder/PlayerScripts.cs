using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayerScripts : MonoBehaviour
{
    //�v���C���[
    [SerializeField]
    private float _PlayerSpeed = 0.6f;               //�v���C���[�̃X�s�[�h
    [SerializeField]
    private SpriteRenderer _playerSprite;

    //�v���C���[�̏e�e
    [SerializeField]
    private GameObject _PlayerBulletObject;          //�e�e�̃v���n�u������
    [SerializeField]
    private float _posRealignment = 0.2f;            //�v���C���[�̏e�e�̈ʒu����

    //�v���C���[�A�j���[�V����
    private Animator animator;

    //���̓V�X�e��(InputSystem)
    private Distortion_Game _gameInputsSystem;
    //���̑ł��o���ʒu�̏����ݒ�
    private readonly float _bulletShotOffset = 6f;

    //�v���C���[�̎�������o���A�̍ő吔
    private readonly int _attachCountMax = 6;
    //�v���C���[�̎�������o���A�̃��X�g
    private List<GameObject> _attachObjects = new List<GameObject>();

    //�v���C���[�s������(��ʊO�ɍs���Ȃ�������)
    // x�������̈ړ��͈͂̍ŏ��l
    [SerializeField] private float _minX = -9;
    // x�������̈ړ��͈͂̍ő�l
    [SerializeField] private float _maxX = 9.45f;
    // y�������̈ړ��͈͂̍ŏ��l
    [SerializeField] private float _minY = -4.2f;
    // y�������̈ړ��͈͂̍ő�l
    [SerializeField] private float _maxY = 3.78f;

    //���G�A�C�e��
    [SerializeField]
    public float flashInterval;                       //�t���b�V�������
    [SerializeField]
    public int loopCount;                             //�_�ł�����Ƃ��̃��[�v�J�E���g(����_�ł����邩)

    public bool ItemInvincibleisGetHit;               //���G�A�C�e���擾���Ă�̂��̔���
    SpriteRenderer flashing;                          //�_�ł����邽�߂�SpriteRenderer

    //�v���C���[�̎c�@�Ăяo��
    private GameObject PlayerRemainingLivesObj;//Unity��ō����GameObject�ł��閼�OPlayerRemainingLivesObj������ϐ�
    //SE�Ăяo��
    private GameObject SEgameObj;              //Unity��ō����GameObject�ł��閼�OSE������ϐ�
    private GameObject gameObjScore;           //Unity��ō����GameObject�ł��閼�OgameObjScore������ϐ�
    //�{�X�̍U���i�I�����U���j�Ăяo��
    private GameObject ChoicesgameObj;         //Unity��ō����GameObject�ł��閼�OChoicesgameObj������ϐ�
    void Start()
    {
        animator = GetComponent<Animator>();

        //�C���v�b�g�V�X�e����p�ӂ��ėL��������
        _gameInputsSystem = new Distortion_Game();
        _gameInputsSystem.Enable();

        SEgameObj               = GameObject.Find("SE");              //Unity��ō����SE���擾
        ChoicesgameObj          = GameObject.Find("GameManager");     //Unity��ō����GameManager���擾
        PlayerRemainingLivesObj = GameObject.Find("GameManager");     //Unity��ō����GameManager���擾
        gameObjScore            = GameObject.Find("GameManager");     //Unity��ō����GameManager���擾

        flashing                = GetComponent<SpriteRenderer>();     //�X�v���C�g�����_���[���擾������


    }

    void Update()
    {
        //�v���C���[�̋@�\�Ăяo��
        Player();

        //���N���b�N�������Ă�����e�𔭎˂���̂��Ăяo��
        if (_gameInputsSystem.Player.Fire.triggered) InputiateBullet();

    }

    /// <summary>
    /// �v���C���[�s��
    /// </summary>
    public void Player()
    {
        //�v���C���[
        //�ړ��������擾
        var movePos = _gameInputsSystem.Player.Move.ReadValue<Vector2>();
        //�ړ������ɂ��ړ����x���o�Ȃ��悤�ɑ��x�����ɂ����normalized�ɂ���
        movePos = movePos.normalized;
        transform.localPosition += (Vector3)movePos * (Time.deltaTime * _PlayerSpeed);

        ////�ړ��͈͐���
        var PlayerPos = transform.position;
        // x�������̈ړ��͈͐���
        PlayerPos.x = Mathf.Clamp(PlayerPos.x, _minX, _maxX);
        // y�������̈ړ��͈͐���
        PlayerPos.y = Mathf.Clamp(PlayerPos.y, _minY, _maxY);
        transform.position = PlayerPos;
    }

    /// <summary>
    /// �e���ˑ��u
    /// </summary>
    public void InputiateBullet()
    {
        //�A�C�e���pSE�Đ�
        SEgameObj.GetComponent<SEScripts>().bulletSE();

        //�ˌ��A�j���[�V����
        animator.SetTrigger("PlayerLoveBullet");

        //�e�𐶐�����
        var bullet = Instantiate(_PlayerBulletObject);
        //�e�̈ʒu���
        bullet.transform.position = transform.position + Vector3.right * _bulletShotOffset * _posRealignment;
    }

    //�����蔻��
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�A�C�e��
        //  ���������I�u�W�F�N�g���I�v�V�����A�C�e��(�K�[�h)�Ȃ��
        if (other.gameObject.tag.Equals("Option"))
        {
            //�A�C�e���pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().ItemSE();

            //  ���łɃI�v�V������ MAX�܂ŕt���Ă���Ȃ牽�����Ȃ�
            if (_attachObjects.Count >= _attachCountMax) return;

            _attachObjects.Add(other.gameObject);

            var itemScripts = other.GetComponent<ItemScripts>();
            //�A�^�b�`���āA���񂾂�BrokenDefender�̏�����
            itemScripts.Attach(transform, 0, BrokenDefender);
            SetItmePosition();

        }
        //  ���G�A�C�e��
        if (other.gameObject.tag.Equals("Invincible"))
        {
            //�A�C�e���pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().ItemSE();

            //���G�A�C�e�������Ɏ擾���Ă����牽�����Ȃ�
            if (ItemInvincibleisGetHit)
            {
                return;
            }
            //ItemInvincibl(���G�A�C�e��)�R���[�`���Ăяo��
            StartCoroutine(_ItemInvincible());
        }

        //�G�l�~�[
        //  ���������I�u�W�F�N�g���{�X����̍U��(�I�����U��)�Ȃ��
        if (other.gameObject.tag.Equals("ChoicesBullet"))
        {
            //�A�C�e���pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().ItemSE();
            ChoicesgameObj.GetComponent<ShootingSceneManagerGameStage>().TimeChoicesPanel();
        }
        //  ���������I�u�W�F�N�g���G�l�~�[
        if (other.gameObject.tag.Equals("Enemy"))
        {

            gameObjScore.GetComponent<ScoreScripts>().RefreshScoreText();//RefreshScoreText()�����s���ĉ��_

            //���G�A�C�e�������Ɏ擾���Ă����玀�ȂȂ�
            if (ItemInvincibleisGetHit)
            {
                return;
            }

            //�v���C���[����
            Destroy(gameObject);
            //�v���C���[�̎c�@�����炷
            PlayerRemainingLivesObj.GetComponent<ScoreScripts>().PlayerRemainingLivesText();

        }
        // ���������̂���Q���Ȃ��
        if (other.gameObject.tag.Equals("Wall"))
        {        
            //�_���[�W���R���[�`���N��
            StartCoroutine(PlayerevivalOn());

            //�v���C���[����
            Destroy(gameObject);
            //�v���C���[�̎c�@�����炷
            PlayerRemainingLivesObj.GetComponent<ScoreScripts>().PlayerRemainingLivesText();

        }
        //  ���������I�u�W�F�N�g���`���[�g���A���p�G�l�~�[�Ȃ��
        if (other.gameObject.tag.Equals("Ring"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();
        }
        
    }

    /// <summary>
    /// �v���C���[���o
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerevivalOn()
    {
        //�_���[�W����������I�u�W�F�������Ȃ�������
        _playerSprite.enabled = false;
        yield return new WaitForSeconds(0.1f); // 0.1�b�ԑҋ@
        _playerSprite.enabled = true;

        //0.5�b�x������
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.1f); // 0.1�b�ԑҋ@

        //���Ԗ߂�
        Time.timeScale = 1f;

    }


    /// <summary>
    /// ���G�A�C�e������(�R���[�`��)
    /// </summary>
    public IEnumerator _ItemInvincible()
    {
        //������t���O��true�ɕύX�i�������Ă����ԁj
        ItemInvincibleisGetHit = true;
        //Debug.Log("�擾");
        //�_�Ń��[�v�J�n
        for (int i = 0; i < loopCount; i++)
        {
            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer(�_��)���I�t
            flashing.enabled = false;
            
            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer(�_��)���I��
            flashing.enabled = true;
            //Debug.Log("���[�v��");
        }

        //�_�Ń��[�v���������瓖����t���O��false(�������ĂȂ����)
        ItemInvincibleisGetHit = false;
        //Debug.Log("�I�����");
    }

    /// <summary>
    /// �A�C�e��(�K�[�h)���~��ɋϓ��ɔz�u����
    /// </summary>
    private void SetItmePosition()
    {
        //  �e�I�v�V�����̊Ԃ̊p�x�i���W�A���j
        var addAngle = Mathf.PI * 2 / _attachObjects.Count;
        //  �I�v�V�����̗v�f�����Ԃɏ�������
        foreach (var (itemScripts, index) in _attachObjects.Select((obj, index) => (obj.GetComponent<ItemScripts>(), index)))
        {
            //  ����̏ꏊ�ɐݒ�
            itemScripts.ResetAngle(addAngle * index);
        }
    }

    /// <summary>
    /// �j�󂳂ꂽ�Q�[���I�u�W�F�N�g(�A�C�e��)�����X�g����폜����
    /// </summary>
    /// <param name="gobj"></param>
    private void BrokenDefender(GameObject gobj)
    {
        _attachObjects.Remove(gobj);
    }
}
