using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;

public class ObstacleScripts : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float _SpeedObstacle = 3f;  //この壁のスピード
    private bool _mouseCrick = false;   // マウスが押されているか押されていないか
    private Vector3 _mousepos;          // 現在のマウスのワールド座標
    //画面の解像度に対する比率と幅を半分にする必要があるため（ー方向と＋方向で値を保つ）2倍する
    private float _screenRatio = 200f;

    //マウスがクリックされたいる瞬間検知時、コールバックされる関数
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("clicked!");
    }

    //マウスが押されている間検知時
    public void OnPointerDown(PointerEventData eventData)
    {
        // 押下開始　フラグを立てる
        _mouseCrick = true;
        // マウスのワールド座標を保存
        _mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //マウスが離れたらの間検知時
    public void OnPointerUp(PointerEventData eventData)
    {
        // 押下終了　フラグを落とす
        _mouseCrick = false;
        _mousepos = Vector3.zero;
    }

    void Update()
    {
        Vector3 nowmousepos;
        Vector3 diffposi;

        //左から来る
        transform.position += Vector3.left * (Time.deltaTime * _SpeedObstacle);
        //画面外に出たら壁退却
        if (transform.position.x < -Screen.width / _screenRatio)
            Destroy(gameObject);


        // マウスが押下されている時、オブジェクトを動かす
        if (_mouseCrick)
        {
            // 現在のマウスのワールド座標を取得
            nowmousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 一つ前のマウス座標との差分を計算して変化量を取得
            diffposi = nowmousepos - _mousepos;
            //　Y成分のみ変化させる
            diffposi.x = 0;
            diffposi.z = 0;
            // 開始時のオブジェクトの座標にマウスの変化量を足して新しい座標を設定
            GetComponent<Transform>().position += diffposi;
            // 現在のマウスのワールド座標を更新
            _mousepos = nowmousepos;
        }
    }

}
