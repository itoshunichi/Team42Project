using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigController : Player
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMode == PlayerMode.PLAYER && !GetComponent<PlayerCollision>().GetIsHit())
        {
            RotationMove();
        }
    }


    private void RotationMove()
    {
        //if (transform.rotation.z != arrow.transform.rotation.z) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.rotation = arrow.transform.rotation;
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);
        //

        dir = new Vector3(Mathf.Sin(angleDirection), -Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
    }

    public void AddForceBall(bool isRight)
    {

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
}
