using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMove : MonoBehaviour {

    public PlayerSmallController playerSC;
    public Player_StageOut stageOut;
    public GameObject point;
    float alpha = 1;
    public float alphaPuls;
    Hammer hammer;
	// Use this for initialization
	void Start () {
        hammer = GetComponent<Hammer>();
	}

    public void Move()
    {
        if (//!stageOut.IsStageOut() &&!playerSC.GetHit() && 
            !hammer.VelocityCount())
        {
            alpha += alphaPuls;
            transform.position = Vector2.Lerp(transform.position, point.transform.position, alpha);
        }
    }

    public Vector2 GetLerpPos()
    {
        return Vector3.Lerp(transform.position, point.transform.position, alpha);
    }

    public void Reset()
    {
        alpha = 0;
    }
}
