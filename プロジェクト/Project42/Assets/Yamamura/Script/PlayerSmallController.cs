using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallController : Player
{
    HingeJoint2D joint;
    public Energy soulEnergy;
    // Use this for initialization
    void Start()
    {
        flickController = flick.GetComponent<FlickController>();
        joint = GetComponent<HingeJoint2D>();
        joint.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        NotMoveCount();
        Move();
    }

    private void Move()
    {
        if (!isHit)
        {
            RotationMove();
        }
    }

    //向きに対して移動
    private void RotationMove()
    {
        if (transform.rotation.z != flick.transform.rotation.z) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (speed < speedMax) speed += 0.002f;
        transform.rotation = flick.transform.rotation;
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);

        dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
    }

   
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject")
        {
            isHit = true;
            speed = 0;
            Debug.Log("HIT");
            Vector2 dir = transform.position - col.gameObject.transform.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(dir * collisionPower);
        }
    }

}
