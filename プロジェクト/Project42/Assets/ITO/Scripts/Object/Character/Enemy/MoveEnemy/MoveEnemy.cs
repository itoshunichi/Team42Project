using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveEnemy : Enemy {

    

    private void Awake()
    {
       
    }
    protected override void Start()
    {
       
        base.Start();
        
    }
    protected override void Move()
    {
        transform.position += Direction() * speed * Time.deltaTime;
        
    }

    protected abstract void StageOut();

    /// <summary>
    /// 画面から出たら
    /// </summary>
    private void OnBecameInvisible()
    {
        StageOut();
    }

   
}
