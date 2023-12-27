using UnityEngine;

public class BackgroundLoopScripts : MonoBehaviour
{
    //スクロールスピード調整パラメータ
    [SerializeField]
    private float _ScrollSpeed = 3f;
    //移動限界距離
    public readonly float _rightLimit = 19.2f;
    void Update()
    {
        //左方向へ　Update　での経過時間をかけたスピードを加速
        transform.position += Vector3.left * (Time.deltaTime * _ScrollSpeed);
        //X座標が左の限界を超えた場合1画面分右に加算する
        if (transform.position.x < - _rightLimit)
            transform.position += Vector3.right * _rightLimit;
    }
}
