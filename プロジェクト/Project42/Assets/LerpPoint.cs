using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPoint : MonoBehaviour {
    //スクリプト
    public Hammer hammer;
    public PlayerSmallController playerController;  //プレイヤーコントローラー
    public Player_StageOut stageOut;                //ステージアウト
    //GameObject
    public GameObject point;                        //Lerpしたい場所
    //float
    float alpha = 1;                                //Lerpのアルファ値
    public float alphaPuls;                         //アルファ値を足していく値
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {   //ガタつきを抑えるために指定した位置にLerpする
        if (//!stageOut.IsStageOut() &&!playerSC.GetHit() && 
            !hammer.VelocityCount())
        {
            alpha += alphaPuls;
            transform.position = Vector2.Lerp(transform.position, point.transform.position, alpha);
        }
        if (playerController.GetAccelerator() > 0)
        {
            alpha = 1.0f;
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
