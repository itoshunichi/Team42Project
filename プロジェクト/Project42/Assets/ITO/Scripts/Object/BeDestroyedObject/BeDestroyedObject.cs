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
    protected BeDestroyedObjectParameter parameter;

    /// <summary>
    /// 体力
    /// </summary>
    private int hp;

    /// <summary>
    /// 吸収されるスピード
    /// </summary>
    protected float beAbsorptionSpeed = 0.01f;

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
        Debug.Log("ウェーブ"+isWaveMode);
        isWaveMode = false;
        boss = GameObject.Find("Boss");
        beAbsorptionSpeed = parameter.beAbsorptionSpeed*Time.deltaTime;
    }

    protected virtual void Update()
    {
        if(isWaveMode)WaveAction();
        BeAbsorption();
        SetSprite();
    }

    /// <summary>
    /// ボスに吸収される処理
    /// </summary>
    protected void BeAbsorption()
    {
        LookBoss();
        float rad = Mathf.Atan2(boss.transform.position.y - transform.position.y,
            boss.transform.position.x - transform.position.x);
        Vector2 pos = transform.position;
        pos.x += beAbsorptionSpeed * Mathf.Cos(rad);
        pos.y += beAbsorptionSpeed * Mathf.Sin(rad);
        transform.position = pos;
    }

    /// <summary>
    /// ボスの方を向く
    /// </summary>
    private void LookBoss()
    {
        Vector2 vec = (boss.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, vec);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスと衝突したら
        if (collision.tag == "Boss")
        {
            GiveEnergy();
        }

        if (collision.tag == "AttackWave")
        {
            isWaveMode = true;
        }

        if (collision.tag == "StopWave")
        {
            StopWave();
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
    protected virtual void WaveAction() { }

    /// <summary>
    /// 停止ウェーブに当たったときの処理
    /// </summary>
    protected virtual void StopWave()
    {
        isWaveMode = false;
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
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// スプライトの設定
    /// </summary>
    protected void SetSprite()
    {
        if (isWaveMode)
            GetComponent<SpriteRenderer>().sprite = parameter.actionSprite;

        else
            GetComponent<SpriteRenderer>().sprite = parameter.defaultSprite;
    }
}
