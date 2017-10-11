using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
   NONE,
   PLAYER,
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector2 movePosition;

    //タッチ移動
    
    public float power = 10f;


    public PlayerMode playerMode;                 //自機速度の段階
    public GameObject ball;
    public GameObject arrow;

    // Use this for initialization
    void Start()
    {
        speed = 0.04f;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerMode == PlayerMode.NONE) return;
        PlayerChange();
        RotationMove();

        //  Move();
    }

    private void RotationMove()
    {
        if (Input.GetMouseButtonUp(0)) transform.rotation = arrow.transform.rotation;
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);
        //
        Vector3 dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
    }

    //プレイヤーにタップしたら
    private void PlayerChange()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                var hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);

                //プレイヤー
                if (hitObject.collider.gameObject.tag == "Player")
                {
                    playerMode = PlayerMode.NONE;
                    ball.GetComponent<PlayerController>().playerMode = PlayerMode.PLAYER;
                    Debug.Log("Hit");
                }

            }
        }
    }

    //線より右側
    bool LineRight(Vector2 pos1,Vector2 pos2,Vector2 dot)
    {
        Vector2 v1 = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y);
        Vector2 v2 = new Vector2(dot.x - pos1.x, dot.y - pos1.y);
        float det = v1.x * v2.y - v2.x * v1.y;
        if (det < 0) return true;
        return false;
    }
}
