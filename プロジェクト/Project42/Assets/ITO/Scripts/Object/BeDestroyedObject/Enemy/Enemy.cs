using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BeDestroyedObject {


    protected Animator animator;

    /// <summary>
    /// 吸収されるスピード
    /// </summary>
    protected float beAbsorptionSpeed =2f;

    /// <summary>
    /// ボス
    /// </summary>
    protected GameObject boss;

    protected bool isActionMode;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        isActionMode = false;
        boss = GameObject.Find("Boss");
        type = ObjectType.ENEMY;
    }

    protected override void Update()
    {
        
        if (isActionMode) Action();
        //ボスに吸収される
        BeAbsorption();
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
        pos.x += beAbsorptionSpeed * Mathf.Cos(rad)*Time.deltaTime;
        pos.y += beAbsorptionSpeed * Mathf.Sin(rad) * Time.deltaTime;
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


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //ボスと衝突したら
        if (collision.collider.tag == "Boss")
        {
            GiveEnergy();
        }


    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //エリアに入ったら
        if (collision.tag == "ActionEria")
        {
            animator.enabled = true;
            isActionMode = true;
        }

    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        //エリアから出たら
        if (collision.tag == "ActionEria")
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
        boss.GetComponent<Boss>().AddEnergy();
        //オブジェクト削除
        GameObject.Find("FormBeDestroyedObject").GetComponent<FormBeDestroyedObject>().DestoryObject(gameObject);
        AudioManager.Instance.PlaySE(AUDIO.SE_MATHEREAT);
    }

    /// <summary>
    /// 衛星のエリアに入った後の行動
    /// </summary>
    protected virtual void Action() { }

    /// <summary>
    /// 衛星のエリアから出たときの処理
    /// </summary>
    protected virtual void StopAction()
    {
        isActionMode = false;
    }

   
}
