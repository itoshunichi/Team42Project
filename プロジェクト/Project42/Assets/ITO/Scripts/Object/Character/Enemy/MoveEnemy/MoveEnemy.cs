using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveEnemyMode
{
    UP,
    DOWN,
    RIGHT,
    LEFT,
}
public abstract class MoveEnemy : Enemy
{

    protected GameObject stageWall;
    protected GameObject wallCollider;
    protected Vector2 velocity;
   protected MoveEnemyMode moveMode;

    private bool isStageOut;
    public MoveEnemyMode MoveMode
    {
        get { return moveMode; }
    }

    protected override void Start()
    {
        base.Start();
        stageWall = GameObject.Find("StageWall");
    }
    /// <summary>
    /// 移動
    /// </summary>
    protected override void Move()
    {
        rigid.velocity = velocity;

    }

    protected abstract void StageOut();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == wallCollider)
        {

            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnBecameInvisible()
    {
        StageOut();
        GetComponent<Collider2D>().enabled = false;
        isStageOut = true;
    }

    private void OnBecameVisible()
    {
        if (isStageOut)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Collider2D>().isTrigger =false;
            isStageOut = false;
        }
    }


}
