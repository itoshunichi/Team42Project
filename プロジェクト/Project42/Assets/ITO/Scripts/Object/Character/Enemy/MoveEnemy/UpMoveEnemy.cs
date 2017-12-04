using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMoveEnemy : MoveEnemy {
    protected override void StageOut()
    {
        float bottomPosY = GameObject.Find("BottomCollider").transform.position.y;
        transform.position = new Vector3(transform.position.x, bottomPosY);
    }

    
}
