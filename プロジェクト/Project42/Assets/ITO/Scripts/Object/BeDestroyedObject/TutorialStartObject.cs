using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartObject : BeDestroyedObject {

    protected override void Start()
    {
        type = ObjectType.NORMAL;
    }
    public override void BeginDamage()
    {
        GameObject.Find("Tutorial").GetComponent<Tutorial>().StartGame();
        base.BeginDamage();
        
    }
}
