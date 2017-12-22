using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{

    public Shake shake;
    public Hammer hammer;
    public float DestroyValue;
    public Energy energy;
    bool isHit = false;
    float time;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hammer.HammerUpdate();
        if (isHit)
        {
            time += 0.01f;
            if (time > 0.05f)
            {
                Time.timeScale = 1f;
                isHit = false;
                time = 0;
            }
        }
    }

    public void Reset()
    {
        hammer.Reset();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if ((col.gameObject.tag == "BeDestroyedObject"||col.gameObject.tag == "boss") && hammer.GetLerpTime() < 1.0f)
            //hammerMove.GetAlpha() < 1.0f)
        // (transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
        {
            Debug.Log("ヒット");
            Time.timeScale = 0.0f;
            shake.ShakeCamera(2.5f, 2.5f, 0.25f);
            Debug.Log(col.gameObject.name);
            col.gameObject.GetComponent<BeDestroyedObject>().BeginDamage(true);

            energy.CombPuls();
            AudioManager.Instance.PlaySE(AUDIO.SE_ENERGYGET);
            isHit = true;
        }

        if (col.gameObject.tag == "Boss" &&
            hammer.GetLerpTime() < 1.0f)
        //    hammerMove.GetAlpha() < 1.0f)
        //(transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
        {
            //col.gameObject.GetComponent<Boss>().Dead();
            AudioManager.Instance.PlaySE(AUDIO.SE_ATTACK);
        }
    }
}
