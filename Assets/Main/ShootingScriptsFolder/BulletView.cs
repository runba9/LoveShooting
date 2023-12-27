using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BulletView : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 0.1f;
    private GameObject gameObjScore; //Unity��ō����GameObject�ł��閼�OGameManager������ϐ�

    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    void Start()
    {
        gameObjScore = GameObject.Find("GameManager");//Unity��ō����GameManager���擾
    }
    void Update()
    {
        transform.position += Vector3.right * (Time.deltaTime * _Speed);
        //��ʂ̉E�[�ɏo���̂��̔���A�o����I�u�W�F�N�g����
        if (transform.position.x > Screen.width / _screenRatio)
            Destroy(gameObject);
    }

    //�e�������ɓ������Ēe����
    public void OnTriggerEnter2D(Collider2D other)
    {
        //�G�l�~�[�ɓ���������
        if (other.gameObject.tag.Equals("Enemy"))
        {
            gameObjScore.GetComponent<ScoreScripts>().RefreshScoreText();//RefreshScoreText()�����s���ĉ��_
            Destroy(gameObject);//�e����
        }

        //�`���[�g���A���p�G�l�~�[�ɓ���������
        if (other.gameObject.tag.Equals("Ring"))
        {
            Destroy(gameObject);//�e����
        }

    }

}
