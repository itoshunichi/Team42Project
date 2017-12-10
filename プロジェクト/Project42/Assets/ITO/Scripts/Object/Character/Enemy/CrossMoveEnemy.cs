using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 左右
/// </summary>
enum ABOUT
{
    RIGHT,
    LEFT
}

/// <summary>
/// 横移動のエネミー
/// </summary>
public class CrossMoveEnemy : Enemy
{

    [SerializeField]
    private ABOUT about;


    protected override void Move()
    {
        if (IsRightMove())
        {
            rigid.velocity = new Vector2(speed, 0);
        }
        if (IsLeftMove())
        {
            rigid.velocity = new Vector2(-speed, 0);
        }
    }

    private void OnBecameInvisible()
    {
        StageOut();
    }

    /// <summary>
    /// ステージ外に行った時の処理
    /// </summary>
    private void StageOut()
    {
        //カメラの左右の位置を取得

        float cameraRange_Right = FindObjectOfType<CameraControl>().getScreenBottomRight().x;
        float cameraRange_Left = FindObjectOfType<CameraControl>().getScreenTopLeft().x;

        if (IsRightMove())
        {
            transform.position = new Vector3(cameraRange_Left, transform.position.y);
        }
        if (IsLeftMove())
        {
            transform.position = new Vector3(cameraRange_Right, transform.position.y);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
       // base.OnTriggerExit2D(collision);
        if (collision.tag == "Wall")
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private bool IsRightMove()
    {
        return about == ABOUT.RIGHT;
    }

    private bool IsLeftMove()
    {
        return about == ABOUT.LEFT;
    }

}

