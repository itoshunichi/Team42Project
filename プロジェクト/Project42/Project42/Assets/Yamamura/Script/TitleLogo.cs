using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour {

    public GameObject optionCanvas;
    public Shake camera;
	// Use this for initialization
	void Start () {
        optionCanvas.SetActive(false);
        GetComponent<Animator>().SetBool("IsStart", true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && GetComponent<Animator>().GetBool("IsStart")) {
            optionCanvas.SetActive(true);
            GetComponent<Animator>().SetBool("IsStart", false);
        }	
	}

    public void Shake()
    {
        camera.ShakeObject();
    }

    public void AnimEnd()
    {
        optionCanvas.SetActive(true);
        GetComponent<Animator>().SetBool("IsStart", false);
    }
}
