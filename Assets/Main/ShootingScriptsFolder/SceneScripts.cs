using UnityEngine;

public class SceneScripts : MonoBehaviour
{
    //タイトルシーンに移動
    public void Scene_titl()
    {
        // 保存されているすべてのデータを消す
        PlayerPrefs.DeleteAll();
        SceneChangr.scenechangrInstance._fade.SceneFade("TitleScene");
    }

    //ゲームチュートリアルシーンに移動
    public void Scene_tutorialGame()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameScene");
    }

    //リザルトシーンに移動
    public void Scene_result()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ResultScene");
    }
    //ゲームシーンに移動
    public void Scene_MeinGame()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSceneStage_Mein");
    }

    //ゲーム終了
    public void OnCrickGameEnd()
    {
        // 保存されているすべてのデータを消す
        PlayerPrefs.DeleteAll();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
    }

}
