using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFadeOut : MonoBehaviour {

    Color color;

	void Start () {
        color = GetComponent<Image>().color;
	}
	
	// Update is called once per frame
	void Update () {
        Fade();
	}

    private void Fade()
    {
        color.a -= 0.05f;
        GetComponent<Image>().color = color;
    }
}
