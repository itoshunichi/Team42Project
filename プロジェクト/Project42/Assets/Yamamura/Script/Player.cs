﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    HAMMER,
    PLAYER,
}

public abstract class Player : MonoBehaviour {
    
    protected Vector3 dir;          //進行方向の位置
    protected float speed;             //移動速度
    public float speedMax;          //限界速度
    public GameObject flick;        //矢印
    protected FlickController flickController;
    protected bool isHit;           //
    protected int hitCount;         //
    public float collisionPower;    //
    public int noMoveCount;         //

    protected void NotMoveCount()
    {
        if (isHit)
        {
            hitCount++;
            if (hitCount > noMoveCount)
            {
                hitCount = 0;
                isHit = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void Reset()
    {
        speed = 0;
    }

    public Vector3 GetDirectionVector()
    {
        return dir;
    }
}
