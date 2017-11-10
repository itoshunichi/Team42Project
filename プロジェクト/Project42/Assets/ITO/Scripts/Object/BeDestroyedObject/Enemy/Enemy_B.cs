using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを追尾するエネミー
/// </summary>
public class Enemy_B : BeDestroyedObject
{

    GameObject player;
    protected override void WaveAction()
    {
        base.WaveAction();
        SetPlayer();
    }

    protected override void Update()
    {
        Move();
        WaveAction();
        SetSprite();
    }

    private void Move()
    {
        if (isWaveMode)
        {
            //プレイヤー追尾
            TrackingPlayer();

        }
        else
        {
            //吸収
            BeAbsorption();
        }
    }

    /// <summary>
    /// プレイヤー取得
    /// </summary>
    private void SetPlayer()
    {
        player = GameObject.Find("PlayerSmall");
    }

    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    private void TrackingPlayer()
    {
        //プレイヤーの方を向く
        LookPlayer();
        //移動速度
        float speed = 0.05f;
        //ラジアン
        float rad = Mathf.Atan2(player.transform.position.y - transform.position.y,
            player.transform.position.x - transform.position.x);

        Vector2 pos = transform.position;
        pos.x += speed * Mathf.Cos(rad);
        pos.y += speed * Mathf.Sin(rad);
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

    protected override void StopWave()
    {
        base.StopWave();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //playerMode消したので取りあえずコメントアウトしました
        //if (collision.collider.tag == "Player"
        //    && collision.gameObject.GetComponent<Player>().playerMode == PlayerMode.PLAYER)
        //{
        //    //StartCoroutine(AttackPlayer(collision.gameObject));
        //}
    }

    /// <summary>
    /// プレイヤーに攻撃
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackPlayer(GameObject obj)
    {

        obj.GetComponent<Rigidbody2D>().AddForce(Direction() * 1000);

        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(gameObject);


    }

    /// <summary>
    /// 自身の向きを取得
    /// </summary>
    /// <returns></returns>
    private Vector3 Direction()
    {
        //自身の向きベクトル取得
        float angleDir = transform.eulerAngles.z * (Mathf.PI / 180f);
        Vector3 dir = new Vector3(-Mathf.Sin(angleDir), Mathf.Cos(angleDir), 0.0f);
        return dir;
    }
}
