using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayerScripts : MonoBehaviour
{
    //プレイヤー
    [SerializeField]
    private float _PlayerSpeed = 0.6f;               //プレイヤーのスピード
    [SerializeField]
    private SpriteRenderer _playerSprite;

    //プレイヤーの銃弾
    [SerializeField]
    private GameObject _PlayerBulletObject;          //銃弾のプレハブを入れる
    [SerializeField]
    private float _posRealignment = 0.2f;            //プレイヤーの銃弾の位置調整

    //プレイヤーアニメーション
    private Animator animator;

    //入力システム(InputSystem)
    private Distortion_Game _gameInputsSystem;
    //球の打ち出す位置の初期設定
    private readonly float _bulletShotOffset = 6f;

    //プレイヤーの周りを回るバリアの最大数
    private readonly int _attachCountMax = 6;
    //プレイヤーの周りを回るバリアのリスト
    private List<GameObject> _attachObjects = new List<GameObject>();

    //プレイヤー行動制限(画面外に行かなくさせる)
    // x軸方向の移動範囲の最小値
    [SerializeField] private float _minX = -9;
    // x軸方向の移動範囲の最大値
    [SerializeField] private float _maxX = 9.45f;
    // y軸方向の移動範囲の最小値
    [SerializeField] private float _minY = -4.2f;
    // y軸方向の移動範囲の最大値
    [SerializeField] private float _maxY = 3.78f;

    //無敵アイテム
    [SerializeField]
    public float flashInterval;                       //フラッシュする間
    [SerializeField]
    public int loopCount;                             //点滅させるときのループカウント(何回点滅させるか)

    public bool ItemInvincibleisGetHit;               //無敵アイテム取得してるのかの判定
    SpriteRenderer flashing;                          //点滅させるためのSpriteRenderer

    //プレイヤーの残機呼び出し
    private GameObject PlayerRemainingLivesObj;//Unity上で作ったGameObjectである名前PlayerRemainingLivesObjを入れる変数
    //SE呼び出し
    private GameObject SEgameObj;              //Unity上で作ったGameObjectである名前SEを入れる変数
    private GameObject gameObjScore;           //Unity上で作ったGameObjectである名前gameObjScoreを入れる変数
    //ボスの攻撃（選択肢攻撃）呼び出し
    private GameObject ChoicesgameObj;         //Unity上で作ったGameObjectである名前ChoicesgameObjを入れる変数
    void Start()
    {
        animator = GetComponent<Animator>();

        //インプットシステムを用意して有効化する
        _gameInputsSystem = new Distortion_Game();
        _gameInputsSystem.Enable();

        SEgameObj               = GameObject.Find("SE");              //Unity上で作ったSEを取得
        ChoicesgameObj          = GameObject.Find("GameManager");     //Unity上で作ったGameManagerを取得
        PlayerRemainingLivesObj = GameObject.Find("GameManager");     //Unity上で作ったGameManagerを取得
        gameObjScore            = GameObject.Find("GameManager");     //Unity上で作ったGameManagerを取得

        flashing                = GetComponent<SpriteRenderer>();     //スプライトレンダラーを取得させる


    }

    void Update()
    {
        //プレイヤーの機能呼び出し
        Player();

        //左クリックを押していたら弾を発射するのを呼び出す
        if (_gameInputsSystem.Player.Fire.triggered) InputiateBullet();

    }

    /// <summary>
    /// プレイヤー行動
    /// </summary>
    public void Player()
    {
        //プレイヤー
        //移動方向を取得
        var movePos = _gameInputsSystem.Player.Move.ReadValue<Vector2>();
        //移動方向による移動速度が出ないように速度を一定にする為normalizedにする
        movePos = movePos.normalized;
        transform.localPosition += (Vector3)movePos * (Time.deltaTime * _PlayerSpeed);

        ////移動範囲制限
        var PlayerPos = transform.position;
        // x軸方向の移動範囲制限
        PlayerPos.x = Mathf.Clamp(PlayerPos.x, _minX, _maxX);
        // y軸方向の移動範囲制限
        PlayerPos.y = Mathf.Clamp(PlayerPos.y, _minY, _maxY);
        transform.position = PlayerPos;
    }

    /// <summary>
    /// 弾発射装置
    /// </summary>
    public void InputiateBullet()
    {
        //アイテム用SE再生
        SEgameObj.GetComponent<SEScripts>().bulletSE();

        //射撃アニメーション
        animator.SetTrigger("PlayerLoveBullet");

        //弾を生成する
        var bullet = Instantiate(_PlayerBulletObject);
        //弾の位置情報
        bullet.transform.position = transform.position + Vector3.right * _bulletShotOffset * _posRealignment;
    }

    //当たり判定
    private void OnTriggerEnter2D(Collider2D other)
    {
        //アイテム
        //  当たったオブジェクトがオプションアイテム(ガード)ならば
        if (other.gameObject.tag.Equals("Option"))
        {
            //アイテム用SE再生
            SEgameObj.GetComponent<SEScripts>().ItemSE();

            //  すでにオプションが MAXまで付いているなら何もしない
            if (_attachObjects.Count >= _attachCountMax) return;

            _attachObjects.Add(other.gameObject);

            var itemScripts = other.GetComponent<ItemScripts>();
            //アタッチして、死んだらBrokenDefenderの処理へ
            itemScripts.Attach(transform, 0, BrokenDefender);
            SetItmePosition();

        }
        //  無敵アイテム
        if (other.gameObject.tag.Equals("Invincible"))
        {
            //アイテム用SE再生
            SEgameObj.GetComponent<SEScripts>().ItemSE();

            //無敵アイテムを既に取得していたら何もしない
            if (ItemInvincibleisGetHit)
            {
                return;
            }
            //ItemInvincibl(無敵アイテム)コルーチン呼び出し
            StartCoroutine(_ItemInvincible());
        }

        //エネミー
        //  当たったオブジェクトがボスからの攻撃(選択肢攻撃)ならば
        if (other.gameObject.tag.Equals("ChoicesBullet"))
        {
            //アイテム用SE再生
            SEgameObj.GetComponent<SEScripts>().ItemSE();
            ChoicesgameObj.GetComponent<ShootingSceneManagerGameStage>().TimeChoicesPanel();
        }
        //  当たったオブジェクトがエネミー
        if (other.gameObject.tag.Equals("Enemy"))
        {

            gameObjScore.GetComponent<ScoreScripts>().RefreshScoreText();//RefreshScoreText()を実行して加点

            //無敵アイテムを既に取得していたら死なない
            if (ItemInvincibleisGetHit)
            {
                return;
            }

            //プレイヤー死ぬ
            Destroy(gameObject);
            //プレイヤーの残機を減らす
            PlayerRemainingLivesObj.GetComponent<ScoreScripts>().PlayerRemainingLivesText();

        }
        // 当たったのが障害物ならば
        if (other.gameObject.tag.Equals("Wall"))
        {        
            //ダメージがコルーチン起動
            StartCoroutine(PlayerevivalOn());

            //プレイヤー死ぬ
            Destroy(gameObject);
            //プレイヤーの残機を減らす
            PlayerRemainingLivesObj.GetComponent<ScoreScripts>().PlayerRemainingLivesText();

        }
        //  当たったオブジェクトがチュートリアル用エネミーならば
        if (other.gameObject.tag.Equals("Ring"))
        {
            //ダメージ用SE再生
            SEgameObj.GetComponent<SEScripts>().damageSE();
        }
        
    }

    /// <summary>
    /// プレイヤー演出
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerevivalOn()
    {
        //ダメージが入ったらオブジェを見えなくさせる
        _playerSprite.enabled = false;
        yield return new WaitForSeconds(0.1f); // 0.1秒間待機
        _playerSprite.enabled = true;

        //0.5秒遅くして
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.1f); // 0.1秒間待機

        //時間戻す
        Time.timeScale = 1f;

    }


    /// <summary>
    /// 無敵アイテム処理(コルーチン)
    /// </summary>
    public IEnumerator _ItemInvincible()
    {
        //当たりフラグをtrueに変更（当たっている状態）
        ItemInvincibleisGetHit = true;
        //Debug.Log("取得");
        //点滅ループ開始
        for (int i = 0; i < loopCount; i++)
        {
            //flashInterval待ってから
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer(点滅)をオフ
            flashing.enabled = false;
            
            //flashInterval待ってから
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer(点滅)をオン
            flashing.enabled = true;
            //Debug.Log("ループ中");
        }

        //点滅ループが抜けたら当たりフラグをfalse(当たってない状態)
        ItemInvincibleisGetHit = false;
        //Debug.Log("終わった");
    }

    /// <summary>
    /// アイテム(ガード)を円状に均等に配置する
    /// </summary>
    private void SetItmePosition()
    {
        //  各オプションの間の角度（ラジアン）
        var addAngle = Mathf.PI * 2 / _attachObjects.Count;
        //  オプションの要素を順番に処理する
        foreach (var (itemScripts, index) in _attachObjects.Select((obj, index) => (obj.GetComponent<ItemScripts>(), index)))
        {
            //  所定の場所に設定
            itemScripts.ResetAngle(addAngle * index);
        }
    }

    /// <summary>
    /// 破壊されたゲームオブジェクト(アイテム)をリストから削除する
    /// </summary>
    /// <param name="gobj"></param>
    private void BrokenDefender(GameObject gobj)
    {
        _attachObjects.Remove(gobj);
    }
}
