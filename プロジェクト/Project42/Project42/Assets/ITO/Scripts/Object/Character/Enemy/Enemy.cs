using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMode
{
    NORMAL,//通常
    TARKING,//プレイヤー追尾
    SHIELD,//シールドエネミー状態
}


public abstract class Enemy : BeDestroyedObject
{
    //プレイヤー
    private GameObject player;
    //移動速度
    protected float speed;
    //エネルギー
    protected GameObject energy;
    //開始時の角度
    protected Quaternion startRotation;
    //エネミーの状態
    EnemyMode mode;

    protected GameObject breakEffect;
    //登場時のエフェクト
    //private GameObject spawnEffect;

    /// <summary>
    /// 共通のパラメーター
    /// </summary>
    [SerializeField]
    private Enemy_CommonParametert commonParameter;



    private bool isMove;
    public bool IsMove
    {
        get { return isMove; }
        set {this.isMove = value; }
    }

    /// <summary>
    /// 画面上にいるかどうか
    /// </summary>
    protected bool isVisible;
    public bool IsVisible
    {
        get { return isVisible; }
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


    protected override void Start()
    {
        base.Start();
        commonParameter = Resources.Load<Enemy_CommonParametert>("Data/Enemy_CommonParametert");
        speed = commonParameter.speed;
        energy = commonParameter.energy;
        breakEffect = commonParameter.breakEffect;
        isMove = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        startRotation = transform.rotation;
        player = GameObject.Find("PlayerFront");
        StartCoroutine(StartMove());
        AppearedInDirector();

    }

    protected virtual void Update()
    {
        SelectMove();
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
        GameObject spawnEffectPrefab = commonParameter.spawnEffect;
        GameObject spawnEffect = Instantiate(spawnEffectPrefab, transform.position, spawnEffectPrefab.transform.rotation);
        spawnEffect.GetComponent<ParticleSystem>().Play();
        //if(spawnEffect.GetComponent<ParticleSystem>().)

        yield return new WaitForSeconds(commonParameter.spaenEffectTime);
        GetComponent<Collider2D>().enabled = true;
        //エフェクト削除
        Destroy(spawnEffect);

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
    /// <summary>
    /// 仮
    /// </summary>
    public void SetShieldModeColor()
    {
       
            GetComponent<SpriteRenderer>().color = Color.magenta;
           // ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
           // color.mode = ParticleSystemGradientMode.Color;
           // color.color = Color.magenta;
           // ParticleSystem.MainModule main = breakEffect.GetComponent<ParticleSystem>().main;
           // main.startColor = Color.magenta;
           //// SpawnEffect
        
    }

    /// <summary>
    /// 移動手段の選択
    /// </summary>
    private void SelectMove()
    {
        if (!isMove) return;
        //追尾中だったら
        if (IsSelectMode(EnemyMode.TARKING))
        {
            TrackingPlayer();
        }
        else
        {
            transform.rotation = startRotation;
            Move();
        }
    }

    protected abstract void Move();



    #region OnTrigger
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerWave(collision);
    }


    /// <summary>
    /// ウェーブと当たったときの処理
    /// </summary>
    private void OnTriggerWave(Collider2D collision)
    {
        if (collision.tag == "AttackWave" && IsSelectMode(EnemyMode.NORMAL))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigid.velocity = Vector2.zero;
            //追尾状態に
            ChangeMode(EnemyMode.TARKING);
        }
    }

    #endregion

    public override void BeginDamage()
    {
        //通常ウェーブ
        if (FindObjectOfType<FormEnemyObject>())
        {
            FindObjectOfType<FormEnemyObject>().DestoryObject(gameObject);

        }
        //ボスウェーブ
        if (FindObjectOfType<FormBossStageObject>())
        {

            FindObjectOfType<FormBossStageObject>().FormRandomEnemy();

            FindObjectOfType<FormBossStageObject>().DestroyEnemy(gameObject);
            FindObjectOfType<Shield>().BreakShield();

        }
        Instantiate(breakEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        Instantiate(energy, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    private void TrackingPlayer()
    {
        if (IsSelectMode(EnemyMode.TARKING))
        {

            //プレイヤーの方を向く
            LookPlayer();
            //ラジアン
            float rad = Mathf.Atan2(player.transform.position.y - transform.position.y,
                player.transform.position.x - transform.position.x);

            Vector2 pos = transform.position;
            pos.x += speed * Mathf.Cos(rad) * Time.deltaTime;
            pos.y += speed * Mathf.Sin(rad) * Time.deltaTime;
            transform.position = pos;
        }
    }

    /// <summary>
    /// プレイヤーの方を向く
    /// </summary>
    private void LookPlayer()
    {
        Vector2 vec = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, vec);
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

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    protected virtual void OnBecameInvisible()
    {
        isVisible = false;
    }





}
