using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの制御
/// </summary>
public class CameraControl : MonoBehaviour
{

    //カメラの縦の範囲の半径
    private float cameraRangeY_Radius;
    public float CameraRangeY_Radius
    {
        get { return cameraRangeY_Radius; }
    }
    //スクロール中かどうか
    private bool isScroll;
    //スクロール開始位置
    private Vector3 scrollStartPosition;
    Vector3 endPosition;
    //スクロール開始時間
    private float scrollStartTime;
    //スクロール終了までの時間
    private float scrollTime = 2;

    Vector3 playerPos;

    ///// <summary>
    ///// 追尾するターゲット
    ///// </summary>
    //[SerializeField]
    //private GameObject target;

    ///// <summary>
    ///// 各枠の座標
    ///// </summary>
    //private Vector2 frameRightPos;
    //private Vector2 frameLeftPos;
    //private Vector2 frameTopPos;
    //private Vector2 frameBottomPos;

    ////カメラの範囲の半径
    //float cameraRangeX_Radius;
    //float cameraRangeY_Radius;

    ////各座標の最大値
    //float maxLeft;
    //float maxRight;
    //float maxTop;
    //float maxBottom;
    ////ゲームプレイ中のカメラのサイズ
    //private float gamePlayCameraSize = 14;

    //private bool isMove;
    //public bool IsMove
    //{
    //    set { isMove = value; }
    //}



    ///// <summary>
    ///// カメラの範囲の設定
    ///// </summary>
    //private void SetCameraRange()
    //{
    //    //if (!isGameStart) return;
    //    //各オブジェクトの座標を取得
    //    frameTopPos = GameObject.Find("TopCollider").transform.position;
    //    frameBottomPos = GameObject.Find("BottomCollider").transform.position;
    //    frameRightPos = GameObject.Find("RightCollider").transform.position;
    //    frameLeftPos = GameObject.Find("LeftCollider").transform.position;

    //    //カメラの横の範囲の半径
    //    cameraRangeX_Radius = transform.position.x - getScreenTopLeft().x;
    //    //カメラの縦の範囲の半径
    //    cameraRangeY_Radius = getScreenTopLeft().y - transform.position.y;

    //    //各座標の最大値
    //    maxLeft = frameLeftPos.x + cameraRangeX_Radius;
    //    maxRight = frameRightPos.x - cameraRangeX_Radius;
    //    maxTop = frameTopPos.y - cameraRangeY_Radius;
    //    maxBottom = frameBottomPos.y + cameraRangeY_Radius;
    //}

    void Start()
    {

        // StartCoroutine(CameraZoom());

    }

    void Update()
    {
        CameraScroll();
        Debug.Log("スクロール"+isScroll);
        //Scroll();
    }

    ///// <summary>
    ///// カメラの移動
    ///// </summary>
    //private void MoveCamera()
    //{
    //    if (!isMove) return;
    //    float posX = Mathf.Clamp(target.transform.position.x, maxLeft, maxRight);
    //    float posY = Mathf.Clamp(target.transform.position.y, maxBottom, maxTop);
    //    Vector3 pos = new Vector3(posX, posY, -10);

    //    transform.position = pos;
    //}

    /// <summary>
    /// 画面の左上を取得
    /// </summary>
    /// <returns></returns>
    public Vector3 getScreenTopLeft()
    {
        Vector3 topLeft = GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero);
        //左右反転
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    /// <summary>
    /// 画面の右上を取得
    /// </summary>
    /// <returns></returns>
    public Vector3 getScreenBottomRight()
    {
        //Vector3 bottomRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        //上下反転
        //bottomRight = (new Vector3(1f, -1f, 1f));
        return -getScreenTopLeft();
    }

    /// <summary>
    /// Wave進行時のカメラのスクロール
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public void StartCameraScroll(float targetPosX,float targetPosY,float time)
    {
        if (isScroll) return;
        //カメラの縦の範囲の半径
        //cameraRangeY_Radius = getScreenTopLeft().y - transform.position.y;
        scrollTime = time;
       
        scrollStartTime = Time.timeSinceLevelLoad;
        scrollStartPosition = transform.position;
        //playerPos = GameObject.Find("PlayerFront").transform.position;
        endPosition = new Vector3(targetPosX,targetPosY, -10);
        isScroll = true;
        
       
    }

    private void CameraScroll()
    {
        if (!isScroll) return;
        var diff = Time.timeSinceLevelLoad - scrollStartTime;
        Debug.Log(endPosition);
        if (diff > scrollTime)
        {
            transform.position = endPosition;
            isScroll = false;
        }

        var rate = diff / scrollTime;

        transform.position = Vector3.Lerp(scrollStartPosition, endPosition, rate);

    }

    
    public IEnumerator CameraZoom()
    {
        //StartCameraScroll();
      
        while (GetComponent<Camera>().orthographicSize >= 12)
        {
           
            GetComponent<Camera>().orthographicSize -= 0.2f;
            yield return null;
        }


        // SetCameraRange();
    }


}



































