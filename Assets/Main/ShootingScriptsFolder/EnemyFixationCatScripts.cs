using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFixationCatScripts : MonoBehaviour
{
    [SerializeField]
    public GameObject _CatEnemy;                  //���̃I�u�W�F�����锠
    [SerializeField]
    private GameObject _blackLoveBullet;          //�U���p�̔�           
    [SerializeField]
    private float _Speed = 1f;                    //�e�̃X�s�[�h

    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;
    private GameObject SEgameObj;                 //Unity��ō����GameObject�ł��閼�OSE������ϐ�
    void Start()
    {

        SEgameObj = GameObject.Find("SE");//Unity��ō����SE���擾

        //BulletsCat(�e�e����)��3�b�����ɌĂяo��
        InvokeRepeating("BulletsCat", 1, 3);
    }

    private void Update()
    {
        //�G�͍����痈��
        transform.position += Vector3.left * (Time.deltaTime * 6);
        //��ʊO�ɏo����G�l�~�[����
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);

    }

    /// <summary>
    /// �e�e����
    /// </summary>
    public void BulletsCat()
    {
        //�e�𐶐�����
        var bullet = Instantiate(_blackLoveBullet);
        //�e�̈ʒu���
        bullet.transform.position = transform.position + Vector3.right * (Time.deltaTime * _Speed);
    }

    //�e�������ɓ������Ēe����
    public void OnTriggerEnter2D(Collider2D other)
    {
        //�v���C���[���e�e,�K�[�h�A�C�e���ɓ���������
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Bullet") || other.gameObject.tag.Equals("Option"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();

            //�d�͂�^���ė���������
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;

        }
    }

}
