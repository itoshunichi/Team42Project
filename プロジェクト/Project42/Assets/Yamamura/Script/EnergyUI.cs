using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUI : MonoBehaviour {

    public GameObject hammer;
    public Energy energy;
    public GameObject player;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = hammer.transform.position;
        gameObject.transform.rotation = hammer.transform.rotation;
	}
}
