using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<Text>().text = GameObject.Find("Panel").GetComponent<RectTransform>().sizeDelta.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
