using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour {

    public GameObject touchEffect;
    
    private void Awake() 
    {
        //シーンを跨いでも消えないように
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var touch = Instantiate(touchEffect, Input.mousePosition, touchEffect.transform.rotation);
            touch.transform.parent = transform;
        }
	}
}
