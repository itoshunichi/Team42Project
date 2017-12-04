using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 上下
/// </summary>
enum CONTEXT
{
    UP,
    DOWN,
}

/// <summary>
/// 縦に移動するエネミー
/// </summary>
public class VerticalMoveEnemy : Enemy
{

    [SerializeField]
    private CONTEXT context;


    protected override void Move()
    {
        if (IsUpMove())
        {
            rigid.velocity = new Vector2(0, speed);
        }
        if (IsDownMove())
        {
            rigid.velocity = new Vector2(0, -speed);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.tag == "Wall")
        {
            //GetComponent<Collider2D>().isTrigger = false;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            // GetComponent<Collider2D>().isTrigger = true;
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
        //カメラの上下の位置を取得
        float cameraRange_Bottom = FindObjectOfType<CameraControl>().getScreenBottomRight().y;
        float cameraRange_Top = FindObjectOfType<CameraControl>().getScreenTopLeft().y;

        if (IsUpMove())
        {
            transform.position = new Vector3(transform.position.x, cameraRange_Bottom);
        }
        if (IsDownMove())
        {
            transform.position = new Vector3(transform.position.x, cameraRange_Top);
        }
    }

    private bool IsUpMove()
    {
        return context == CONTEXT.UP;
    }

    private bool IsDownMove()
    {
        return context == CONTEXT.DOWN;
    }

}
