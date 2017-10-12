using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの制御
/// </summary>
public class CameraControl : MonoBehaviour
{

    /// <summary>
    /// 追尾するターゲット
    /// </summary>
    [SerializeField]
    private GameObject target;

    /// <summary>
    /// 各枠の座標
    /// </summary>
    private Vector2 frameRightPos;
    private Vector2 frameLeftPos;
    private Vector2 frameTopPos;
    private Vector2 frameBottomPos;

    //カメラの範囲の半径
    float cameraRangeX_Radius;
    float cameraRangeY_Radius;

    //各座標の最大値
    float maxLeft ;
    float maxRight;
    float maxTop;
    float maxBottom;


    void Start()
    {       
        //各オブジェクトの座標を取得
        frameTopPos = GameObject.Find("TopCollider").transform.position;
        frameBottomPos = GameObject.Find("BottomCollider").transform.position;
        frameRightPos = GameObject.Find("RightCollider").transform.position;
        frameLeftPos = GameObject.Find("LeftCollider").transform.position;

        //カメラの横の範囲の半径
        cameraRangeX_Radius = transform.position.x - getScreenTopLeft().x;
        //カメラの縦の範囲の半径
        cameraRangeY_Radius = getScreenTopLeft().y - transform.position.y;

        //各座標の最大値
        maxLeft = frameLeftPos.x + cameraRangeX_Radius;
        maxRight = frameRightPos.x - cameraRangeX_Radius;
        maxTop = frameTopPos.y - cameraRangeY_Radius;
        maxBottom = frameBottomPos.y + cameraRangeY_Radius;

    }

    void Update()
    {

        MoveCamera();

    }

    /// <summary>
    /// カメラの移動
    /// </summary>
    private void MoveCamera()
    {



        float posX = Mathf.Clamp(target.transform.position.x, maxLeft, maxRight);
        float posY = Mathf.Clamp(target.transform.position.y, maxBottom, maxTop);
        Vector3 pos = new Vector3(posX, posY, -10);
        
        transform.position = pos;
    }

    /// <summary>
    /// 画面の左上を取得
    /// </summary>
    /// <returns></returns>
    private Vector3 getScreenTopLeft()
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
    private Vector3 getScreenBottomRight()
    {
        Vector3 bottomRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        //上下反転
        bottomRight = (new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
}
