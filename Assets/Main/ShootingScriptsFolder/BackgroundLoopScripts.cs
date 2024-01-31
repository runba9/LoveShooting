using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoopScripts : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_propName = "_MainTex";

    //動くスピード
    [SerializeField]
    private Vector2 m_offsetSpeed;
    //動かしたい背景マテリアル
    private Material m_material;

    private void Start()
    {
        if (GetComponent<Image>() is Image i)
        {
            m_material = i.material;
        }
    }

    private void Update()
    {
        if (m_material)
        {
            // xとyの値が0 ～ 1でリピートするようにする
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
            var offset = new Vector2(x, y);
            m_material.SetTextureOffset(k_propName, offset);
        }

        ////左方向へ　Update　での経過時間をかけたスピードを加速
        //transform.position += Vector3.left * (Time.time * movespeed);
        ////X座標が左の限界を超えた場合1画面分右に加算する
        //if (transform.position.x < -_rightLimit)
        //    transform.position += Vector3.right * _rightLimit;
    }

    private void OnDestroy()
    {
        // ゲームをやめた後にマテリアルのOffsetを戻しておく
        if (m_material)
        {
            m_material.SetTextureOffset(k_propName, Vector2.zero);
        }
    }
}
