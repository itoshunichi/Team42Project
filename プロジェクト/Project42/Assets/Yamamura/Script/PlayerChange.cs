using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{

    public PlayerSmallController small;
    public PlayerBigController big;
    public GameObject bigPlayer;
    public GameObject smallPlayer;
    public GameObject[] chains;
    public FlickController flick;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerTapChange();
    }

    //プレイヤーにタップしたら
    private void PlayerTapChange()
    {
        Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(tapPoint);
        if (Input.GetMouseButtonDown(0))
        {
            if (col)
            {
                var hitObject = Physics2D.Raycast(tapPoint, Vector2.zero);

                //プレイヤー
                if (hitObject.collider.gameObject.name == "PlayerSmall" && small.playerMode == PlayerMode.PLAYER)
                {
                    small.Change(false);
                    big.Change(true);
                    flick.AddRotation(360);
                }
                else if (hitObject.collider.gameObject.name == "PlayerBig" && big.playerMode == PlayerMode.PLAYER)
                {
                    small.Change(true);
                    big.Change(false);
                    flick.AddRotation(360);
                }
            }
        }
    }

    private void Chain()
    {
        //smallがプレイヤーの時
        //ジョイントのつなぎ　bigPlayer -> chains -> smallPlayer
        for (int i = 0; i < chains.Length - 1; i++)
        {
            chains[i].GetComponent<HingeJoint2D>().connectedBody = chains[i + 1].GetComponent<Rigidbody2D>();
        }
        chains[chains.Length].GetComponent<HingeJoint2D>().connectedBody = bigPlayer.GetComponent<Rigidbody2D>();

        //bigがプレイヤーの時  
        //ジョイントのつなぎ　smallPlayer -> chains -> bigPlayer
        for (int i = 1; i < chains.Length; i++)
        {
            chains[i].GetComponent<HingeJoint2D>().connectedBody = chains[i - 1].GetComponent<Rigidbody2D>();
        }
        chains[0].GetComponent<HingeJoint2D>().connectedBody = smallPlayer.GetComponent<Rigidbody2D>();
    }

}
