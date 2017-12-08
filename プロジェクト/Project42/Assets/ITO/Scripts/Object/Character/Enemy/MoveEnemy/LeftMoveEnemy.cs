using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveEnemy : MoveEnemy {

  //  GameObject rightCollider;

    protected override void Start()
    {
        base.Start();
        velocity = new Vector2(-speed, 0);
        wallCollider = GameObject.Find("LeftCollider");
    }
    protected override void StageOut()
    {
        transform.position = new Vector3(-wallCollider.transform.position.x, transform.position.y);
    }

}
