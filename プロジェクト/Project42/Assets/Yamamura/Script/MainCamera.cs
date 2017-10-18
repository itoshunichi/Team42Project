using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public GameObject playerSmall;  //プレイヤースモール
    public GameObject playerBig;    //プレイヤービッグ
    PlayerSmallController smallC;        //プレイヤースモールのスクリプト
  
	// Use this for initialization
	void Start () {
        smallC = playerSmall.GetComponent<PlayerSmallController>();
    }
	
	// Update is called once per frame
    void Update()
    {
        if (smallC.playerMode == PlayerMode.PLAYER)
        {
            transform.position = new Vector3(playerSmall.transform.position.x, playerSmall.transform.position.y, -10);
        }
        else if (smallC.playerMode == PlayerMode.NONE)
        {
            transform.position = new Vector3(playerBig.transform.position.x, playerBig.transform.position.y, -10);
        }
    }
}
