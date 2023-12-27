using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 0.1f;
    private GameObject SEgameObj;       //Unity��ō����GameObject�ł��閼�OSE������ϐ�
    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    void Start()
    {
        SEgameObj = GameObject.Find("SE");//Unity��ō����SE���擾
    }
    void Update()
    {
        //�e����
        transform.position -= Vector3.right * (Time.deltaTime * _Speed);
        //��ʍ��[�ɏo����I�u�W�F�N�g�j��
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);
    }

    //�e�������ɓ������Ēe����
    public void OnTriggerEnter2D(Collider2D other)
    {
        //�v���C���[�ɓ���������
        if (other.gameObject.tag.Equals("Player") ||  other.gameObject.tag.Equals("Bullet"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();
            Destroy(gameObject);//�e����
        }
    }
}
