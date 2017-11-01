using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigController : Player
{
    HingeJoint2D joint;
    public Energy soulEnergy;

    // Use this for initialization
    void Start()
    {
        flickController = flick.GetComponent<FlickController>();
        joint = GetComponent<HingeJoint2D>();
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (addForceCount < 90)
        {
            addForceCount++;
            var force = transform.right * (power);
            addForceAlpha += 0.01f;
            GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(force, Vector2.zero, addForceAlpha));
        }

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
        transform.rotation = flick.transform.rotation;
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
            playerMode = PlayerMode.PLAYER;
            joint.enabled = true;
            GetComponent<Rigidbody2D>().mass = 1;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (!isPlayer)
        {
            GetComponent<SpriteRenderer>().sprite = normal;
            playerMode = PlayerMode.HAMMER;
            joint.enabled = false;
            GetComponent<Rigidbody2D>().mass = 0.005f;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        speed = 0;
    }

    public void RotationForce(float power)
    {
        addForceCount = 0;
        addForceAlpha = 0;
        this.power *= power;
    }

    //public void AddForceBall(bool isRight)
    //{
    //    speed = 0;
    //    //Velocityをいったん０に  
    //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //    //ボールの向きをプレイヤーに向ける
    //    var vec = ball.transform.position - transform.position;
    //    ball.transform.rotation = Quaternion.LookRotation(Vector3.forward, vec.normalized);
    //    ball.GetComponent<PlayerSmallController>().Reset();
    //    if (isRight)
    //    {
    //        ball.GetComponent<Rigidbody2D>().AddForce(ball.transform.right * power);
    //    }
    //    else
    //    {
    //        ball.GetComponent<Rigidbody2D>().AddForce(-ball.transform.right * power);
    //    }
    //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //}

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

        else if (col.gameObject.tag == "BeDestroyedObject" && playerMode == PlayerMode.HAMMER && transform.GetComponent<Rigidbody2D>().velocity !=Vector2.zero)
        {
            Destroy(col.gameObject);
            soulEnergy.AddEnergy(10);
        }
    }

   
}
