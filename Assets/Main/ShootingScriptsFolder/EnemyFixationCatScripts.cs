using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFixationCatScripts : MonoBehaviour
{
    [SerializeField]
    public GameObject _CatEnemy;                  //���̔L�I�u�W�F�����锠
    [SerializeField]
    private GameObject _blackLoveBullet;          //�U���p�̔�           
    [SerializeField]
    private float _Speed = 1f;                    //�e�̃X�s�[�h

    private GameObject SEgameObj;                 //Unity��ō����GameObject�ł��閼�OSE������ϐ�         
    
    void Start()
    {

        SEgameObj = GameObject.Find("SE");//Unity��ō����SE���擾

        //BulletsCat(�e�e����)��3�b�����ɌĂяo��
        InvokeRepeating("BulletsCat", 1, 3);
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
        //�v���C���[���e�e�ɓ���������
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Bullet"))
        {
            //�_���[�W�pSE�Đ�
            SEgameObj.GetComponent<SEScripts>().damageSE();

            //�_���[�W����������I�u�W�F���\��
            _CatEnemy.SetActive(false);
            /// ���񂾂�0.1�b��ɌĂяo�������������Ƃ𕪂���₷�����鉉�o
            Invoke(("CatrevivalOn"), 0.1f);

            //Destroy(gameObject);//�e����
        }
    }

    /// <summary>
    /// ���񂾂�0.1�b��ɌĂяo��
    /// </summary>
    public void CatrevivalOn()
    {
        //�摜��\��
        _CatEnemy.SetActive(true);
    }

}
