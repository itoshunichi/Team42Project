using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float power;             //振る力
    public Energy soulEnergy;
    int addForceCount = 120;
    float addForceAlpha = 0;
    float countVelocity = 120;
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (addForceCount < 10)
        {
            addForceCount++;
            var force = transform.right * (power);
            addForceAlpha += 0.02f;

            GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(force, Vector2.zero, addForceAlpha));
        }

        VelocityZero(10);
    }

    private void VelocityZero(int countMax)
    {
        countVelocity++;
        if (countVelocity > countMax)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }

    public void Reset()
    {
        countVelocity = 0;
        addForceCount = 0;
        addForceAlpha = 0;
    }

    public void RotationForce(bool isRight, float scalar)
    {
        addForceCount = 0;
        addForceAlpha = 0;
        if (!isRight && power < 0)
            power = -power;
        else if (isRight && power > 0)
            this.power = -power;
        power *= scalar;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && transform.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            col.gameObject.GetComponent<BeDestroyedObject>().BeginDamage(1);
            soulEnergy.AddEnergy(10);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss" && transform.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            col.gameObject.GetComponent<Boss>().BeginDamage();
        }
    }
}
