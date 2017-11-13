using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour
{
    public GameObject player;
    private float power;             //振る力
    public float powerOne;          //一段階目の力
    public float powerTwo;          //二段階目の力
    public float powerThree;        //三段階目の力
    Energy energy;       //Energy
    int addForceCount = 120;        //
    public int ForceCountMax;
    float addForceAlpha = 0;        //
    float countVelocity = 120;      //velocityをゼロにするカウント
    public float velocityCountMax;
    public Shake shake;
    public Sprite[] hammerLv;

    // Use this for initialization
    void Start()
    {
        energy = GetComponent<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
        //if (addForceCount < ForceCountMax)
        //{
        //    addForceCount++;
        //    var force = transform.right * (power);
        //    addForceAlpha += 0.02f;

        //    GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(transform.right * (power), Vector2.zero, addForceAlpha));
        //}

        VelocityZero();
        SpriteChange();
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
        //if (isRight)
        power = powerOne;
        //else power = -powerOne;
        SetPower(isRight);
    }
    public void SetRotationForceTwo(bool isRight)
    {
        Reset();
        //if (isRight)
        power = powerTwo;
        //else power = -powerTwo;
        SetPower(isRight);
    }
    public void SetRotationForceThree(bool isRight)
    {
        Reset();
        //if(isRight)
        power = powerThree;
        //else power = -powerThree;
        SetPower(isRight);
    }

    private void SetPower(bool isRight)
    {
        if (isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (power));
        else if (!isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (-power));
    }


    private void SpriteChange()
    {
        if (energy.GetEnergy() < 100) GetComponent<SpriteRenderer>().sprite = hammerLv[0];
        else if (energy.GetEnergy() >= 100 && energy.GetEnergy() < 200) GetComponent<SpriteRenderer>().sprite = hammerLv[1];
        else if (energy.GetEnergy() >= 200) GetComponent<SpriteRenderer>().sprite = hammerLv[2];
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && transform.GetComponent<Rigidbody2D>().velocity.x > 1.8f || transform.GetComponent<Rigidbody2D>().velocity.y > 1.8f)
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
        if (col.gameObject.tag == "Boss" && transform.GetComponent<Rigidbody2D>().velocity != Vector2.zero && energy.LevelMax())
        {
            //col.gameObject.GetComponent<Boss>().Dead();
            SceneNavigater.Instance.Change("Result");
        }
    }
}
