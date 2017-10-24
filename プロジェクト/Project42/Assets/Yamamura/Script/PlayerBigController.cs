using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigController : Player
{

    Quaternion rotation = Quaternion.identity;

    // Use this for initialization
    void Start()
    {
        flickController = flick.GetComponent<FlickController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (speed < speedMax) speed += 0.001f;
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);
     
        dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
    }

    public void Change(bool isPlayer)
    {
        if (isPlayer)
        {
            GetComponent<SpriteRenderer>().sprite = eye;
            transform.rotation = rotation = new Quaternion(0, 0, ball.transform.rotation.z + 180, transform.rotation.w); ;
            playerMode = PlayerMode.PLAYER;
            GetComponent<Rigidbody2D>().mass = 1;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if(!isPlayer)
        {
            GetComponent<SpriteRenderer>().sprite = normal;
            playerMode = PlayerMode.NONE;
            GetComponent<Rigidbody2D>().mass = 0.005f;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        speed = 0;
    }
   
    public void AddForceBall(bool isRight)
    {
        speed = 0;
        //Velocityをいったん０に  
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //ボールの向きをプレイヤーに向ける
        ball.transform.rotation = Quaternion.LookRotation(Vector3.forward, ball.transform.position - transform.position);
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
   
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && playerMode == PlayerMode.PLAYER)
        {
            isHit = true;
            Debug.Log("HIT");
            Vector2 dir = transform.position - col.gameObject.transform.position;

            angleDirection = col.gameObject.transform.eulerAngles.z * (Mathf.PI / 180.0f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(dir * collisionPower);

        }
    }
}
