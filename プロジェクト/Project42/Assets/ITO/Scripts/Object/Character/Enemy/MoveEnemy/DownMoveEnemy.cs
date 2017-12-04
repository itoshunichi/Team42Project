using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMoveEnemy : MoveEnemy {
    protected override void StageOut()
    {
        float topPosY = GameObject.Find("TopCollider").transform.position.y;
        transform.position = new Vector3(transform.position.x, topPosY);
    }
}
