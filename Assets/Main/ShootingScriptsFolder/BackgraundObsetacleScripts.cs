using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgraundObsetacleScripts : MonoBehaviour
{
    [SerializeField]
    public GameObject Up;
    [SerializeField]
    public GameObject Down;

    //�X�N���[���X�s�[�h�����p�����[�^
    [SerializeField]
    private float _ScrollSpeed = 3f;

    //�ړ����E����
    private readonly float _rightLimit = 19.2f;

    void Update()
    {
        //�������ց@Update�@�ł̌o�ߎ��Ԃ��������X�s�[�h������
        Down.transform.position += Vector3.left * (Time.deltaTime * _ScrollSpeed);
        //X���W�����̌��E�𒴂����ꍇ1��ʕ��E�ɉ��Z����
        if (Down.transform.position.x < -_rightLimit)
            Down.transform.position += Vector3.right * _rightLimit;

        if (Down.transform.position.x < -_rightLimit)
        {
            //�������ց@Update�@�ł̌o�ߎ��Ԃ��������X�s�[�h������
            Up.transform.position += Vector3.left * (Time.deltaTime * _ScrollSpeed);
            //X���W�����̌��E�𒴂����ꍇ1��ʕ��E�ɉ��Z����
            if (Up.transform.position.x < -_rightLimit)
                Up.transform.position += Vector3.right * _rightLimit;
        }
    }
}
