using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShootingSceneManager : MonoBehaviour
{
    //エネミーのプレハブ
    [SerializeField]
    private GameObject _enemyPrefabs;
    //アイテムのプレハブ
    [SerializeField]
    private GameObject _ItemPrefabs;
    //アイテムのプレハブ
    [SerializeField]
    private GameObject _PlayerPrefabs;
    //プレイヤーのプレハブ
    [SerializeField]
    private PlayerScripts _PlayerPrefabsScripts;
    //スコアプレハブ
    [SerializeField]
    public TextMeshProUGUI textScore;               //スコアテキスト

    //説明書のリスト
    [SerializeField]
    private List<Sprite> _instructionManualList;    //変えたい画像のリスト(説明書)
    public float _instructionManualListNumber = 2;  //説明書の数
    private int _instructionManua = 0;              //説明書をめくったページの個数

    //表示したいパネルの箱（操作説明書）
    [SerializeField]
    public GameObject _instructionManuaPanel;       //表示したい画像達
    public Image ExplainPoint;                      //表示したい画像達を設置する
    public TMP_Text _nextText;                      //次へボタン

    //表示したいパネルの箱（実機説明書）
    [SerializeField]
    public GameObject TryEnemy;                     //チュートリアル後に表れる説明テキスト

    public bool Preface;                            //操作説明が終わったかどうか

    //スキップボタン
    [SerializeField]
    public GameObject SkipButton;

    //カウントダウン
    [SerializeField]
    private float timerscore = 20;           //経過時間

    private float _timer = 0;                //経過時間
    private float _limitTimer = 0;           //出現時間
    private float _enemuLeftPos = 10;        //敵の出現座標のうちX座標右固定

    void Start()
    {
        //操作説明が終わったらfalseにしていく
        Preface = true;

        //チュートリアル説明
        StartCoroutine(Tutorial());
    }

    void Update()
    {
        //1番上のtext表示
        RefreshScoreText();

        //操作説明が終わったら
        if (Preface == false)
        {
            _limitTimer = Random.Range(0.5f, 2.0f);

            _timer += Time.deltaTime;     //時間を追加
                                          //予定時刻を超えたら
            if (_timer >= _limitTimer)
            {
                _timer = 0;               //時間をリセット
                                          //予定時間を再設定する
                _limitTimer = Random.Range(0.5f, 2.0f);
                InstantiateEnemy();       //敵を生成

                //画像表示
                TryEnemy.SetActive(true);
            }
        }


        //20秒から少しずつ減って０になったら
        timerscore -= Time.deltaTime;
        if (timerscore <= 0)
        {
            StartCoroutine(MovingScene());//シーン移動
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

    /// <summary>
    /// チュートリアル序章(操作説明)
    /// </summary>
    /// <returns></returns>
    private IEnumerator Tutorial()
    {
        // 1秒待機
        yield return new WaitForSeconds(1);

        //時を止める
        Time.timeScale = 0;

        //説明書を表示
        ExplainPoint.sprite = _instructionManualList[_instructionManua];
        _instructionManuaPanel.SetActive(true);

    }

    //エネミーとアイテムが右から左へ横からプレハブでランダムに出す
    private void InstantiateEnemy()
    {
        //エネミー
        if (Random.Range(0, 2) == 0)
        {
            var item = Instantiate(_enemyPrefabs);
            item.transform.position = new Vector3(_enemuLeftPos, Random.Range(-5, 5f), 0);
        }
    }



    //説明書を次のページに、ループさせる
    public void OnButtonExplainNext()
    {
        if (_instructionManua == _instructionManualListNumber)
        {
            //読み終わったら閉じる
            _instructionManuaPanel.SetActive(false);
            //操作説明終わり
            Preface = false;
            //時を進める
            Time.timeScale = 1;
            //スキップボタン表示
            SkipButton.SetActive(true);
            PlayerAdmission();
            return;
        }
        _instructionManua++;
        ExplainPoint.sprite = _instructionManualList[_instructionManua];

        if (_instructionManua == _instructionManualListNumber) _nextText.SetText("OK");
    }

    /// <summary>
    /// プレイヤー登場
    /// </summary>
    private void PlayerAdmission()
    {
        Instantiate(_PlayerPrefabs, new Vector3(-7, 0, 0), Quaternion.identity);
    }

    //シーン移動
    private IEnumerator MovingScene()
    {
        timerscore = 0;     //時間をリセット

        //フェード用のキャンバスを出せばフェードインアウトが出来る

        //4秒待つ
        yield return new WaitForSeconds(4);

        //フェートインアウト処理後ステージ画面に飛ぶ
        SceneManager.LoadScene("ShootingGameSceneStage_Mein");
        //SceneChangr.scenechangrInstance._fade.SceneFade("ShootingGameSceneStage_Mein");
    }
}
