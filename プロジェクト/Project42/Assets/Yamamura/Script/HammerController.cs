using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    //スクリプト
    public Shake shake;                         //mainCamera揺らす
    public Energy energy;                       //エネルギー
    public Hammer hammer;                       //ハンマーの主な処理
    public LerpPoint lerpPoint;               //ハンマーが指定位置に移動する
    public HammerSpriteChange hammerSprite;     //レベルによってスプライトを変える
    public EnergyEffect energyEffect;           //エネルギーのレベルによってエフェクトを変える
    LerpRotation lerpRotation;                  //指定ポイントをまとめるオブジェを回転させる
    //GameObject
    public GameObject player;                   //プレイヤー
    public GameObject flick;                    //フリック
    //float
    public float DestroyValue;                  //エネミーが死ぬ値(velocity.x or y)
    // Use this for initialization
    void Start()
    {
        lerpRotation = player.GetComponentInChildren<LerpRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        hammer.HammerUpdate();
        lerpPoint.Move();
        hammerSprite.SpriteChange();
        energyEffect.EnergyLevelEffect();
    }

    public void Reset()
    {
        hammer.Reset();
        lerpPoint.Reset();
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "BeDestroyedObject" &&
            (transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
        {
            Time.timeScale = 0.7f;
            shake.ShakeObject();    //Camera揺らす
            col.gameObject.GetComponent<BeDestroyedObject>().BeginDamage();
            if (col.gameObject.GetComponent<BeDestroyedObject>().Type != ObjectType.SATELLITE)
            {
                energy.AddEnergy();
                AudioManager.Instance.PlaySE(AUDIO.SE_ENERGYGET);
            }
            Time.timeScale = 1f;
        }
        //ボスに触れたとき
        if (col.gameObject.tag == "Boss")
        {   //エネルギーがMaxでvelocityが規定値以上だったらゲームクリア
            if (energy.LevelMax() && (transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
            {   
                col.gameObject.GetComponent<Boss>().Dead();
                AudioManager.Instance.PlaySE(AUDIO.SE_ATTACK);
            }
            //エネルギーがMaxじゃないならハンマーがはじかれる
            else if (!energy.LevelMax())
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (hammer.RotationPower() < 0)
                {
                    hammer.SetRotationForce(true, 2);
                    lerpRotation.SetRotation(90);
                }
                else if (hammer.RotationPower() > 0)
                {
                    hammer.SetRotationForce(false, 2);
                    lerpRotation.SetRotation(-90);
                }
            }
        }
    }
}
