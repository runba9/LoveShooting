using UnityEngine;

public class TutorialPanelScripts : MonoBehaviour
{
    [SerializeField] 
    private GameObject TutorialPanel;       //�J�������p�l���̔�

    //public static EnemyBoss demoscript;     //EnemyBoss�X�N���v�g�̒����Ăяo���ׂ̔�
    private GameObject demoscript;         //Unity��ō����GameObject�ł��閼�OChoicesgameObj������ϐ�

    private void Start()
    {
        demoscript = GameObject.Find("GameManager");     //Unity��ō����GameManager���擾
    }

    //�I�����U���Ń{�^����I�񂾂�s���@�\�R��

    /// <summary>
    /// �{�X�G�ɃN���e�B�J�������U��
    /// </summary>
    public void CrickButtonMovingTimeForward_Love()
    {
        //����i�߂�
        Time.timeScale = 1;
        //�p�l�����\��
        TutorialPanel.SetActive(false);
        //EnemyBoss�X�N���v�g�̒����Ăяo��
        demoscript.GetComponent<EnemyBoss>().Criticalattack();
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
