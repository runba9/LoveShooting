using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// "AudioSource"�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�^�b�`
[RequireComponent(typeof(AudioSource))]
public class SEScripts : MonoBehaviour
{
    public AudioClip YesButton;
    public AudioClip NoButton;

    public AudioClip bulletClip;     //�e�۔��ˎ�
    public AudioClip damageClip;     //�_���[�W���󂯂���
    public AudioClip itemdamageClip; //�A�C�e�����擾������
    public AudioClip playerClip;     //�v���C���[���U�����ꂽ��

    private AudioSource audioSource;  //SE�p

    public static SEScripts _seaudioSource;//�ǂ��ł��X�N���v�g���Ăяo������


    private void Awake()
    {
        // ����������2�񂵂Ȃ��悤�ɂ���
        if (_seaudioSource == null)
        {
            // �ϐ��̒��ɃX�N���v�g���i�[
            _seaudioSource = GetComponent<SEScripts>();
            _seaudioSource = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // "SEAudioSource"�R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
        _seaudioSource = GetComponent<SEScripts>();

    }

    private void Update()
    {

        //�V�[���ړ����Ă��f�[�^�ۑ�����A�葱����
        DontDestroyOnLoad(this);
    }


    public void OnClickDiscSe1()
    {
        audioSource.PlayOneShot(YesButton);
        //Debug.Log("click se1");
    }
    public void OnClickDiscSe2()
    {
        audioSource.PlayOneShot(NoButton);
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
        audioSource.volume = newSliderValue;
    }

}