using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMode
{
    NORMAL,//通常
    //TARKING,//プレイヤー追尾
    SHIELD,//シールドエネミー状態
}


public abstract class Enemy : BeDestroyedObject
{
    //プレイヤー
    protected GameObject player;
    //移動速度
    protected float speed;
    //エネルギー
    protected GameObject energy;
    //開始時の角度
    protected Quaternion startRotation;

    [SerializeField]
    //エネミーの状態
    EnemyMode mode;

    protected GameObject breakEffect;
    //登場時のエフェクト
    private GameObject spawnEffect;

   // private GameObject breakEffect_shield;
   // private GameObject spawnEffect_shield;

    /// <summary>
    /// 共通のパラメーター
    /// </summary>
    [SerializeField]
    //private Enemy_CommonParametert commonParameter;



    private bool isMove;
    public bool IsMove
    {
        get { return isMove; }
        set { this.isMove = value; }
    }




    /// <summary>
    /// 指定した状態かどうか
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public bool IsSelectMode(EnemyMode mode)
    {
        return this.mode == mode;
    }
    /// <summary>
    /// 状態の変更
    /// </summary>
    /// <param name="mode"></param>
    public void ChangeMode(EnemyMode mode)
    {
        this.mode = mode;
    }


    private void Awake()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        speed = 5;
        energy = Resources.Load<GameObject>("Prefab/Energy");
        SetEffect();
        isMove = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        startRotation = transform.rotation;
        player = GameObject.Find("PlayerFront");
        StartCoroutine(StartMove());
        AppearedInDirector();

    }

    private void SetEffect()
    {
        if(mode == EnemyMode.NORMAL)
        {
            breakEffect = Resources.Load<GameObject>("Prefab/Effect/Enemy/Break_Enemy_New");
            //spawnEffect = Resources.Load<GameObject>("Prefab/Effect/Enemy/Enemy_Spawn");
        }
        if(mode == EnemyMode.SHIELD)
        {
            Debug.Log("Set");
            breakEffect = Resources.Load<GameObject>("Prefab/Effect/Enemy/Break_Enemy_Shield");
           // spawnEffect = Resources.Load<GameObject>("Prefab/Effect/Enemy/Enemy_Shield_Spawn");
        }
    }

    protected virtual void Update()
    {
        if (isMove)
        {
            Move();
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
        //SetShieldModeColor();
    }

    /// <summary>
    /// 登場演出
    /// </summary>
    /// <returns></returns>
    private void AppearedInDirector()
    {

        //エフェクト生成
        StartCoroutine(SpawnEffect());


        GetComponent<SpriteRenderer>().enabled = true;
        //フェードして出現
        StartCoroutine(FadeSpawn());
        GetComponent<Animator>().enabled = true;



    }

    /// <summary>
    /// 登場時のエフェクトの生成
    /// </summary>
    private IEnumerator SpawnEffect()
    {
        GetComponent<Collider2D>().enabled = false;
        // GameObject effect = Instantiate(spawnEffect, transform.position,spawnEffect.transform.rotation);
        //effect.GetComponent<ParticleSystem>().Play();
        GameObject effect = transform.GetChild(0).gameObject;
        yield return new WaitForSeconds(1);
        GetComponent<Collider2D>().enabled = true;
        //エフェクト削除
        Destroy(effect);

        yield return new WaitForSeconds(0.5f);
        isMove = true;

    }

    /// <summary>
    /// 登場時フェードして出現
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeSpawn()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        //不透明になるまでループ
        while (color.a <= 255)
        {
            color.a += 0.1f;
            GetComponent<SpriteRenderer>().color = color;
        }
        yield break;

    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1.5f);
        isMove = true;
    }
   

    protected abstract void Move();



    #region OnTrigger
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }




    #endregion

    public override void BeginDamage(bool isDamage)
    {
        //通常ウェーブ
        if (FindObjectOfType<FormEnemyObject>())
        {
            FindObjectOfType<FormEnemyObject>().DestoryObject(gameObject,isDamage);
            

        }
        //ボスウェーブ
        if (FindObjectOfType<FormBossStageObject>())
        {
            FindObjectOfType<FormBossStageObject>().FormRandomEnemy();
            FindObjectOfType<FormBossStageObject>().DestroyEnemy(gameObject,isDamage);
            

            FindObjectOfType<Shield>().BreakShield();

        }
        //エフェクトの生成
        InstatiateBreakEffect();
        if (isDamage)
        {
            Instantiate(energy, transform.position, Quaternion.identity);
        }
    }



    /// <summary>
    /// 破壊エフェクトの生成
    /// </summary>
    private void InstatiateBreakEffect()
    {
       
        Instantiate(breakEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
    }

    public void Stop()
    {
        isMove = false;
        rigid.velocity = Vector2.zero;

    }

    public void SetStartRotation()
    {
        transform.rotation = startRotation;
    }

    /// <summary>
    /// 自身の向きを取得
    /// </summary>
    /// <returns></returns>
    protected Vector3 Direction()
    {
        //自身の向きベクトル取得
        float angleDir = transform.eulerAngles.z * (Mathf.PI / 180f);
        Vector3 dir = new Vector3(-Mathf.Sin(angleDir), Mathf.Cos(angleDir), 0.0f);
        return dir;
    }





}
