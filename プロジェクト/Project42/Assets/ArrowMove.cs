using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour {

    Vector3 dir;
    public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);

        dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        transform.position += dir * speed;
	}

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
