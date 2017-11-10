using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EriaPoint : BeDestroyedObject {

    /// <summary>
    /// 壊せるかどうか
    /// </summary>
    [SerializeField]
    private bool canDestroy;


    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public override void BeginDamage()
    {
        if (!canDestroy) return;
        base.BeginDamage();
        transform.parent.GetComponent<ActionEria>().BreakEriaPoint(gameObject);
    }

}
