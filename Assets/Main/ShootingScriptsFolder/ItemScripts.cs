using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemScripts : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private float                      _Speed       = 6;      //���x
    private bool                       _isAttached;           //�A�C�e�����������Ă邩�ǂ���
    private int                        _hp          = 2;      //�̗�
    private float                      _angleSpeed  = 7f;     //��]���x
    private System.Action<GameObject>  _attachCallBack;       //�v���C���[�ƂԂ��锻��ƃv���C���[�̎艺�ɂȂ�
    private float                      _angle       = 0;      //�����x��
    private float                     Angle         => _angle;//�O����E����
    private readonly float _barrierLoseCount        = 1;      //�K�[�h�̐�


    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    //������
    public void SetUp(float speed = 6)
    {
        _Speed = speed;
    }


    /// <summary>
    /// �v���C���[�ɃA�^�b�`����
    /// </summary>
    /// <param name="parent">�v���C���[</param>
    /// <param name="angle">�����p�x</param>
    /// <param name="attachCallBack">�ڑ��̃R�[���o�b�N</param>
    public void Attach(Transform parent, float angle, System.Action<GameObject> attachCallBack)
    {
        _attachCallBack = attachCallBack;
        _angle = angle;
        _isAttached = true;
        transform.parent = parent;
        //�A�C�e���ɃA�j���[�V���������s������
        _animator.gameObject.SetActive(true);

    }

    //�p�x�̍Đݒ�
    public void ResetAngle(float angle)
    {
        _angle = angle;
    }
    void Update()
    {
        if (_isAttached) 
        {
            //�A�C�e�����擾���{�ŉE��]������
            _angle += Time.deltaTime * _angleSpeed;
            if(_angle >= Mathf.PI * 2)_angle -= Mathf.PI * 2;
            transform.localPosition = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0);

        }

        else
        {
            //�����痈��
            transform.position += Vector3.left * (Time.deltaTime * _Speed);
            if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);
        }

 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�A�C�e���������Ă��ăG�l�~�[���ǂɓ�����������s
        if(other.gameObject.tag.Equals("Enemy") && _isAttached || other.gameObject.tag.Equals("BulletEnemy") && _isAttached)
        {
            _hp--;
            //�A�C�e���̗̑͂��O�ɂȂ�����I�u�W�F�N�g�j��
            if(_hp <= 0)
            {
                //HP���O�ɂȂ����玀��
                _attachCallBack ? .Invoke(gameObject);
                Destroy(gameObject);
            }
            else if (_hp == _barrierLoseCount)
            {
                //�o���A�̑ϋv�x�ɒB������1�x�����Ăяo���o���A���\��
                _animator.gameObject.SetActive(false);
            }
        }

    }
}
    