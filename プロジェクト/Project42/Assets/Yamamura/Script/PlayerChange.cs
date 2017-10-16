using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{

    public PlayerController small;
    public PlayerController big;
    public GameObject[] chains;

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
                    small.playerMode = PlayerMode.NONE;
                    small.GetComponent<Rigidbody2D>().mass = 0.005f;
                    big.playerMode = PlayerMode.PLAYER;
                    Debug.Log("SmallTap");
                  
                }
                else if (hitObject.collider.gameObject.name == "PlayerBig" && big.playerMode == PlayerMode.PLAYER)
                {
                    small.playerMode = PlayerMode.PLAYER;
                    big.playerMode = PlayerMode.NONE;
                    big.GetComponent<Rigidbody2D>().mass = 0.005f;
                    Debug.Log("BigTap");

                }
            }
        }
    }

}
