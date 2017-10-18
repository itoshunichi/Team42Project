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
    private float beAbsorptionSpeed = 1f;

    /// <summary>
    /// ボス
    /// </summary>
    protected GameObject boss;

    /// <summary>
    /// 移動開始位置
    /// </summary>
    protected Vector3 startPosition;

    /// <summary>
    /// 移動開始時間
    /// </summary>
    protected float startTime;

    public float GiveEnergyPoint
    {
        get { return parameter.giveEnergyPoint; }
    }


    protected virtual void Start()
    {
        boss = GameObject.Find("Boss");
        startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;
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

        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > parameter.beAbsorptionTime)
        {
            transform.position = boss.transform.position;
        }

        var rate = diff / parameter.beAbsorptionTime;
        GetComponent<Rigidbody2D>().position = Vector3.Lerp(startPosition, boss.transform.position, rate * beAbsorptionSpeed);
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
        if(collision.tag == "Wave")
        {
            WaveAction();
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

    
    protected virtual void WaveAction()
    {
        Debug.Log("waveAction");
    }

    /// <summary>
    /// ダメージを与えられるときに呼び出し
    /// </summary>
    /// <param name="damagePoint"></param>
    public void BeginDamage(int damagePoint)
    {
        hp -= damagePoint;
        BreakObject();
    }

    private void BreakObject()
    {
        if(hp<=0)
        {

        }
    }
}
