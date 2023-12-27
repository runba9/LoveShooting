using UnityEngine;

public class SeenScripts : MonoBehaviour
{

    //タイトルシーンに移動
    public void Titlestart_button()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("TitleSeen");
    }

    //ゲームシーンに移動
    public void Gamestart_button()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSeen");
    }

    //リザルトシーンに移動
    public void Result_button()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ResultSeen");
    }

    public void NextStage_ShootingGameSeenStage_Mein()
    {
        SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSeenStage_Mein");
    }

    //ゲーム終了
    public void OnCrickGameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
    }

}
