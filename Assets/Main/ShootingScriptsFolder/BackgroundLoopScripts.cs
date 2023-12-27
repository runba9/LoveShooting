using UnityEngine;

public class BackgroundLoopScripts : MonoBehaviour
{
    //�X�N���[���X�s�[�h�����p�����[�^
    [SerializeField]
    private float _ScrollSpeed = 3f;
    //�ړ����E����
    public readonly float _rightLimit = 19.2f;
    void Update()
    {
        //�������ց@Update�@�ł̌o�ߎ��Ԃ��������X�s�[�h������
        transform.position += Vector3.left * (Time.deltaTime * _ScrollSpeed);
        //X���W�����̌��E�𒴂����ꍇ1��ʕ��E�ɉ��Z����
        if (transform.position.x < - _rightLimit)
            transform.position += Vector3.right * _rightLimit;
    }
}
