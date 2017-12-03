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

    private bool isTarkingPlayer = false;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("PlayerSmall");
    }

    protected virtual void Update()
    {
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

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        if (collision.tag == "AttackWave")
        {
            isTarkingPlayer = !isTarkingPlayer;
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
            Instantiate(breakEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
            Instantiate(energy, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    private void TrackingPlayer()
    {
        if (!isTarkingPlayer) return;
        //プレイヤーの方を向く
        LookPlayer();
        //移動速度
        //float speed = 0.05f;
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

    ///// <summary>
    ///// プレイヤーに攻撃
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator AttackPlayer(GameObject obj)
    //{

    //    obj.GetComponent<Rigidbody2D>().AddForce(Direction() * 300);
    //    player.transform.rotation = Quaternion.FromToRotation(Vector3.up, Direction());

    //    yield return new WaitForSeconds(0.5f);
    //    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //    Destroy(gameObject);


    //}


    ///// <summary>
    ///// 自身の向きを取得
    ///// </summary>
    ///// <returns></returns>
    //private Vector3 Direction()
    //{
    //    //自身の向きベクトル取得
    //    float angleDir = transform.eulerAngles.z * (Mathf.PI / 180f);
    //    Vector3 dir = new Vector3(-Mathf.Sin(angleDir), Mathf.Cos(angleDir), 0.0f);
    //    return dir;
    //}


}
