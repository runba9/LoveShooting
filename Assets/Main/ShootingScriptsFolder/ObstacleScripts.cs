using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;

public class ObstacleScripts : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float _SpeedObstacle = 3f;  //���̕ǂ̃X�s�[�h
    private bool _mouseCrick = false;   // �}�E�X��������Ă��邩������Ă��Ȃ���
    private Vector3 _mousepos;          // ���݂̃}�E�X�̃��[���h���W
    //��ʂ̉𑜓x�ɑ΂���䗦�ƕ��𔼕��ɂ���K�v�����邽�߁i�[�����Ɓ{�����Œl��ۂj2�{����
    private float _screenRatio = 200f;

    //�}�E�X���N���b�N���ꂽ����u�Ԍ��m���A�R�[���o�b�N�����֐�
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("clicked!");
    }

    //�}�E�X��������Ă���Ԍ��m��
    public void OnPointerDown(PointerEventData eventData)
    {
        // �����J�n�@�t���O�𗧂Ă�
        _mouseCrick = true;
        // �}�E�X�̃��[���h���W��ۑ�
        _mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //�}�E�X�����ꂽ��̊Ԍ��m��
    public void OnPointerUp(PointerEventData eventData)
    {
        // �����I���@�t���O�𗎂Ƃ�
        _mouseCrick = false;
        _mousepos = Vector3.zero;
    }

    void Update()
    {
        Vector3 nowmousepos;
        Vector3 diffposi;

        //�����痈��
        transform.position += Vector3.left * (Time.deltaTime * _SpeedObstacle);
        //��ʊO�ɏo����Ǒދp
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);


        // �}�E�X����������Ă��鎞�A�I�u�W�F�N�g�𓮂���
        if (_mouseCrick)
        {
            // ���݂̃}�E�X�̃��[���h���W���擾
            nowmousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ��O�̃}�E�X���W�Ƃ̍������v�Z���ĕω��ʂ��擾
            diffposi = nowmousepos - _mousepos;
            //�@Y�����̂ݕω�������
            diffposi.x = 0;
            diffposi.z = 0;
            // �J�n���̃I�u�W�F�N�g�̍��W�Ƀ}�E�X�̕ω��ʂ𑫂��ĐV�������W��ݒ�
            GetComponent<Transform>().position += diffposi;
            // ���݂̃}�E�X�̃��[���h���W���X�V
            _mousepos = nowmousepos;
        }
    }

}
