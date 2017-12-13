using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BeDestroyedObject
{
    public override void BeginDamage()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        transform.parent.GetComponent<Boss>().TargetEnemy_ChangeSheild(collision);
    }


}
