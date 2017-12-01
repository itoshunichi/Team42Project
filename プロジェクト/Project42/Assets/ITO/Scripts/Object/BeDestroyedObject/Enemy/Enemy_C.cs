using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾を飛ばすエネミー
/// </summary>
public class Shot_Enemy : Enemy
{

    private float attackTimer;

    /// <summary>
    /// 弾
    /// </summary>
    [SerializeField]
    private GameObject bullet;

    /// <summary>
    /// 弾の生成
    /// </summary>
    private void InstantiateBullet()
    {
        attackTimer += Time.deltaTime;
        //1秒間隔で生成する
        if (attackTimer > 0.5f)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            attackTimer = 0f;
        }

    }


    protected override void Action()
    {
        InstantiateBullet();
    }




}
