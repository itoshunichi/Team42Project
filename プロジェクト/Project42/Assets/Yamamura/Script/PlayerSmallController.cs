using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallController : Player
{
    HingeJoint2D joint;
    // Use this for initialization
    void Start()
    {
        flickController = flick.GetComponent<FlickController>();
        joint = GetComponent<HingeJoint2D>();
        joint.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {   //AddForce 
        if (addForceCount < 90)
        {
            addForceCount++;
            GetComponent<Rigidbody2D>().AddForce(transform.right * (power));
        }
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, ball.transform.position);
        VelocityZero(60);
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
        if (speed < speedMax) speed += 0.001f;
        transform.rotation = flick.transform.rotation;
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
            playerMode = PlayerMode.PLAYER;
            joint.enabled = true;
            GetComponent<Rigidbody2D>().mass = 1;
            GetComponent<SpriteRenderer>().sprite = eye;
        }
        else if (!isPlayer)
        {
            playerMode = PlayerMode.NONE;
            joint.enabled = false;
            GetComponent<Rigidbody2D>().mass = 0.005f;
            GetComponent<SpriteRenderer>().sprite = normal;
        }
        speed = 0;
    }

    public void RotationForce(float power)
    {
        addForceCount = 0;
        this.power *= power;
    }

    public void AddForceBall(bool isRight)
    {
        speed = 0;
        //Velocityをいったん０に
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //ボールの向きをプレイヤーに向ける
        var vec = ball.transform.position - transform.position;
        ball.transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position.normalized);
        ball.GetComponent<PlayerBigController>().Reset();
        if (isRight)
        {
            ball.GetComponent<Rigidbody2D>().AddForce(ball.transform.right * power);
            //ball.GetComponent<Rigidbody2D>().AddForce(Vector2.right * power);
        }
        else
        {
            ball.GetComponent<Rigidbody2D>().AddForce(-ball.transform.right * power);
            //ball.GetComponent<Rigidbody2D>().AddForce(Vector2.left * power);
        }
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
