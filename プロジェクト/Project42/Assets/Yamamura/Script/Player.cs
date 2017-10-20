using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    NONE,
    PLAYER,
}

public abstract class Player : MonoBehaviour {
   
    public PlayerMode playerMode;   //自機速度の段階
    protected Vector3 dir;          //進行方向の位置
    public float speed;             //移動速度
    public float power;             //振る力
    public GameObject ball;         //操作していないプレイヤー
    public GameObject flick;        //矢印
    protected FlickController flickController;
    protected bool isHit;           //
    protected int count;            //
    public float collisionPower;    //
    public int noMoveCount;         //
    public GameObject[] chains;

    protected void NotMoveCount()
    {
        if (isHit)
        {
            count++;
            if (count > noMoveCount)
            {
                count = 0;
                isHit = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
