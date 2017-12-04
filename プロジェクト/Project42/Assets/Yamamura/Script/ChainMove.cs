using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMove : MonoBehaviour
{
    private float speed;
    public GameObject player;
    public Hammer hammer;
    PlayerSmallController playerSC;
    Player_StageOut stageOut;
    public GameObject point;
    float alpha = 1;
    public float alphaPuls;
    // Use this for initialization
    void Start()
    {
        playerSC = player.GetComponent<PlayerSmallController>();
        stageOut = player.GetComponent<Player_StageOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (//!stageOut.IsStageOut() && !playerSC.GetHit() && 
            !hammer.VelocityCount())
        {
            alpha += alphaPuls;
            transform.position = Vector2.Lerp(transform.position, point.transform.position, alpha);
        }
        else if (hammer.VelocityCount())
            alpha = 0;
    }

    public void Reset()
    {
        alpha = 0;
    }
}
