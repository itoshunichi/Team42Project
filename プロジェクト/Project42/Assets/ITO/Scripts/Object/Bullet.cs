using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletMode
{
    UP,
    RIGHT,
    LEFT,
    DOWN,
}

public class Bullet : MonoBehaviour {

   public BulletMode mode;

    [SerializeField,Range(1f,5f)]
   float speed;

	void Start () {
        
	}
	
	void Update () {
        Move();
	}

    private void Move()
    {
        Vector2 pos = transform.position;
        if (mode == BulletMode.UP) pos.y += speed * Time.deltaTime;
        if (mode == BulletMode.DOWN) pos.y -= speed * Time.deltaTime;
        if (mode == BulletMode.RIGHT) pos.x += speed * Time.deltaTime;
        if (mode == BulletMode.LEFT) pos.x -= speed * Time.deltaTime;

        transform.position = pos;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall") Destroy(gameObject);
    }

   
}
