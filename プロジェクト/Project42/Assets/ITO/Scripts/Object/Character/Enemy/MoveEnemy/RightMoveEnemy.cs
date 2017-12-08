using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveEnemy : MoveEnemy{
    protected override void StageOut()
    {
        float leftPosX = GameObject.Find("LeftCollider").transform.position.x;
        transform.position = new Vector3(leftPosX, transform.position.y);
    }

    
}
