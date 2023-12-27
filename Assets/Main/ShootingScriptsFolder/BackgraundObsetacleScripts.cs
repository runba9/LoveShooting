using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgraundObsetacleScripts : MonoBehaviour
{
    [SerializeField]
    public GameObject Up;
    [SerializeField]
    public GameObject Down;

    //スクロールスピード調整パラメータ
    [SerializeField]
    private float _ScrollSpeed = 3f;

    //移動限界距離
    private readonly float _rightLimit = 19.2f;

    void Update()
    {
        //左方向へ　Update　での経過時間をかけたスピードを加速
        Down.transform.position += Vector3.left * (Time.deltaTime * _ScrollSpeed);
        //X座標が左の限界を超えた場合1画面分右に加算する
        if (Down.transform.position.x < -_rightLimit)
            Down.transform.position += Vector3.right * _rightLimit;

        if (Down.transform.position.x < -_rightLimit)
        {
            //左方向へ　Update　での経過時間をかけたスピードを加速
            Up.transform.position += Vector3.left * (Time.deltaTime * _ScrollSpeed);
            //X座標が左の限界を超えた場合1画面分右に加算する
            if (Up.transform.position.x < -_rightLimit)
                Up.transform.position += Vector3.right * _rightLimit;
        }
    }
}
