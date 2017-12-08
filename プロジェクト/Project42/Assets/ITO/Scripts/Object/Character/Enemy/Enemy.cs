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
    [SerializeField]
    protected float speed;
    //エネルギー
    [SerializeField]
    protected GameObject energy;
    //開始時の角度
    protected Quaternion startRotation;
    //エネミーの状態
    EnemyMode mode;

    private bool isMove;
    public bool IsMove
    {
        get { return isMove; }
        set { this.isMove = value; }
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
        startRotation = transform.rotation;
        player = GameObject.Find("PlayerFront").transform.GetChild(1).gameObject;
        StartCoroutine(StartMove());
        Debug.Log(mode);
    }

    protected virtual void Update()
    {
        SelectMove();
        SetShieldModeColor();
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1.5f);
        isMove = true;
    }
    /// <summary>
    /// 仮
    /// </summary>
    private void SetShieldModeColor()
    {
        if (IsSelectMode(EnemyMode.SHIELD))
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
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

    #region OnCollision
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        AttackPlayer(collision);
    }

    /// <summary>
    /// プレイヤーに攻撃
    /// </summary>
    /// <returns></returns>
    private void AttackPlayer(Collision2D collision)
    {
        //player.GetComponent<PlayerSmallController>().IsMove = true;
       
        //isStop = true;

        if (IsSelectMode(EnemyMode.TARKING) && collision.collider.gameObject.name == "PlayerSmall")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Direction() * 1000,ForceMode2D.Impulse);
           // player.transform.rotation = Quaternion.FromToRotation(Vector3.up, Direction());
            transform.rotation = startRotation;
            //通常の移動に
            ChangeMode(EnemyMode.NORMAL);
        }

    }

    #endregion



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
        if(FindObjectOfType<FormBossStageObject>())
        {
            FindObjectOfType<FormBossStageObject>().DestroyEnemy(gameObject);
           
        }

        if(IsSelectMode(EnemyMode.SHIELD))
        {
            FindObjectOfType<Boss>().InstantiateWave();
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
        transform.rotation= startRotation;
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
