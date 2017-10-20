using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttack : MonoBehaviour
{ 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);

            if (!hitObject) return;
            if (hitObject.collider.tag == "Boss"||hitObject.collider.tag == "Enemy")
            {
               // hitObject.collider.gameObject.GetComponent<Enemy>().AddDamagePoint();
               // hitObject.collider.gameObject.GetComponent<Enemy>().BeginDamage(1);
            }
        }

    }

  
}
