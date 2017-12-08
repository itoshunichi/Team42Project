using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveEnemy : Enemy {

    protected GameObject stageWall;
    protected GameObject wallCollider;
    protected Vector2 velocity;
    private void Awake()
    {
       
    }
    protected override void Start()
    {      
        base.Start();
        stageWall = GameObject.Find("StageWall");
    }
    protected override void Move()
    {
        rigid.velocity = velocity;
        //transform.position += Direction() * speed * Time.deltaTime;
        
    }

    protected abstract void StageOut();

    protected  void OnTriggerExit2D(Collider2D collision)
    {
        if (IsSelectMode(EnemyMode.TARKING)) return;
        if (collision.gameObject == wallCollider)
        {
            Debug.Log("ステージ外");
            StageOut();
        }
    }

    //protected override void OnBecameInvisible()
    //{
    //    base.OnBecameInvisible();
        
    //}
    
   
}
