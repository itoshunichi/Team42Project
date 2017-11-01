using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    HAMMER,
    PLAYER,
}

public abstract class Player : MonoBehaviour {
   
    public PlayerMode playerMode;   //自機速度の段階
    protected Vector3 dir;          //進行方向の位置
    protected float speed;             //移動速度
    public float speedMax;          //限界速度
    public float power;             //振る力
    //public GameObject ball;         //操作していないプレイヤー
    public GameObject flick;        //矢印
    protected FlickController flickController;
    protected bool isHit;           //
    protected int hitCount;         //
    public float collisionPower;    //
    public int noMoveCount;         //
    public Sprite normal;
    public Sprite eye;
    protected float angleDirection;
    protected float countVelocity = 120;
    protected int addForceCount = 120;
    protected float addForceAlpha = 0;

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

    protected void VelocityZero(int countMax)
    {
        if (playerMode == PlayerMode.HAMMER)
        {
            countVelocity++;
            if (countVelocity > countMax)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    public void Reset()
    {
        countVelocity = 0;
        speed = 0;
        addForceCount = 0;
        addForceAlpha = 0;
    }

    public Vector3 GetDirectionVector()
    {
        return dir;
    }
}
