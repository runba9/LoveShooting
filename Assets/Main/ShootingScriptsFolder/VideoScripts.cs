using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScripts : MonoBehaviour
{
    [SerializeField]
    private GameObject BGM;

    [SerializeField]
    private GameObject Video;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�{�^������������
    public void VideoCrick()
    {

        //BGM���Đ�
        BGM.SetActive(true);

        //�r�f�I��ʕ���
        Video.SetActive(false);

    }


    //�{�^������������
    public void VideoCrickVideo()
    {

        //BGM���Đ����Ȃ�
        BGM.SetActive(false);

        //�r�f�I��ʊJ��
        Video.SetActive(true);

    }
}
