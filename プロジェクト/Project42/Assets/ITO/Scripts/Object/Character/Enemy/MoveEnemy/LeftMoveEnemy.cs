using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveEnemy : MoveEnemy {
    protected override void StageOut()
    {
        float rightPosX = GameObject.Find("RightCollider").transform.position.x;
        transform.position = new Vector3(rightPosX, transform.position.y);
    }

}
