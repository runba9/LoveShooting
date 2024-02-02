using UnityEngine;

public class ShootingSceneManagerGameStage : MonoBehaviour
{
    [SerializeField]
    private GameObject      _enemyPrefabs;            //エネミーのプレハブ
    [SerializeField]
    private GameObject      _enemytracingPrefabs;     //エネミー追跡型のプレハブ
    [SerializeField]
    private GameObject      _enemyBosPrefabs;         //エネミーBosのプレハブ
    [SerializeField]
    private GameObject      _ChoicesBulletPrefabs;    //エネミーBos攻撃用のプレハブ
    [SerializeField]
    public GameObject       _choicesObjPanel;         //表示させたい選択肢用のパネル箱
    [SerializeField]
    private GameObject      _ItemPrefabs;             //アイテムのプレハブ
    [SerializeField]
    private GameObject      _InvincibleItemPrefabs;   //無敵アイテムのプレハブ
    [SerializeField]
    private GameObject      _playerPrefabs;           //プレイヤーのプレハブ
    [SerializeField]
    private PlayerScripts   _playerSCripts;           //プレイヤースクリプト

    Transform playerTransform;                        //プレイヤーの座標Transform
    private float _timer = 0;                         //経過時間
    private float _limitTimer = 0;                    //出現時間
    private float _streamPosx = 10;                   //出現座標のうちX座標右固定
    public static ShootingSceneManagerGameStage _shootingSceneManagerGameStage;//どこでもスクリプトを呼び出すため
    void Start()
    {
        _limitTimer = Random.Range(0.5f, 2.0f);
        Playerrevival();
    }

    void Update()
    {
        //タイム
        _timer += Time.deltaTime;     //時間を追加
        //予定時刻を超えたら
        if (_timer >= _limitTimer)
        {
            _timer = 0;               //時間をリセット
            //予定時間を再設定する
            _limitTimer = Random.Range(0.5f, 2.0f);
            InstantiateEnemy();       //敵を生成
        }
    }

    /// <summary>
    /// プレイヤー残機追加処理
    /// </summary>
    public void Playerrevival()
    {
        // プレイヤーを生成
        var player = Instantiate(_playerPrefabs);
        //出現座標設定
        player.transform.position = new(-7, 0, 7);
    }

    //エネミーとアイテムが右から左へと横から出てくるのをプレハブでランダムに出す
    private void InstantiateEnemy()
    {
        if (Random.Range(0, 20) == 0)
        {
            var item = Instantiate(_ItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }  //ガードアイテム
        if (Random.Range(0, 10) == 0)
        {
            var item = Instantiate(_InvincibleItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }  //無敵アイテム
        if (Random.Range(0, 1) == 0)
        {
            var enemy = Instantiate(_enemyPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }   //エネミー
        if (Random.Range(0, 5) == 0)
        {
            var enemy = Instantiate(_enemytracingPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(-5, 5f), 0);
        }   //エネミー追跡型
    }

    /// <summary>
    /// Bosエネミーをプレハブで出す
    /// </summary>
    public void BosEnemy()
    {
        // ボスを生成
        var enemy = Instantiate(_enemyBosPrefabs);
        //出現座標設定
        enemy.transform.position = new (_streamPosx, 1, 7);
    }

    /// <summary>
    /// Bosエネミーの攻撃＆選択肢登場
    /// </summary>
    public void TimeChoicesPanel()
    {
        //プレイヤータグがあるかnullで確認してあったら実行
        if (playerTransform == null)
        {
            //呼び出す
            TimeChoicesPanel_On();
        }
        else { }

    }
    public void TimeChoicesPanel_On()
    {
        //時を止める
        Time.timeScale = 0;
        //パネル表示
        _choicesObjPanel.SetActive(true);
    }

}
