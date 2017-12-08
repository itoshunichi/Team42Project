using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : BeDestroyedObject
{

    private GameObject player;
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected GameObject energy;
    protected Quaternion startRotation;




    private bool isTarkingPlayer = false;

    private bool isStop = false;
    public bool IsTarkingPlayer
    {
        set { this.isTarkingPlayer = value; }
    }


    protected override void Start()
    {
        base.Start();
        startRotation = transform.rotation;
        player = GameObject.Find("PlayerFront");
    }

    protected virtual void Update()
    {
        if (isStop) return;
        if (isTarkingPlayer)
        {
            TrackingPlayer();
        }
        else
        {
            Move();
        }
    }

    protected abstract void Move();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if (isTarkingPlayer && collision.collider.gameObject.name == "PlayerSmall")
        {
            AttackPlayer(player.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AttackWave")
        {
            Debug.Log("wave");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigid.velocity = Vector2.zero;
            isTarkingPlayer = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {

    }

    public override void BeginDamage()
    {
        if (FindObjectOfType<FormEnemyObject>())
        {
            FindObjectOfType<FormEnemyObject>().DestoryObject(gameObject);
           
        }
        if(FindObjectOfType<FormBossStageObject>())
        {
            FindObjectOfType<FormBossStageObject>().DestroyEnemy(gameObject);
        }

        Instantiate(breakEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        Instantiate(energy, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    private void TrackingPlayer()
    {
        if (!isTarkingPlayer) return;
       
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

    /// <summary>
    /// プレイヤーの方を向く
    /// </summary>
    private void LookPlayer()
    {
        Vector2 vec = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, vec);
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

    /// <summary>
    /// プレイヤーに攻撃
    /// </summary>
    /// <returns></returns>
    private void AttackPlayer(GameObject obj)
    {
        //player.GetComponent<PlayerSmallController>().IsMoveStop = true;
        //obj.GetComponent<Rigidbody2D>().AddForce(Direction() * 100);
        //player.transform.rotation = Quaternion.FromToRotation(Vector3.up, Direction());
        isStop = true;
        FindObjectOfType<FormBossStageObject>().AddNormalEnemy(gameObject);
        FindObjectOfType<FormBossStageObject>().DestroyEnemy(gameObject);
        
    }





}
