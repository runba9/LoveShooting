using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangr : MonoBehaviour
{
    // �N���X�̎擾
    [SerializeField]
    public Fade _fade;

    // �ǂ�����ł��ꕶ�ŌĂׂ�悤�ɂ���B 
    public static SceneChangr scenechangrInstance;
 
    private void Awake()
    {
        // ����������2�񂵂Ȃ��悤�ɂ���
        if(scenechangrInstance == null)
        {
            // �ϐ��̒��ɃX�N���v�g���i�[
            scenechangrInstance = GetComponent<SceneChangr>();
            scenechangrInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

// public static�����C��
// static�͂ǂ�����ł������Ă����
