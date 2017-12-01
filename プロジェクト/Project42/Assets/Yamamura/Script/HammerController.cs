using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour {

    public Shake shake;
    public Hammer hammer;
    public HammerMove hammerMove;
    public float DestroyValue;
    public Energy energy;

	// Use this for initialization
	void Start () {
  	}
	
	// Update is called once per frame
	void Update () {
        hammer.HammerUpdate();
        hammerMove.Move();
	}

    public void Reset()
    {
        hammer.Reset();
        hammerMove.Reset();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && hammerMove.GetAlpha() < 1.0f)
           // (transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
        {
            Debug.Log("ヒット");
            Time.timeScale = 0.7f;
            shake.ShakeObject();
            Debug.Log(col.gameObject.name);
            col.gameObject.GetComponent<BeDestroyedObject>().BeginDamage();
            if (col.gameObject.GetComponent<BeDestroyedObject>().Type != ObjectType.SATELLITE)
            {
                energy.CombPuls();
                AudioManager.Instance.PlaySE(AUDIO.SE_ENERGYGET);
            }
            Time.timeScale = 1f;
        }

        if (col.gameObject.tag == "Boss" && 
            hammerMove.GetAlpha() < 1.0f)
            //(transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
        {
            //col.gameObject.GetComponent<Boss>().Dead();
            AudioManager.Instance.PlaySE(AUDIO.SE_ATTACK);
        }
    }
}
