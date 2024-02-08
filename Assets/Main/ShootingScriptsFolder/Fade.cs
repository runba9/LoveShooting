using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    // Fade�ɂ����鎞�ԁB
    [SerializeField]
    private float FadeTime = 1.0f;

    // Fade�p��PrefubCanvas�̎擾�B
    [SerializeField]
    private GameObject PrefabFadeCanvas;

    // ��������FadeCanvas�i�[�p
    private GameObject _fadeCanvas;

    // component�擾�p�ϐ�
    private CanvasGroup _canvasGroup;

    // �t���O
    private bool IsEndFadeIn = false;
    private bool IsEndFadeOut = false;

    private void Start()
    {
        // �V�[�����܂����ł��j������Ȃ��悤�ɁB
        DontDestroyOnLoad(this);

        // _fadeCanvas�̐����B
        _fadeCanvas = Instantiate(PrefabFadeCanvas, this.gameObject.transform);

        // alpha�l�ύX�p��_fadeCanvas�擾
        _canvasGroup = _fadeCanvas.GetComponent<CanvasGroup>();

        //  ��ʏ��FadeCanvas�������Ȃ��悤�ɂ���B
        _fadeCanvas.SetActive(false);
    }

    /// <summary>
    ///  ��ʂ��Â��Ȃ� => �V�[���J�� => ��ʂ����邭�Ȃ�B
    /// </summary>
    /// <param name="name">�J�ڐ�V�[����</param>
    public async void SceneFade(string name)
    {
        // _fadeCanvas��������悤�ɂ���B
        _fadeCanvas.SetActive(true);

        //��ʂ����񂾂�Â�����B
        await FadeOut(this.GetCancellationTokenOnDestroy());

        //��ʂ��^���ÂɂȂ�܂�(��̏������I���܂�)�҂B
        await UniTask.WaitUntil(() => IsEndFadeOut, cancellationToken: this.GetCancellationTokenOnDestroy());

        // �t���O�̏�����
        IsEndFadeOut = false;

        // �V�[���J��
        SceneManager.LoadScene(name);

        // ��3�͏�̋t�B
        await FadeIn(this.GetCancellationTokenOnDestroy());
        await UniTask.WaitUntil(() => IsEndFadeIn, cancellationToken: this.GetCancellationTokenOnDestroy());
        IsEndFadeIn = false;

        // Canvas�������Ȃ�����B
        _fadeCanvas.SetActive(false);

    }

    private async UniTask FadeIn(CancellationToken token)
    {
        // canvas��alpha�l��1�ɂ���B
        _canvasGroup.alpha = 1f;

        // alpha�l��0���傫���Ƃ�
        while (_canvasGroup.alpha > 0f)
        {
            // ���t���[��alpha�l�����Z
            _canvasGroup.alpha -= Time.deltaTime / FadeTime;
            // ����0��菬�����Ȃ�����
            if (_canvasGroup.alpha < 0f)
            {
                // 0�ɖ߂�
                _canvasGroup.alpha = 0f;
            }

            // UniTask���g���ꍇ�������Ƃ�����
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        // FadeIn�I���t���O
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
