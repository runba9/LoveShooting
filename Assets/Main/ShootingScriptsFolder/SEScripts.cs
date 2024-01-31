using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// "AudioSource"�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�^�b�`
[RequireComponent(typeof(AudioSource))]
public class SEScripts : MonoBehaviour
{
    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;
    public AudioClip sound04;

    public AudioClip bulletClip;     //�e�۔��ˎ�
    public AudioClip damageClip;     //�_���[�W���󂯂���
    public AudioClip itemdamageClip; //�A�C�e�����擾������
    public AudioClip playerClip;     //�v���C���[���U�����ꂽ��

    AudioSource audioSource;

    public static SEScripts _seaudioSource;//�ǂ��ł��X�N���v�g���Ăяo������

    private AudioSource SEaudioSource; //SE�p
    void Start()
    {
        // "SEAudioSource"�R���|�[�l���g���擾
        SEaudioSource = gameObject.GetComponent<AudioSource>();

        //Component���擾
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        
        //�V�[���ړ����Ă��f�[�^�ۑ�����A�葱����
        DontDestroyOnLoad(this);
    }


    public void OnClickDiscSe1()
    {
        audioSource.PlayOneShot(sound01);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe2()
    {
        audioSource.PlayOneShot(sound02);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe3()
    {
        audioSource.PlayOneShot(sound03);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe4()
    {
        audioSource.PlayOneShot(sound04);
        //Debug.Log("click se1");
    }
    /// <summary>
    /// �A�C�e���pSE
    /// </summary>
    public void ItemSE()
    {
        audioSource.PlayOneShot(itemdamageClip);
    }
    /// <summary>
    /// �_���[�W�pSE
    /// </summary>
    public void damageSE()
    {
        audioSource.PlayOneShot(damageClip);
    }
    /// <summary>
    /// �e�۔���SE
    /// </summary>
    public void bulletSE()
    {
        audioSource.PlayOneShot(bulletClip);
    }
    /// <summary>
    /// �v���C���[�U����SE
    /// </summary>
    public void playerSE()
    {
        audioSource.PlayOneShot(playerClip);
    }

    public void SESoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        SEaudioSource.volume = newSliderValue;
    }

}