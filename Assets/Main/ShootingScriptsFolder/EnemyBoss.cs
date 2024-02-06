using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    public GameObject _enemy;                       //このボスオブジェを入れる箱
    private SpriteRenderer _enemyboss;              //ボスのスプライトレンダラー用の箱
    [SerializeField]
    private float _Speed = 3f;                      //エネミーのスピード
    [SerializeField]
    private int _EnemyBosshp = 100;                 //ボスの体力
    private int Critical;                           //クリティカル攻撃告白
    private int Damage;                             //プレイヤーの通常攻撃
    private bool criticalCount = false;             //クリティカル攻撃告白の有無
    private Slider BossHp_slider;                   //ボスのhpを可視化するためにスライダーで表示
                                                    
    private Animator animator;                      //アニメーション
    public GameObject[] _choicesBullet;             //攻撃の選択肢とダミーをランダムで出す為ののリスト

    private Vector2 pos;                            //座標
    public int _enemyBoss = 1;                      //エネミーの数
    public EffectScripts _effectEnemy;              //爆発エフェクトのプレハブ
    private System.Action _deadCallback;            //死んだときに死んだことを伝える
    private GameObject SEgameObj;                   //Unity上で作ったGameObjectである名前SEを入れる変数

    public static EnemyBoss _enemyBossScripts;      //どこでもスクリプトを呼び出すため
    void Start()
    {
        SEgameObj           = GameObject.Find("SE");                                      //Unity上で作ったSEを取得
        BossHp_slider       = GameObject.Find("BossHpSlider").GetComponent<Slider>();     //Unity上で作ったBossHpSliderを取得
        _enemyboss          = GetComponent<SpriteRenderer>(); //ボスのスプライトレンダラー取得
        animator = GetComponent<Animator>();                　//アニメーション取得
        //5秒感覚で開けて呼び出しクリティカル攻撃弾を発射させる
        InvokeRepeating("InputiateChoicesbullet", 1.0f, 5.0f);
    }       

    public void SetUp(float speed = 6, System.Action deadCallback = null)
    {
        _Speed = speed;
        _deadCallback = deadCallback;
    }

    void Update()
    {

        //現在のボスのhp
        //Debug.Log("現在hp" + 最大hp);
        //ボスが半分のダメージを食らったらの処理
        if (_EnemyBosshp <= 50 )
        {
            animator.SetBool("BossLoveMotion", true);
        }
        //ボスの体力が０か０を超えたらオブジェクト破壊
        if (_EnemyBosshp <= 0 || _EnemyBosshp <= -1)
        {
            StartCoroutine(Deadboss());
        }

        //ボスの移動範囲（左右移動）
        pos = transform.position;
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

    /// <summary>
    /// クリティカル攻撃弾発射装置
    /// </summary>
    private void InputiateChoicesbullet()
    {
        //アイテム用SE再生
        SEgameObj.GetComponent<SEScripts>().bulletSE();

        //リスト化したものをランダムで出力していく、下のメモの簡易バージョン
        var Choicesbullet = Random.Range(0, _choicesBullet.Length);
        Instantiate(_choicesBullet[Choicesbullet], transform.position, transform.rotation);
        /*メモ----------
           　簡易バージョンが作れたのが嬉しかったのでメモに残してます
        //選択肢
        if (Random.Range(0, 1) == 0)
        {
            //弾を生成する
            var Choicesbullet = Instantiate(_choicesBullet);
            //弾の位置情報
            Choicesbullet.transform.position = transform.position;
        }
        //ダミー選択肢
        if (Random.Range(0, 1) == 0)
        {
            //弾を生成する
            var Choicesbullet = Instantiate(_choicesNoBullet);
            //弾の位置情報
            Choicesbullet.transform.position = transform.position;
        }
        */

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

        // HPゲージに値を設定
        BossHp_slider.value = _EnemyBosshp;

        //ダメージが入ったらオブジェを見えなくさせ0.1後に復活
        StartCoroutine(EnemyrevivalOn());
        /*メモ-------
        Invoke(("EnemyrevivalOn"), 0.1f);で呼び出していたが
        コルーチンの方が圧倒的に使いやすい
        */
    }

    /// <summary>
    /// クリティカル告白攻撃
    /// </summary>
    public void Criticalattack()
    {
        Debug.Log("呼び出しOK");
        //クリティカル告白攻撃をtrueにして
        criticalCount = true;
        //攻撃呼び出し
        Conventionalattack();
    }


    /// <summary>
    /// 攻撃したオブジェを0.1秒後に呼び出す演出コルーチン
    /// </summary>
    private IEnumerator EnemyrevivalOn()
    {
        //ダメージが入ったらオブジェを見えなくさせる
        _enemyboss.enabled = false;
        yield return new WaitForSeconds(0.1f); // 0.1秒間待機
        _enemyboss.enabled = true;
        /*メモ----------------
         _enemy.SetActive(true);で消すと全部消えるので画像だけの
        スプライトレンダラーのみon、offした方が便利
         */

    }

    /// <summary>
    /// ボスの死んだ処理
    /// </summary>
    private IEnumerator Deadboss()
    {
        yield return new WaitForSeconds(1);

        // 弾が当たった場所に爆発エフェクトを生成する
        Instantiate(_effectEnemy,transform.localPosition,Quaternion.identity);

        //HPが０になったら死ぬ
        _deadCallback?.Invoke();        //メモ：？は_deadCallbackがnullじゃないときに関数を呼び出す
        _enemyboss.enabled = false;     //敵消滅

        yield return new WaitForSeconds(3);

        //フェートインアウト処理後リザルト画面に飛ぶ
        SceneManager.LoadScene("ResultScene");
        //SceneChangr.scenechangrInstance._fade.SceneFade("ResultScene");

    }

}
