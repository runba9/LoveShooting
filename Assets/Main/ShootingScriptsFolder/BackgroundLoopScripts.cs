using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoopScripts : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_propName = "_MainTex";

    //�����X�s�[�h
    [SerializeField]
    private Vector2 m_offsetSpeed;
    //�����������w�i�}�e���A��
    private Material m_material;

    private void Start()
    {
        if (GetComponent<Image>() is Image i)
        {
            m_material = i.material;
        }
    }

    private void Update()
    {
        if (m_material)
        {
            // x��y�̒l��0 �` 1�Ń��s�[�g����悤�ɂ���
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
            var offset = new Vector2(x, y);
            m_material.SetTextureOffset(k_propName, offset);
        }

        ////�������ց@Update�@�ł̌o�ߎ��Ԃ��������X�s�[�h������
        //transform.position += Vector3.left * (Time.time * movespeed);
        ////X���W�����̌��E�𒴂����ꍇ1��ʕ��E�ɉ��Z����
        //if (transform.position.x < -_rightLimit)
        //    transform.position += Vector3.right * _rightLimit;
    }

    private void OnDestroy()
    {
        // �Q�[������߂���Ƀ}�e���A����Offset��߂��Ă���
        if (m_material)
        {
            m_material.SetTextureOffset(k_propName, Vector2.zero);
        }
    }
}
