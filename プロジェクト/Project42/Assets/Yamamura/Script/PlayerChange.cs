using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{

    public PlayerSmallController small;
    public PlayerBigController big;
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
                    small.Change(false);
                    big.Change(true);
                }
                else if (hitObject.collider.gameObject.name == "PlayerBig" && big.playerMode == PlayerMode.PLAYER)
                {
                    small.Change(true);
                    big.Change(false);
                }
            }
        }
    }

}
