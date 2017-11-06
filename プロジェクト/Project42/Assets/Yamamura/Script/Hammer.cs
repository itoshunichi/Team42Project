using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private float power;             //振る力
    public float powerOne;          //一段階目の力
    public float powerTwo;          //二段階目の力
    public float powerThree;        //三段階目の力
    Energy hammerEnergy;       //Energy
    public Energy soulEnergy;       //Energy
    int addForceCount = 120;        //
    public int ForceCountMax;
    float addForceAlpha = 0;        //
    float countVelocity = 120;      //velocityをゼロにするカウント
    public float velocityCountMax;
    public Shake shake;
    // Use this for initialization
    void Start()
    {
        hammerEnergy = GetComponent<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (addForceCount < ForceCountMax)
        //{
        //    addForceCount++;
        //    var force = transform.right * (power);
        //    addForceAlpha += 0.02f;

        //    GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(transform.right * (power), Vector2.zero, addForceAlpha));
        //}

        VelocityZero();
    }

    private void VelocityZero()
    {
        countVelocity++;
        if (countVelocity > velocityCountMax)
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

    public void SetRotationForceOne(bool isRight)
    {
        Reset();
        power = powerOne;
        SetPower(isRight);
    }
    public void SetRotationForceTwo(bool isRight)
    {
        Reset();
        power = powerTwo;
        SetPower(isRight);
    }
    public void SetRotationForceThree(bool isRight)
    {
        Reset();
        power = powerThree;
        SetPower(isRight);
    }

    private void SetPower(bool isRight)
    {
        if (isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (power));
        else if (!isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (-power));
        //if (isRight && power > 0)
        //    power = -power;
        //else if (!isRight && power < 0)
        //    power = -power;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && transform.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            Time.timeScale = 0.7f;
            shake.ShakeObject();
           // col.gameObject.GetComponent<BeDestroyedObject>().BeginDamage(1);
            hammerEnergy.AddEnergy(10);
            Time.timeScale = 1.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss" && transform.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            //col.gameObject.GetComponent<Boss>().BeginDamage();
        }
    }
}
