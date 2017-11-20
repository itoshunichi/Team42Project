using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour {

    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        
	}

    //移動処理
    private void Move()
    {
        gameObject.transform.position = target.GetComponent<HammerMove>().GetLerpPos();
        gameObject.transform.rotation = target.transform.rotation;
    }
}
