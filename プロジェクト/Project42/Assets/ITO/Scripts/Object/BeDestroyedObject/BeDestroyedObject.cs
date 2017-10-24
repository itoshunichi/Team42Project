using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 破壊されるオブジェクト
/// </summary>
public abstract class BeDestroyedObject : MonoBehaviour
{

    /// <summary>
    /// パラメーター
    /// </summary>
    [SerializeField]
    BeDestroyedObjectParameter parameter;

    /// <summary>
    /// 体力
    /// </summary>
    private int hp;

    /// <summary>
    /// 吸収されるスピード
    /// </summary>
    private float beAbsorptionSpeed = 0.01f;

    /// <summary>
    /// ボス
    /// </summary>
    protected GameObject boss;



    protected bool isWaveMode;

    public float GiveEnergyPoint
    {
        get { return parameter.giveEnergyPoint; }
    }


    protected virtual void Start()
    {
        boss = GameObject.Find("Boss");
        beAbsorptionSpeed = parameter.beAbsorptionSpeed;
    }

    protected virtual void Update()
    {

        BeAbsorption();
    }

    /// <summary>
    /// ボスに吸収される処理
    /// </summary>
    protected void BeAbsorption()
    {
        float rad = Mathf.Atan2(boss.transform.position.y - transform.position.y,
            boss.transform.position.x - transform.position.x);
        Vector2 pos = transform.position;
        pos.x += beAbsorptionSpeed * Mathf.Cos(rad);
        pos.y += beAbsorptionSpeed * Mathf.Sin(rad);
        transform.position = pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスと衝突したら
        if(collision.tag == "Boss")
        {
            GiveEnergy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //ウェーブが判定外にいったら
        if(collision.tag == "AttackWave")
        {
            isWaveMode = true;
        }

        if(collision.tag == "StopWave")
        {
            isWaveMode = false;
        }
    }
    /// <summary>
    /// ボスにエネルギーを与える
    /// </summary>
    private void GiveEnergy()
    {
        //ボスにエネルギーを追加
        boss.GetComponent<Boss>().AddEnergy(parameter.giveEnergyPoint);
        //オブジェクト削除
        GameObject.Find("FormBeDestroyedObject").GetComponent<FormBeDestroyedObject>().DestoryObject(gameObject);
    }

    
    /// <summary>
    /// ウェーブにあった後の行動
    /// </summary>
    protected virtual void WaveAction()
    {
        if (!isWaveMode) return;
    }

    /// <summary>
    /// ダメージを与えられるときに呼び出し
    /// </summary>
    /// <param name="damagePoint"></param>
    public void BeginDamage(int damagePoint)
    {
        hp -= damagePoint;
        BreakObject();
        Destroy(gameObject);
    }

    private void BreakObject()
    {
        if(hp<=0)
        {

        }
    }
}
