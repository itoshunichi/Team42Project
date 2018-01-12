using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMoveEnemy : MoveEnemy {

    //GameObject bottomCollider;
    protected override void Start()
    {
        base.Start();
        moveMode = MoveEnemyMode.UP;
        velocity = new Vector2(0, speed);
        wallCollider = GameObject.Find("TopCollider");

    }
    protected override void StageOut()
    {
        transform.position = new Vector3(transform.position.x, GameObject.Find("BottomCollider").transform.position.y);
    }

    
}
