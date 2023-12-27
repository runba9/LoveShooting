using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// "AudioSource"�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�^�b�`
[RequireComponent(typeof(AudioSource))]
public class BGMScripts : MonoBehaviour
{

    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;
    public AudioClip sound04;
    AudioSource audioSource;


    private AudioSource BGMaudioSource; //BGM�p
    void Start()
    {
        // "BGMAudioSource"�R���|�[�l���g���擾
        BGMaudioSource = gameObject.GetComponent<AudioSource>();

        //Component���擾
        audioSource = GetComponent<AudioSource>();

        //�V�[���ړ����Ă��f�[�^�ۑ�����A�葱����
       // DontDestroyOnLoad(this);
    }


    public void OnClickDiscSe1()
    {
        audioSource.PlayOneShot(sound01);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe2()
    {
        audioSource.PlayOneShot(sound02);
        //Debug.Log("click se2");
    }
    public void OnClickDiscSe3()
    {
        audioSource.PlayOneShot(sound03);
        //Debug.Log("click se3");
    }

    public void OnClickDiscSe4()
    {
        audioSource.PlayOneShot(sound04);
        //Debug.Log("click se3");
    }

    public void BGMSoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        BGMaudioSource.volume = newSliderValue;
    }

}