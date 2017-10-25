using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを追尾するエネミー
/// </summary>
public class Enemy_B : BeDestroyedObject {

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
        
        if (GameObject.Find("PlayerSmall").GetComponent<PlayerSmallController>().playerMode == PlayerMode.PLAYER)
        {
            player = GameObject.Find("PlayerSmall");
        }
        else
        {
            player = GameObject.Find("PlayerBig");
        }
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.tag == "Player")
        {
            
        }
    }
}
