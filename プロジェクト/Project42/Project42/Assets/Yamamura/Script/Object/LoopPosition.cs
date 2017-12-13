using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが画面外に行った際に反対側から出てくる
/// 用に見せるために画面外のプレイヤーを位置を変える処理
/// </summary>
public class LoopPosition : MonoBehaviour
{
    public GameObject[] sidePos;//左右エリア
    public GameObject[] upDownPos;//上下エリア
    public GameObject[] hammers;

    // Update is called once per frame
    void Update()
    {
        AreaCheck();
    }

    /// <summary>
    /// 各エリア内での処理
    /// </summary>
    private void AreaCheck()
    {
        Area();
    }

    private void Area()
    {
        foreach (var hammer in hammers)
        {   //上のエリアより上に行ったら
            if (transform.position.y > upDownPos[0].transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, upDownPos[1].transform.position.y);
                hammer.transform.position = new Vector2(hammer.transform.position.x, upDownPos[1].transform.position.y);
            }
            //下のエリアより下に行ったら
            else if (transform.position.y < upDownPos[1].transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, upDownPos[0].transform.position.y);
                hammer.transform.position = new Vector2(hammer.transform.position.x, upDownPos[0].transform.position.y);
            }
            //左のエリアより左に行ったら
            else if (transform.position.x < sidePos[0].transform.position.x)
            {
                transform.position = new Vector2(sidePos[1].transform.position.x, transform.position.y);
                hammer.transform.position = new Vector2(sidePos[1].transform.position.x, hammer.transform.position.y);
            }
            //右のエリアより右に行ったら
            else if (transform.position.x > sidePos[1].transform.position.x)
            {
                transform.position = new Vector2(sidePos[0].transform.position.x, transform.position.y);
                hammer.transform.position = new Vector2(sidePos[0].transform.position.x, hammer.transform.position.y);
            }
        }
    }

}
