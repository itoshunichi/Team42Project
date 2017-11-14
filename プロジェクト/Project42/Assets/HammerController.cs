using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour {

    public Shake shake;
    public Energy energy;
    public Hammer hammer;
    public HammerMove hammerMove;
    public HammerSpriteChange hammerSprite;
    public float DestroyValue;
	// Use this for initialization
	void Start () {
  	}
	
	// Update is called once per frame
	void Update () {
        hammer.HammerUpdate();
        hammerMove.Move();
        hammerSprite.SpriteChange();
	}

    public void Reset()
    {
        hammer.Reset();
        hammerMove.Reset();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue)
        {
            Time.timeScale = 0.7f;
            shake.ShakeObject();
            Debug.Log(col.gameObject.name);
            if (col.gameObject.GetComponent<BeDestroyedObject>().Type == ObjectType.ENEMY)
            {
                col.gameObject.GetComponent<BeDestroyedObject>().BeginDamage();
                energy.AddEnergy(25.0f);
            }
            Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss" && energy.LevelMax() || transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue)
        {
            //col.gameObject.GetComponent<Boss>().Dead();
            SceneNavigater.Instance.Change("Result");
        }
    }
}
