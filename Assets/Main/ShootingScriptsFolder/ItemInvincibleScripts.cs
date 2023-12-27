using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvincibleScripts : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 6f;          //�G�l�~�[�̃X�s�[�h
    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    void Update()
    {
        //�G�͍����痈��
        transform.position += Vector3.left * (Time.deltaTime * _Speed);
        //��ʊO�ɏo����G�l�~�[����
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);

    }

    //�v���C���[�ɓ���������擾
    public void OnTriggerEnter2D(Collider2D other)
    {
        //�G�l�~�[�ɓ���������
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);//�e����
        }
    }
}
