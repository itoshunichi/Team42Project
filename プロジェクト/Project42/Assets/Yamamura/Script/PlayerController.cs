using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
   NONE,
   PLAYER,
}

public enum PlayerType
{
    SMALL,
    BIG,
}

public class PlayerController : MonoBehaviour
{
    public float speed;     //プレイヤースピード
    public float power;     //振り回す力
   
    public PlayerMode playerMode;                 //自機速度の段階
    public PlayerType playerType;                   //プレイヤータイプ
    public GameObject ball;
    public GameObject arrow;
    Vector3 dir;    //進行方向の位置

    // Use this for initialization
    void Start()
    {
        speed = 0.04f;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerMode == PlayerMode.NONE) return;
        RotationMove();
        Mass();
   }

    private void RotationMove()
    {
        transform.rotation = arrow.transform.rotation;
         //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);
        //
     
        if(playerType == PlayerType.BIG)dir = new Vector3(Mathf.Sin(angleDirection), -Mathf.Cos(angleDirection), 0.0f);
        else if(playerType == PlayerType.SMALL) dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
    }

    private void Mass()
    {
        if (playerMode == PlayerMode.PLAYER) GetComponent<Rigidbody2D>().mass = 1;
        else if (playerMode == PlayerMode.NONE) GetComponent<Rigidbody2D>().mass = 0.005f;
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
    }
}
