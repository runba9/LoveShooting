using UnityEngine;

public class SeenScripts : MonoBehaviour
{

    //�^�C�g���V�[���Ɉړ�
    public void Titlestart_button()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("TitleSeen");
    }

    //�Q�[���V�[���Ɉړ�
    public void Gamestart_button()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSeen");
    }

    //���U���g�V�[���Ɉړ�
    public void Result_button()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ResultSeen");
    }

    public void NextStage_ShootingGameSeenStage_Mein()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSeenStage_Mein");
    }

    //�Q�[���I��
    public void OnCrickGameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
    }

}
