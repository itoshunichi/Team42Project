using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour
{
    public GameObject player;
    private float rotationPower;
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
    DistanceJoint2D distance;

    // Use this for initialization
    void Start()
    {
        energy = GetComponent<Energy>();
        distance = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LerpRotaion();
        //transform.rotation = player.transform.rotation;

        VelocityZero();
        SpriteChange();
    }

    private void LerpRotaion()
    {
        if (addForceCount < ForceCountMax)
        {
            addForceCount++;
            var force = transform.right * (rotationPower);
            addForceAlpha += 0.02f;

            GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(transform.right * (rotationPower), Vector2.zero, addForceAlpha));
        }
    }
    //velocityをゼロにする処理
    private void VelocityZero()
    {
        if (countVelocity > velocityCountMax)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (countVelocity <= velocityCountMax)
        {
            countVelocity++;
        }
    }

    public void Reset()
    {
        countVelocity = 0;
        addForceCount = 0;
        addForceAlpha = 0;
    }

    public void SetRotationForce(bool isRight, int powerNum)
    {
        transform.rotation = player.transform.rotation;
        Reset();
        if (isRight)
        {
            if (powerNum == 0) rotationPower = powerOne;
            else if (powerNum == 1) rotationPower = powerTwo;
            else if (powerNum == 2) rotationPower = powerThree;
        }
        else if (!isRight)
        {
            if (powerNum == 0) rotationPower = -powerOne;
            else if (powerNum == 1) rotationPower = -powerTwo;
            else if (powerNum == 2) rotationPower = -powerThree;
        }
        //if (powerNum == 0) FlickRotation(isRight, powerOne);
        //else if (powerNum == 1) FlickRotation(isRight, powerTwo);
        //else if (powerNum == 2) FlickRotation(isRight, powerThree);
    }

    private void FlickRotation(bool isRight, float power)
    {
        if (isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (power));
        else if (!isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (-power));
    }

    //レベルによって見た目を変える
    private void SpriteChange()
    {
        if (energy.GetEnergy() < 100) GetComponent<SpriteRenderer>().sprite = hammerLv[0];
        else if (energy.GetEnergy() >= 100 && energy.GetEnergy() < 200) GetComponent<SpriteRenderer>().sprite = hammerLv[1];
        else if (energy.GetEnergy() >= 200) GetComponent<SpriteRenderer>().sprite = hammerLv[2];
    }

    public bool VelocityCountUp()
    {
        if (countVelocity < velocityCountMax) return true;
        return false;
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
