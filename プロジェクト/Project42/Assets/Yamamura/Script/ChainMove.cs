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
    float alpha;
    // Use this for initialization
    void Start()
    {
        playerSC = player.GetComponent<PlayerSmallController>();
        stageOut = player.GetComponent<Player_StageOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stageOut.IsStageOut() || !playerSC.GetHit() || !hammer.VelocityCountUp())
        {
            alpha += 0.01f;
            transform.position = Vector2.Lerp(transform.position, point.transform.position, alpha);
        }
        
        speed = playerSC.GetSpeed();
       // Move();
    }

    private void Move()
    {
        if (!stageOut.IsStageOut() || !playerSC.GetHit() || hammer.VelocityCountUp())
        {
            transform.rotation = player.transform.rotation;
            float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);

            var dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
            transform.position += dir * speed;
        }
    }

    public void Reset()
    {
        alpha = 0;
    }
}
