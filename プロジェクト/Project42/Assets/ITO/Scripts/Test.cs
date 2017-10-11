using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(1600,900, false);       
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
