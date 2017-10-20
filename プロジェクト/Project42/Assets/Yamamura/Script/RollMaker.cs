using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMaker : MonoBehaviour {

    public GameObject playerSmall;
    public GameObject playerBig;
    PlayerSmallController psc;
    PlayerBigController pbc;

    float rotationZ = 60;

	// Use this for initialization
	void Start () {
        psc = playerSmall.GetComponent<PlayerSmallController>();
	}
	
	// Update is called once per frame
	void Update () {
    //    Pos();
    //    AddRotation();
	}

    private void Pos()
    {
        if (psc.playerMode == PlayerMode.PLAYER) transform.position = playerSmall.transform.position;
        else if (psc.playerMode == PlayerMode.NONE) transform.position = playerBig.transform.position;
        
    }

    public void AddRotation()
    {

        transform.rotation = new Quaternion(0, 0, rotationZ, transform.rotation.w);
        rotationZ -=10;
    }
}
