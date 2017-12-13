//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Satellite : BeDestroyedObject {

//    /// <summary>
//    /// 壊せるかどうか
//    /// </summary>
//    [SerializeField]
//    private bool canDestroy;


//    protected override void Start()
//    {
//        type = ObjectType.SATELLITE;
//    }

//    protected override void Update()
//    {

//    }

//    public override void BeginDamage()
//    {
//        if (!canDestroy) return;
//        base.BeginDamage();
//        transform.parent.GetComponent<ActionEria>().BreakEriaPoint(gameObject,true);
//    }

//}
