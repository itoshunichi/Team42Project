using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveEnemy : MoveEnemy{

    //GameObject leftCollider;

    protected override void Start()
    {
        base.Start();
        velocity = new Vector2(speed, 0);
        wallCollider = GameObject.Find("RightCollider");
    }
    protected override void StageOut()
    {
        transform.position = new Vector3(-wallCollider.transform.position.x, transform.position.y);
    }

    
}
