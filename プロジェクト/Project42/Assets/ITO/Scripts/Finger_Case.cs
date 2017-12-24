using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger_Case : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(FindObjectOfType<FlickController>())
        {
            if(FindObjectOfType<FlickController>().FlickCount >= 1)
            {
                Destroy(gameObject);
            }
           
        }
		
	}
}
