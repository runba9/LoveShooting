using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesBulletView : MonoBehaviour
{
    //�e�̃X�s�[�h
    [SerializeField]
    private float _Speed = 1f;
    private GameObject SEgameObj;       //Unity��ō����GameObject�ł��閼�OSE������ϐ�
    public static ChoicesBulletView _choicesBulletView;   //�ǂ��ł��X�N���v�g���Ăяo������
    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;
    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity��ō����SE���擾
    }
        void Update()
    {
        transform.position -= Vector3.right * (Time.deltaTime * _Speed);

        //��ʍ��[�ɏo����I�u�W�F�N�g�j��
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�e���G�l�~�[�ɓ���������
        if (collision.gameObject.tag.Equals("Player"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().ItemSE();

            Destroy(this.gameObject);     //�G����
        }

    }
}
