using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMove : MonoBehaviour
{

    public GameObject[] players;
    Vector3 position;
    int playerNumber;
    bool isMoveSmall = true;
    float alpha;
    // Use this for initialization
    void Start()
    {
        position = transform.position;
        playerNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
       // Debug.Log(playerNumber);
        if (isMoveSmall)
        {
            FromSmall();
        }
        else if(!isMoveSmall)
        {
            FromBig();
        }
        alpha += 0.02f;
        transform.position = Vector2.MoveTowards(transform.position, players[playerNumber].transform.position,10);
    }

    private void FromSmall()
    {   //
        if (Vector2.Distance(transform.position,players[playerNumber].transform.position) < 0.3 && playerNumber > 0) 
            playerNumber -= 1;
    }

    private void FromBig()
    {   //
        if (Vector2.Distance(transform.position, players[playerNumber].transform.position) < 0.3 && playerNumber < players.Length - 1)
            playerNumber += 1;
    }

    public void SetMove(bool isMoveSmall)
    {
        this.isMoveSmall = isMoveSmall;
        if (isMoveSmall) playerNumber = players.Length - 1;
        else if (!isMoveSmall) playerNumber = 0;
    }
}
