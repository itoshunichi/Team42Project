using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMoveEnemy : MoveEnemy {

    //GameObject topCollider;

    protected override void Start()
    {
        base.Start();
        moveMode = MoveEnemyMode.DOWN;
        velocity = new Vector2(0, -speed);
        wallCollider = GameObject.Find("BottomCollider");
    }
    protected override void StageOut()
    {
       // Debug.Log(-wallCollider.transform.position);
        transform.position = new Vector3(transform.position.x, GameObject.Find("TopCollider").transform.position.y);
    }
}
