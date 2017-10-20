﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigController : Player
{

    Quaternion rotation = Quaternion.identity;
    float s;
    bool roll;
    float rollSpeed;
    // Use this for initialization
    void Start()
    {
        flickController = flick.GetComponent<FlickController>();
        s = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, ball.transform.position);
        NotMoveCount();
        Move();
    }
 

    private void Move()
    {

        if (playerMode == PlayerMode.PLAYER && !isHit)
        {
            RotationMove();
        }
    }

    private void RotationMove()
    {
        if (transform.rotation.z != flick.transform.rotation.z) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(flickController.GetFlick())transform.rotation = flick.transform.rotation;
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);
        //

        //dir = new Vector3(Mathf.Sin(angleDirection), -Mathf.Cos(angleDirection), 0.0f);
        dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
    }

    public void Change(bool isPlayer)
    {
        if (isPlayer)
        {
            transform.rotation = rotation = new Quaternion(0, 0, ball.transform.rotation.z + 180, transform.rotation.w); ;
            playerMode = PlayerMode.PLAYER;
            GetComponent<Rigidbody2D>().mass = 1;
        }
        else if(!isPlayer)
        {
            playerMode = PlayerMode.NONE;
            GetComponent<Rigidbody2D>().mass = 0.005f;
        }
    }
   
    public void AddForceBall(bool isRight)
    {
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (isRight)
        {
            ball.GetComponent<Rigidbody2D>().AddForce(Vector2.right * power);
        }
        else
        {
            ball.GetComponent<Rigidbody2D>().AddForce(Vector2.left * power);
       }
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public void Roll()
    {
        roll = true;
        rollSpeed = 30;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "BeDestroyedObject" && playerMode == PlayerMode.PLAYER)
        {
            isHit = true;
            Debug.Log("HIT");
            Vector2 dir = transform.position - col.gameObject.transform.position;

            float angleDirection = col.gameObject.transform.eulerAngles.z * (Mathf.PI / 180.0f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(dir * collisionPower);

        }
    }
}
