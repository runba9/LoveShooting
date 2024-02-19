using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

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
    private GameObject      _EnemyFixationCat;        //エネミー逆さプレハブ
    [SerializeField]
    public GameObject       _choicesObjPanel;         //表示させたい選択肢用のパネル箱
    [SerializeField]
    private GameObject      _ItemPrefabs;             //アイテムのプレハブ
    [SerializeField]
    private GameObject      _InvincibleItemPrefabs;   //無敵アイテムのプレハブ
    [SerializeField]
    private GameObject      _playerPrefabs;           //プレイヤーのプレハブ
    [SerializeField]
    public PlayerScripts   _playerScripts;            //プレイヤースクリプト

    Transform playerTransform;                        //プレイヤーの座標Transform
    private float _timer = 0;                         //経過時間
    private float _timerCat = 0;                      //逆さエネミー用の経過時間
    private float _limitTimer = 0;                    //出現時間
    private float _streamPosx = 10;                   //出現座標のうちX座標右固定
    public EnemyBoss _enemyBoss;                      //ボスエネミースクリプトを呼び出す

    //ランダムに出現するエネミーの座標ｘ,ｙのふり幅数値
    public float EnemyPosx = -4;
    public float EnemyPosy =  4;


    public static ShootingSceneManagerGameStage _shootingSceneManagerGameStage;//どこでもスクリプトを呼び出すため
    void Start()
    {
        _limitTimer = Random.Range(0.5f, 2.0f);

        //最初に呼び出すオブジェ達
        Playerrevival();
        EnemyCat();

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

            //敵を生成
            InstantiateEnemy();       
        }

        //5秒おきに逆さエネミーの敵を生成
        _timerCat += Time.deltaTime;     //時間を追加
        if (_timerCat >= 5)
        {
            _timerCat = 0;               //時間をリセット

            //敵を生成
            EnemyCat();
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

        //プレイヤー無敵タイムを呼び出す
        _playerScripts = player.GetComponent<PlayerScripts>();
        _playerScripts.GetComponent<PlayerScripts>().InvincibilityTimeBetweenRevivals();      

    }

    /// <summary>
    /// 逆さエネミー生成
    /// </summary>
    public void EnemyCat()
    {
        //逆さのエネミー生成
        Instantiate(_EnemyFixationCat);
        //敵は左から来る
        _EnemyFixationCat.transform.position = new(9, 3.41f, 0);
    }

    //エネミーとアイテムが右から左へと横から出てくるのをプレハブでランダムに出す
    private void InstantiateEnemy()
    {
        if (Random.Range(0, 20) == 0)
        {
            var item = Instantiate(_ItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }  //ガードアイテム
        if (Random.Range(0, 10) == 0)
        {
            var item = Instantiate(_InvincibleItemPrefabs);
            item.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }  //無敵アイテム
        if (Random.Range(0, 1) == 0)
        {
            var enemy = Instantiate(_enemyPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
        }   //エネミー
        if (Random.Range(0, 5) == 0)
        {
            var enemy = Instantiate(_enemytracingPrefabs);
            enemy.transform.position = new Vector3(_streamPosx, Random.Range(EnemyPosx, EnemyPosy), 0);
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
        _enemyBoss = enemy.GetComponent<EnemyBoss>();
        /*　メモ--------
         * 
         * GetComponentで見つけられずnullになる
         * 解決）
         * public EnemyBoss _enemyBoss;　
         * _enemyBoss = enemy.GetComponent<EnemyBoss>();
         * スクリプトを呼び出しを設定し解決
         */
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

        //パネル表示
        _choicesObjPanel.SetActive(true);
        //時を止める
        Time.timeScale = 0;

    }

}
