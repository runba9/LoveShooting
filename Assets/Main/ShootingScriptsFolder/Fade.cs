using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    // Fadeにかかる時間。
    [SerializeField]
    private float FadeTime = 1.0f;

    // Fade用のPrefubCanvasの取得。
    [SerializeField]
    private GameObject PrefabFadeCanvas;

    // 生成したFadeCanvas格納用
    private GameObject _fadeCanvas;

    // component取得用変数
    private CanvasGroup _canvasGroup;

    // フラグ
    private bool IsEndFadeIn = false;
    private bool IsEndFadeOut = false;

    private void Start()
    {
        // シーンをまたいでも破棄されないように。
        DontDestroyOnLoad(this);

        // _fadeCanvasの生成。
        _fadeCanvas = Instantiate(PrefabFadeCanvas, this.gameObject.transform);

        // alpha値変更用の_fadeCanvas取得
        _canvasGroup = _fadeCanvas.GetComponent<CanvasGroup>();

        //  画面上のFadeCanvasを見えないようにする。
        _fadeCanvas.SetActive(false);
    }
    
    /// <summary>
    ///  画面が暗くなる => シーン遷移 => 画面が明るくなる。
    /// </summary>
    /// <param name="name">遷移先シーン名</param>
    public async void SceneFade(string name)
    {
        // _fadeCanvasを見えるようにする。
        _fadeCanvas.SetActive(true);

        //画面をだんだん暗くする。
        await FadeOut(this.GetCancellationTokenOnDestroy());

        //画面が真っ暗になるまで(上の処理が終わるまで)待つ。
        await UniTask.WaitUntil(() => IsEndFadeOut, cancellationToken: this.GetCancellationTokenOnDestroy());

        // フラグの初期化
        IsEndFadeOut = false;

        // シーン遷移
        SceneManager.LoadScene(name);

        // 下3つは上の逆。
        await FadeIn(this.GetCancellationTokenOnDestroy());
        await UniTask.WaitUntil(() => IsEndFadeIn, cancellationToken: this.GetCancellationTokenOnDestroy());
        IsEndFadeIn = false;

        // Canvasを見えなくする。
        _fadeCanvas.SetActive(false);

    }

    private async UniTask FadeIn(CancellationToken token)
    {
        // canvasのalpha値を1にする。
        _canvasGroup.alpha = 1f;

        // alpha値が0より大きいとき
        while(_canvasGroup.alpha > 0f)
        {
            // 毎フレームalpha値を減算
            _canvasGroup.alpha -= Time.deltaTime / FadeTime;
            // もし0より小さくなったら
            if(_canvasGroup.alpha < 0f)
            {
                // 0に戻す
                _canvasGroup.alpha = 0f;
            }

            // UniTaskを使う場合書くことが多い
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        // FadeIn終了フラグ
        IsEndFadeIn = true;
    }

    private async UniTask FadeOut(CancellationToken token)
    {
        _canvasGroup.alpha = 0f;

        while (_canvasGroup.alpha < 1f)
        {
            _canvasGroup.alpha += Time.deltaTime / FadeTime;
            if (_canvasGroup.alpha > 1f)
            {
                _canvasGroup.alpha = 1f;
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        IsEndFadeOut = true;
    }
}
