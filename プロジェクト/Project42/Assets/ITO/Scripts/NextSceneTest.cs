using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTest : MonoBehaviour {


    [SerializeField]
    private string nextSceneName;
	// Use this for initialization
	void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {

        if (SceneNavigater.Instance.IsFading) return;
        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_KETTEI);
            SceneNavigater.Instance.Change(nextSceneName);
        }
		
	}
}
