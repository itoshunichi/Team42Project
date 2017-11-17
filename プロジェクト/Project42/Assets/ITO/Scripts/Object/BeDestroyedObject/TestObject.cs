using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : BeDestroyedObject {

    /// <summary>
    /// 球(仮)
    /// </summary>
    //[SerializeField]
    //private GameObject bulletPrefab;

    //protected override void Action()
    //{
    //    GameObject bullet = (GameObject)Instantiate(bulletPrefab);
    //    bullet.transform.position = transform.position;
    //}

    protected override void Start()
    {
        type = ObjectType.NORMAL;
    }

    public override void BeginDamage()
    {
        base.BeginDamage();
    }




}
