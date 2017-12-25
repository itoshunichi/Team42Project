using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarkingEnemy : Enemy
{

    protected override void Move()
    {
        TrackingPlayer();
    }


    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    private void TrackingPlayer()
    {

        //プレイヤーの方を向く
        LookPlayer();
        //ラジアン
        float rad = Mathf.Atan2(player.transform.position.y - transform.position.y,
            player.transform.position.x - transform.position.x);

        Vector2 pos = transform.position;
        pos.x += speed * Mathf.Cos(rad) * Time.deltaTime;
        pos.y += speed * Mathf.Sin(rad) * Time.deltaTime;
        transform.position = pos;

    }

    /// <summary>
    /// プレイヤーの方を向く
    /// </summary>
    private void LookPlayer()
    {
        Vector2 vec = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, vec);
    }

}
