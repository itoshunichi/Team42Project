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
    /// 吸収されるスピード
    /// </summary>
    protected float beAbsorptionSpeed = 0.01f;

    /// <summary>
    /// ボス
    /// </summary>
    protected GameObject boss;

    protected bool isActionMode;

    protected ObjectType type;

    protected GameObject player;

    public ObjectType Type
    {
        get { return type; }
    }
    

    public float GiveEnergyPoint
    {
        get { return parameter.giveEnergyPoint; }
    }


    protected virtual void Start()
    {
        isActionMode = false;
        boss = GameObject.Find("Boss");
        beAbsorptionSpeed = parameter.beAbsorptionSpeed * Time.deltaTime;
        type = parameter.type;
        player = GameObject.Find("PlayerSmall");
    }

    protected virtual void Update()
    {
        if (type != ObjectType.ENEMY) return;
        if(isActionMode)Action();
        //ボスに吸収される
        BeAbsorption();
        //スプライトの設定
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
        //エリアに入ったら
        if (collision.tag == "ActionEria")
        {
            isActionMode = true;
        }

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //ボスと衝突したら
        if (collision.collider.tag == "Boss")
        {
            GiveEnergy();
        }

       
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        //エリアから出たら
        if(collision.tag == "ActionEria")
        {
            StopAction();
        }
    }

    /// <summary>
    /// ボスにエネルギーを与える
    /// </summary>
    private void GiveEnergy()
    {
        if (type == ObjectType.SATELLITE) return;
        //ボスにエネルギーを追加
        boss.GetComponent<Boss>().AddEnergy(parameter.giveEnergyPoint);
        //オブジェクト削除
        GameObject.Find("FormBeDestroyedObject").GetComponent<FormBeDestroyedObject>().DestoryObject(gameObject);
    }


    /// <summary>
    /// ウェーブにあった後の行動
    /// </summary>
    protected virtual void Action() { }

    /// <summary>
    /// 停止ウェーブに当たったときの処理
    /// </summary>
    protected virtual void StopAction()
    {
        isActionMode = false;
    }

    /// <summary>
    /// ダメージを与えられるときに呼び出し
    /// </summary>
    /// <param name="damagePoint"></param>
    public virtual void BeginDamage()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// スプライトの設定
    /// </summary>
    protected void SetSprite()
    {
        if (isActionMode)
            GetComponent<SpriteRenderer>().sprite = parameter.actionSprite;

        else
            GetComponent<SpriteRenderer>().sprite = parameter.defaultSprite;
    }
}
