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

    //ボタンを押したら
    public void VideoCrick()
    {

        //BGMを再生
        BGM.SetActive(true);

        //ビデオ画面閉じる
        Video.SetActive(false);

    }


    //ボタンを押したら
    public void VideoCrickVideo()
    {

        //BGMを再生しない
        BGM.SetActive(false);

        //ビデオ画面開く
        Video.SetActive(true);

    }
}
