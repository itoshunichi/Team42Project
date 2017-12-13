using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHaloLayer : MonoBehaviour {

    private Component halo;

	// Use this for initialization
	void Start () {
        halo = gameObject.GetComponent("Halo");
        halo.GetComponent<Renderer>().sortingLayerName = "Default";
        halo.GetComponent<Renderer>().sortingOrder = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
