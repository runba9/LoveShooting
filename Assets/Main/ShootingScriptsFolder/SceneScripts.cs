using UnityEngine;

public class SceneScripts : MonoBehaviour
{
    //�^�C�g���V�[���Ɉړ�
    public void Scene_titl()
    {
        // �ۑ�����Ă��邷�ׂẴf�[�^������
        PlayerPrefs.DeleteAll();
        SceneChangr.scenechangrInstance._fade.SceneFade("TitleScene");
    }

    //�Q�[���`���[�g���A���V�[���Ɉړ�
    public void Scene_tutorialGame()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameScene");
    }

    //���U���g�V�[���Ɉړ�
    public void Scene_result()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ResultScene");
    }
    //�Q�[���V�[���Ɉړ�
    public void Scene_MeinGame()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSceneStage_Mein");
    }

    //�Q�[���I��
    public void OnCrickGameEnd()
    {
        // �ۑ�����Ă��邷�ׂẴf�[�^������
        PlayerPrefs.DeleteAll();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
    }

}
