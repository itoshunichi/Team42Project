using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseUI : MonoBehaviour {

    public GameObject poseUI;

	// Use this for initialization
	void Start () {
		
	}

    public void PorsePush()
    {
        poseUI.SetActive(true);
    }

    public void ContinuePush()
    {
        poseUI.SetActive(false);
    }
}
