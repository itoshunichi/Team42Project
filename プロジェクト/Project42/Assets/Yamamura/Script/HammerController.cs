using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{

    public Shake shake;
    public Energy energy;
    public Hammer hammer;
    public HammerMove hammerMove;
    public HammerSpriteChange hammerSprite;
    public EnergyEffect energyEffect;
    public GameObject player;
    public GameObject flick;
    public float DestroyValue;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hammer.HammerUpdate();
        //hammerMove.Move();
        hammerSprite.SpriteChange();
        energyEffect.EnergyLevelEffect();
    }

    public void Reset()
    {
        hammer.Reset();
        hammerMove.Reset();
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
                energy.AddEnergy(25.0f);
                AudioManager.Instance.PlaySE(AUDIO.SE_ENERGYGET);
            }
            Time.timeScale = 1f;
        }

        if (col.gameObject.tag == "Boss")
        {
            if (energy.LevelMax() && (transform.GetComponent<Rigidbody2D>().velocity.x > DestroyValue || transform.GetComponent<Rigidbody2D>().velocity.y > DestroyValue))
            {
                col.gameObject.GetComponent<Boss>().Dead();
                AudioManager.Instance.PlaySE(AUDIO.SE_ATTACK);
            }
            else if (!energy.LevelMax())
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (hammer.RotationPower() < 0)
                {
                    hammer.SetRotationForce(true, 1);
                }
                else if (hammer.RotationPower() > 0)
                {
                    hammer.SetRotationForce(false, 1);
                }
            }
        }
    }
}
