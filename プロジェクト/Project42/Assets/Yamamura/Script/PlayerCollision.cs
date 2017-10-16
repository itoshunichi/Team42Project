using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public float collisionPower;
    bool isHit;
    public int noMoveCount;
    int count;
    PlayerSmallController pc;
	// Use this for initialization
	void Start () {
        pc = GetComponent<PlayerSmallController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isHit)
        {
            count++;
            if (count > noMoveCount)
            {
                count = 0;
                isHit = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && pc.playerMode == PlayerMode.PLAYER)
        {
            isHit = true;
            Debug.Log("HIT");
            Vector2 dir = transform.position - col.gameObject.transform.position;

            float angleDirection = col.gameObject.transform.eulerAngles.z * (Mathf.PI / 180.0f);
            //Vector3 dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(dir * collisionPower);
            
        }
    }

    public bool GetIsHit()
    {
        return isHit;
    }
}
