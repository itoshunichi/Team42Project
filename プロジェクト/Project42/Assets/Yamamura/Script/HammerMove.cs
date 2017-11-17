using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMove : MonoBehaviour {

    public PlayerSmallController playerSC;
    public Player_StageOut stageOut;
    public GameObject point;
    float alpha;
    Hammer hammer;
	// Use this for initialization
	void Start () {
        hammer = GetComponent<Hammer>();
	}

    public void Move()
    {
        if (!stageOut.IsStageOut() || !playerSC.GetHit() || !hammer.VelocityCountUp())
        {
            alpha += 0.01f;
            transform.position = Vector2.Lerp(transform.position, point.transform.position, alpha);
        }
    }

    public void Reset()
    {
        alpha = 0;
    }
}
