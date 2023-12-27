using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 6f;          //�G�l�~�[�̃X�s�[�h
    public EffectScripts _effectEnemy;  // �����G�t�F�N�g�̃v���n�u
    private System.Action _deadCallback;//���񂾂Ƃ��Ɏ��񂾂��Ƃ�`����
    private GameObject SEgameObj;       //Unity��ō����GameObject�ł��閼�OSE������ϐ�
    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity��ō����SE���擾
    }
    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed        = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {
        //�G�͍����痈��
        transform.position += Vector3.left * (Time.deltaTime * _Speed);
        //��ʊO�ɏo����G�l�~�[����
        if(transform.position.x < -Screen.width / _screenRatio)
        Destroy(gameObject);

    }

    //�G����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�e���v���C���[���G�l�~�[�ɓ���������
        if(collision.gameObject.tag.Equals("Bullet") || collision.gameObject.tag.Equals("Player"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();

            // �e�����������ꏊ�ɔ����G�t�F�N�g�𐶐�����
            Instantiate(
                _effectEnemy,
                collision.transform.localPosition,
                Quaternion.identity);

            _deadCallback?.Invoke();  //�����F�H��_deadCallback��null����Ȃ��Ƃ��Ɋ֐����Ăяo��
                                      //�G����
            Destroy(gameObject);
        }

    }


}
