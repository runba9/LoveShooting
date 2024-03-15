using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytracingScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed;                   //�G�l�~�[�̃X�s�[�h
    private bool EnemyThrobbing;            //���񂾂�ǂ�������@�\�������ׂ̔���

    Transform playerTransform;              //�v���C���[�i�ǂ����������Ώہj�̍��WTransform
    [SerializeField]
    private float Acceleration_speed = 5;                    //�v���C���[�̍��W�������Ēǂ�������X�s�[�h
    public EffectScripts _effectEnemy;      //�����G�t�F�N�g�̃v���n�u
    private System.Action _deadCallback;    //���񂾂Ƃ��Ɏ��񂾂��Ƃ�`����
    private GameObject SEgameObj;           //Unity��ō����GameObject�ł��閼�OSE������ϐ�

    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity��ō����SE���擾

        EnemyThrobbing = true;            //�G�l�~�[�����Ă��锻��
    }
    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;//�L�b�N�o�b�N
    }

    void Update()
    {
        //�G�͍����痈��
        transform.position += Vector3.left * (Time.deltaTime * _Speed);

        //�v���C���[�^�O��T�����W�擾������
        GameObject playerObj = GameObject.Find("Player(Clone)");

        // �Q�[���I�u�W�F�N�g�����݂��邩�m�F
        if (playerObj != null)
        {
            //Debug.Log("�I�u�W�F�N�g����������܂����F" + playerObj.name);

            //�G�l�~�[�����Ă锻��
            if(EnemyThrobbing == true)
            {
                //�v���C���[�̍��W�������Ēǂ�������s���J�n
                playerTransform = playerObj.transform;
                // �v���C���[�Ƃ̋�����Acceleration_speed�����ɂȂ�����
                if (Vector2.Distance(this.transform.position, playerTransform.position) <= Acceleration_speed)
                {
                    transform.position = Vector2.Lerp(transform.position, playerTransform.transform.position, _Speed * Time.deltaTime);
                    
                }
                else 
                {
                }
            }


        }
        // �Q�[���I�u�W�F�N�g�����݂��Ȃ������烍�O���o��
        else
        {
            //Debug.LogWarning("�I�u�W�F�N�g���F 'ObjectName'��������܂���");
        }

        //��ʊO�ɏo����G�l�~�[����
        if (transform.position.x < -Screen.width / _screenRatio)
        Destroy(gameObject);
        }

        //�G����
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�e,�v���C���[�A�K�[�h�A�C�e�����G�l�~�[�ɓ���������
            if (collision.gameObject.tag.Equals("Bullet") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Option"))
            {
                //�G�l�~�[���񂾔���
                EnemyThrobbing = false;

                //�_���[�W�pSE�Đ�
                SEgameObj.GetComponent<SEScripts>().damageSE();

                // �e�����������ꏊ�ɔ����G�t�F�N�g�𐶐�����
                Instantiate(_effectEnemy,collision.transform.localPosition,Quaternion.identity);

                _deadCallback?.Invoke();  //�����F�H��_deadCallback��null����Ȃ��Ƃ��Ɋ֐����Ăяo��
   
                //�d�͂�^���ė���������
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;

                //�G����
                //Destroy(gameObject);
        }

    }

    } 
