using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BeDestroyedObject
{
    [SerializeField]
    private GameObject breakEffect;
    public override void BeginDamage()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        transform.parent.GetComponent<Boss>().TargetEnemy_ChangeSheild(collision);
    }

    public void BreakShield()
    {
        if (!FindObjectOfType<Boss>().IsShield) return;
        if (FindObjectOfType<FormBossStageObject>().ShieldEnemys.Count == 0)
        {
            //GetComponent<ParticleSystem>().Stop();
            Instantiate(breakEffect, transform.position, breakEffect.transform.rotation).GetComponent<ParticleSystem>().Play();
        }
    }

}
