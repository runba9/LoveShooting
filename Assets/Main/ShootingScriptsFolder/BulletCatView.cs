using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCatView : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 1f;
    //[SerializeField]
    //public GameObject playerTrans;         // �v���C���[��Transform
    public EffectScripts _effectEnemy;  // �����G�t�F�N�g�̃v���n�u
    private System.Action _deadCallback;//���񂾂Ƃ��Ɏ��񂾂��Ƃ�`����
    private GameObject SEgameObj;       //Unity��ō����GameObject�ł��閼�OSE������ϐ�

    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;
    Transform playerTr;                 // �v���C���[��Transform

    void Start()
    {
        SEgameObj = GameObject.Find("SE"); //Unity��ō����SE���擾

    }

    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {
        //�e���
        transform.position -= Vector3.up * (Time.deltaTime * _Speed);

        //��ʉ��[�ɏo����I�u�W�F�N�g�j��
        if (transform.position.y < -Screen.height / _screenRatio)
            Destroy(gameObject);

    }

    //�e�������ɓ������Ēe����
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�ɓ���������
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Bullet"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();

            // �e�����������ꏊ�ɔ����G�t�F�N�g�𐶐�����
            Instantiate(
                _effectEnemy,
                collision.transform.localPosition,
                Quaternion.identity);

            _deadCallback?.Invoke();  //�����F�H��_deadCallback��null����Ȃ��Ƃ��Ɋ֐����Ăяo��
            Destroy(gameObject);//�e����
        }
    }
}
