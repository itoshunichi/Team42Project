using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが画面外に行った際に反対側から出てくる
/// 用に見せるために画面外のプレイヤーを位置を変える処理
/// </summary>
public class LoopPosition : MonoBehaviour
{
    public GameObject[] frontPos;//正面エリア
    public GameObject[] sidePos;//左右エリア
    public GameObject[] UpDownPos;//上下エリア
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
        foreach (var hammer in hammers)
        {   //上面エリアか下面エリアにいるとき
            if (transform.position.y < frontPos[0].transform.position.y - 1 || transform.position.y > frontPos[1].transform.position.y + 1)
            {
                if (transform.position.x < frontPos[0].transform.position.x + 0.1f)
                {
                    transform.position = new Vector2(frontPos[1].transform.position.x - 0.1f, transform.position.y);
                    hammer.transform.position = new Vector2(frontPos[1].transform.position.x, hammer.transform.position.y);
                }
                else if (transform.position.x > frontPos[1].transform.position.x - 0.1f)
                {
                    transform.position = new Vector2(frontPos[0].transform.position.x + 0.1f, transform.position.y);
                    hammer.transform.position = new Vector2(frontPos[0].transform.position.x, hammer.transform.position.y);
                }
            }
            //左エリアか右エリアにいるとき
            else if (transform.position.x < frontPos[0].transform.position.x|| transform.position.x > frontPos[1].transform.position.x)
            {
                if (transform.position.y < frontPos[0].transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x, frontPos[1].transform.position.y);
                    hammer.transform.position = new Vector2(hammer.transform.position.x, frontPos[1].transform.position.y);
                }
                else if (transform.position.y > frontPos[1].transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x, frontPos[0].transform.position.y);
                    hammer.transform.position = new Vector2(hammer.transform.position.x, frontPos[0].transform.position.y);
                }
            }
        }
    }

    private void Area()
    {
        foreach (var hammer in hammers)
        {
            if (transform.position.y > UpDownPos[0].transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, UpDownPos[1].transform.position.y);
                hammer.transform.position = new Vector2(hammer.transform.position.x, UpDownPos[1].transform.position.y);
            }
            else if (transform.position.y < UpDownPos[1].transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, UpDownPos[0].transform.position.y);
                hammer.transform.position = new Vector2(hammer.transform.position.x, UpDownPos[0].transform.position.y);
            }
            else if (transform.position.x < sidePos[0].transform.position.x)
            {
                transform.position = new Vector2(sidePos[1].transform.position.x, transform.position.y);
                hammer.transform.position = new Vector2(sidePos[1].transform.position.x, hammer.transform.position.y);
            }
            else if (transform.position.x > sidePos[1].transform.position.x)
            {
                transform.position = new Vector2(sidePos[0].transform.position.x, transform.position.y);
                hammer.transform.position = new Vector2(sidePos[0].transform.position.x, hammer.transform.position.y);
            }
        }
    }

}
