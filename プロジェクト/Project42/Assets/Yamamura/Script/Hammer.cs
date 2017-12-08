using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour
{
    public GameObject player;
    private float rotationPower;    //回転力
    public float powerOne;          //一段階目の力
    public float powerTwo;          //二段階目の力
    public float powerThree;        //三段階目の力

    int addForceCount = 120;        //
    public int ForceCountMax;
    float addForceAlpha = 0;        //
    float countVelocity = 120;      //velocityをゼロにするカウント
    public float velocityCountMax;
    public GameObject effect;       //ハンマー振る際の
    float time;
    private bool isCamera;

    public bool IsCamera
    {
        get { return isCamera; }
    }
    // Use this for initialization
    void Start()
    {

    }

    //
    public void HammerUpdate()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, player.transform.position);
        time += Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, time / 100);
        // LerpRotaion();
        //VelocityZero();
    }

    public float GetLerpTime()
    {
        return time / 100;
    }

    private void LerpRotaion()
    {
        if (addForceCount < ForceCountMax)
        {
            addForceCount++;
            addForceAlpha += 0.005f;
            var vector = transform.right * (rotationPower);
            GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(vector, Vector2.zero, addForceAlpha));
            //GetComponent<Rigidbody2D>().AddForce(transform.right * (rotationPower));
        }
    }
    //velocityをゼロにする処理
    private void VelocityZero()
    {
        if (!VelocityCount())
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        countVelocity++;
    }
    //指定数値を０に
    public void Reset()
    {
        time = 0;
        //countVelocity = 0;
        //addForceCount = 0;
        //addForceAlpha = 0;
        //GetComponent<HammerMove>().Reset();
    }
    //回転する力を設定
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

        AudioManager.Instance.PlaySE(AUDIO.SE_SWING);
    }

    public bool VelocityCount()
    {
        if (countVelocity < velocityCountMax) return true;
        return false;
    }

    void OnWillRenderObject()
    {   //SceneCameraとプレビューカメラに反応しないように
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
        {
            isCamera = true;
            if (addForceCount < ForceCountMax)
                Instantiate(effect, transform.position, effect.transform.rotation);
        }
        else
        {
            isCamera = false;
        }
    }
}
