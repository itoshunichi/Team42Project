using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionEnemy : Enemy
{

    protected override void Start()
    {
        base.Start();
        SetVelocity();
    }


    private void SetVelocity()
    {
        Vector3 vel = Vector3.zero;
        float degree = UnityEngine.Random.Range(0, 360);
        vel.x = speed * Mathf.Cos(degree * Mathf.PI / 180f);
        vel.y = speed * Mathf.Sin(degree * Mathf.PI / 180f);
        rigid.velocity = vel;
    }

    protected override void Move()
    {
        if(rigid.velocity == Vector2.zero)
        {
            SetVelocity();
        }
    }
}
