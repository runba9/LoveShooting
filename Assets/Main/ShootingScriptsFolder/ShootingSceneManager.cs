using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ShootingSceneManager : MonoBehaviour
{
    //エネミーのプレハブ
    [SerializeField]
    private GameObject _enemyPrefabs;
    //アイテムのプレハブ
    [SerializeField]
    private GameObject _ItemPrefabs;
    //プレイヤーのプレハブ
    [SerializeField]
    private PlayerScripts _PlayerPrefabs;
    //スコアプレハブ
    [SerializeField]
    public TextMeshProUGUI textScore;        //スコアテキスト
    private float timerscore = 20;           //経過時間

    private float _timer        = 0; //経過時間
    private float _limitTimer   = 0; //出現時間
    private float _enemuLeftPos = 10;//敵の出現座標のうちX座標右固定

    void Start()
    {
        _limitTimer = Random.Range(0.5f, 2.0f);
    }

    void Update()
    {
        //1番上のtext表示
        RefreshScoreText();

        //20秒から少しずつ減って０になったら
        timerscore -= Time.deltaTime;
        if (timerscore <= 0)
        {

            seenr();//シーン移動
        }


        _timer += Time.deltaTime;     //時間を追加
            //予定時刻を超えたら
        if(_timer >= _limitTimer)
        {
            _timer = 0;               //時間をリセット
            //予定時間を再設定する
            _limitTimer = Random.Range(0.5f, 2.0f);
            InstantiateEnemy();       //敵を生成
        }
    }

    //スコアテキスト更新
    public void RefreshScoreText()
    {
        // オブジェクトからTextコンポーネントを取得
        Text score_text = textScore.GetComponent<Text>();
        //画面に貰った数値を表示
        textScore.text = "TutorialTime : " + timerscore.ToString("00");//ToString("00")＝("F１")同じ、F２で小数点追加など出来る
    }

    //エネミーとアイテムが右から左へと横から出てくるのをプレハブでランダムに出す
    private void InstantiateEnemy()
    {
        //アイテム
        if(Random.Range(0, 4) == 0)
        {
            var enemy = Instantiate(_ItemPrefabs);
            enemy.transform.position = new Vector3(_enemuLeftPos, Random.Range(-5, 5f), 0);
        }
        //エネミー
        if (Random.Range(0, 2) == 0)
        {
            var item = Instantiate(_enemyPrefabs);
            item.transform.position = new Vector3(_enemuLeftPos, Random.Range(-5, 5f), 0);
        }
    }

    private void seenr()
    {
        timerscore = 0;               //時間をリセット
        //フェートインアウト処理後ステージ画面に飛ぶ
        SceneManager.LoadScene("ShootingGameSeenStage_Mein");
    }

}
