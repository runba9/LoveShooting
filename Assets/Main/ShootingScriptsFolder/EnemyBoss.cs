using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    public GameObject _enemy;                       //このボスオブジェを入れる箱
    [SerializeField]
    private float _Speed = 3f;                      //エネミーのスピード
    [SerializeField]
    private int _EnemyBosshp = 30;                  //ボスの体力
    private int Critical;                           //クリティカル攻撃告白
    private int Damage;                             //プレイヤーの通常攻撃
    private bool criticalCount = false;             //クリティカル攻撃告白の有無
    public bool _criticalconfession = false;

    public GameObject[] _choicesBullet;             //攻撃の選択肢とダミーをランダムで出す為ののリスト

    private Vector2 pos;                            //座標
    public int _enemyBoss = 1;                      //エネミーの数
    public EffectScripts _effectEnemy;              //爆発エフェクトのプレハブ
    private System.Action _deadCallback;            //死んだときに死んだことを伝える
    private GameObject SEgameObj;                   //Unity上で作ったGameObjectである名前SEを入れる変数

    public static EnemyBoss _enemyBossScripts;        //どこでもスクリプトを呼び出すため
    void Start()
    {
        SEgameObj = GameObject.Find("SE");          //Unity上で作ったSEを取得

        //5秒後InputiateBullet()を呼び出し弾を発射させる
        InvokeRepeating("InputiateChoicesbullet", 1, 5);
    }
    /// <summary>
    /// 弾発射装置
    /// </summary>
    public void InputiateChoicesbullet()
    {
        //アイテム用SE再生
        SEgameObj.GetComponent<SEScripts>().bulletSE();

        //リスト化したものをランダムで出力していく、下のメモの簡易バージョン
        var Choicesbullet = Random.Range(0, _choicesBullet.Length);
        Instantiate(_choicesBullet[Choicesbullet], transform.position, transform.rotation);

        //////メモ(これはただの記念で残してるだけです)
        ////選択肢
        //if (Random.Range(0, 1) == 0)
        //{
        //    //弾を生成する
        //    var Choicesbullet = Instantiate(_choicesBullet);
        //    //弾の位置情報
        //    Choicesbullet.transform.position = transform.position;
        //}
        ////ダミー選択肢
        //if (Random.Range(0, 1) == 0)
        //{
        //    //弾を生成する
        //    var Choicesbullet = Instantiate(_choicesNoBullet);
        //    //弾の位置情報
        //    Choicesbullet.transform.position = transform.position;
        //}

    }

    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {
        //現在のボスのhp
        Debug.Log("現在hp" + _EnemyBosshp);
        //ボスの体力が０か０を超えたらオブジェクト破壊
        if (_EnemyBosshp <= 0)
        {
            Deadboss();
        }

        pos = transform.position;

        //左右移動
        transform.Translate(transform.right * Time.deltaTime * _Speed * _enemyBoss);
        if (pos.x > 7)//幅
        {
            _enemyBoss = -1;//速さ
        }
        if (pos.x < 6)
        {
            _enemyBoss = 1;
        }

    }

    //敵消滅
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //弾がエネミーに当たったら
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();
            // ボスが生きてる時の処理
            Conventionalattack();
        }
    }

    /// <summary>
    /// 通常攻撃
    /// </summary>
    public void Conventionalattack()
    {

        //ダメージが入ったらオブジェを見えなくさせる
        //_enemy.SetActive(false);

        //通常攻撃
        Damage = 1;

        //フラグがtrueならクリティカル攻撃処理を起動させる
        if (criticalCount == true)
        {
            //クリティカルダメージ
            Critical = 10;

            //計算処理
            _EnemyBosshp -= Critical;
            Debug.Log("クリティカルhp" + _EnemyBosshp);
            //全て終わったらフラグを戻す
            criticalCount = false;
        }

        //計算処理
        _EnemyBosshp -= Damage;
        Debug.Log("計算処理後hp" + _EnemyBosshp);
        //ダメージが入ったらオブジェを見えなくさせ0.1後に復活
        Invoke(("EnemyrevivalOn"), 0.1f);

    }

    /// <summary>
    /// クリティカル告白攻撃
    /// </summary>
    public void Criticalattack()
    {
        //クリティカル告白攻撃をtrueにして
        criticalCount = true;
        //攻撃呼び出し
        Conventionalattack();
    }

    /// <summary>
    /// 消したオブジェを0.1秒後に呼び出す演出
    /// </summary>
    public void EnemyrevivalOn()
    {
        //_enemy.SetActive(true);
    }

    /// <summary>
    /// ボスの死んだ処理
    /// </summary>
    public void Deadboss()
    {
        // 弾が当たった場所に爆発エフェクトを生成する
        Instantiate(
            _effectEnemy,
            transform.localPosition,
            Quaternion.identity);

        //HPが０になったら死ぬ
        _deadCallback?.Invoke();  //メモ：？は_deadCallbackがnullじゃないときに関数を呼び出す
        Destroy(gameObject);     //敵消滅
                                 //フェートインアウト処理後リザルト画面に飛ぶ
        SceneChangr.scenechangrInstance._fade.SceneFade("ResultSeen");
    }

}
