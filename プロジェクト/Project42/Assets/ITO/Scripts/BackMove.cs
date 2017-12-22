using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove : MonoBehaviour {


    [SerializeField]
    private float speed = 5f;

    //[SerializeField]
    //private bool isMove = false;

    private Vector3 startLoopPosition;
    //public bool IsMove
    //{
    //    set { isMove = value; }
    //}

   
	void Start () {
        //back = transform.GetChild(0).gameObject;
	}
	
	void Update () {
        Move();
	}

    private void Move()
    {
        //if (!isMove) return;
        Vector2 pos = transform.position;
        pos.y -= speed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnBecameVisible()
    {
        //isMove = true;
       
        
    }

    private void SetLoopStartPosition()
    {
       
    }

    private void OnBecameInvisible()
    {
        Vector2 pos = transform.position;
        pos.y = Camera.main.transform.position.y + 42.2f;
        transform.position = pos;

    }
}
