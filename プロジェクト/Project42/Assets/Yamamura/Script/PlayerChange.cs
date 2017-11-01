using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{

    PlayerSmallController small;
    PlayerBigController big;
    public GameObject bigPlayer;
    public GameObject smallPlayer;
    public GameObject[] chains;
    public FlickController flick;

    public SoulMove soul;
    // Use this for initialization
    void Start()
    {
        small = smallPlayer.GetComponent<PlayerSmallController>();
        big = bigPlayer.GetComponent<PlayerBigController>();
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
                    flick.SetRotation(-small.GetDirectionVector());
                    
                    soul.SetMove(false);
                    BigPlayerChain();
                }
                else if (hitObject.collider.gameObject.name == "PlayerBig" && big.playerMode == PlayerMode.PLAYER)
                {
                    small.Change(true);
                    big.Change(false);
                    flick.SetRotation(-big.GetDirectionVector());
                    soul.SetMove(true);
                    SmallPlayerChain();
                }
            }
        }
    }
    //ジョイントの付け替え
    private void SmallPlayerChain()
    {

        for (int i = 0; i < chains.Length - 1; i++)
        {
            chains[i].GetComponent<HingeJoint2D>().connectedBody = chains[i + 1].GetComponent<Rigidbody2D>();
        }
        chains[chains.Length - 1].GetComponent<HingeJoint2D>().connectedBody = bigPlayer.GetComponent<Rigidbody2D>();
        foreach (var chain in chains)
        {
            chain.GetComponent<HingeJoint2D>().anchor = new Vector2(0, -0.1f);
            chain.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0.1f);
        }
        
    }
    //ジョイントの付け替え
    private void BigPlayerChain()
    {
        for (int i = chains.Length -1; i > 0; i--)
        {
            chains[i].GetComponent<HingeJoint2D>().connectedBody = chains[i - 1].GetComponent<Rigidbody2D>();
        }
        chains[0].GetComponent<HingeJoint2D>().connectedBody = smallPlayer.GetComponent<Rigidbody2D>();
        foreach (var chain in chains)
        {
            chain.GetComponent<HingeJoint2D>().anchor = new Vector2(0, 0.1f);
            chain.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -0.1f);
        }
        chains[0].GetComponent<HingeJoint2D>().anchor = new Vector2(0, 0.12f);
        chains[0].GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0.1f);
    }

}
