using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾を飛ばすエネミー
/// </summary>
public class Enemy_C : BeDestroyedObject
{

    private float attackTimer;

    /// <summary>
    /// 弾のリスト
    /// </summary>
    [SerializeField]
    private List<GameObject> bullets;

    /// <summary>
    /// 弾での攻撃
    /// </summary>
    private void BulletAttack()
    {
        attackTimer += Time.deltaTime;
        //1秒間隔で生成する
        if (attackTimer > 1f)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                Instantiate(bullets[i], transform.position, Quaternion.identity);

            }
            attackTimer = 0f;
        }

    }


    protected override void WaveAction()
    {
        Debug.Log("攻撃");
        BulletAttack();
    }




}
