using System.Threading;
using UnityEngine;

public class TutorialPanelScripts : MonoBehaviour
{

    [SerializeField] 
    private GameObject TutorialPanel;                              //�J�������p�l���̔�

    public GameObject _demoscript;                                 //Unity��ō����GameObject�ł��閼�OChoicesgameObj������ϐ�
    public static TutorialPanelScripts _tutorialPanelScripts;      //�ǂ��ł��X�N���v�g���Ăяo������
    public void Start()
    {
        _demoscript = GameObject.Find("GameManager");     �@       //Unity��ō����GameManager���擾
    }

    //�I�����U���Ń{�^����I�񂾂�s���@�\�R��

    /// <summary>
    /// �{�X�G�ɃN���e�B�J�������U��
    /// </summary>
    public void CrickButtonMovingTimeForward_Love()
    {
        //����i�߂�
        Time.timeScale = 1;

        //EnemyBoss�X�N���v�g�̒����Ăяo��
        _demoscript.GetComponent<ShootingSceneManagerGameStage>()._enemyBoss.Criticalattack();
        //�p�l�����\��
        TutorialPanel.SetActive(false);

    }

    /// <summary>
    /// �v���C���[�ɍU������@�\�i�������j
    /// </summary>
    public void CrickButtonMovingTimeForward_Bad()
    {
        //����i�߂�
        Time.timeScale = 1;
        //�p�l�����\��
        TutorialPanel.SetActive(false);
    }

    /// <summary>
    /// �������Ȃ�
    /// </summary>
    public void CrickButtonMovingTimeForward_Nomal()
    {
        //����i�߂�
        Time.timeScale = 1;
        //�p�l�����\��
        TutorialPanel.SetActive(false);
    }

}
