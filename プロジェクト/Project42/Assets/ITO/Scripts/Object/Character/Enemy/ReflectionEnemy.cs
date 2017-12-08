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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
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
