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
    
    int addForceCount = 120;        //
    public int ForceCountMax;
    float addForceAlpha = 0;        //
    float countVelocity = 120;      //velocityをゼロにするカウント
    public float velocityCountMax;

    
    // Use this for initialization
    void Start()
    {
   
    }


    public void HammerUpdate()
    {
        LerpRotaion();
        VelocityZero();
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
        GetComponent<HammerMove>().Reset();
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
    }

    private void FlickRotation(bool isRight, float power)
    {
        if (isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (power));
        else if (!isRight)
            GetComponent<Rigidbody2D>().AddForce(transform.right * (-power));
    }

   
    public bool VelocityCountUp()
    {
        if (countVelocity < velocityCountMax) return true;
        return false;
    }


}
