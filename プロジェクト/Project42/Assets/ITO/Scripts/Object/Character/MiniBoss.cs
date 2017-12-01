using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Boss {


    protected override void Start()
    {
        
    }

    protected override void Update()
    {
       
    }
    public override void Dead()
    {
        Debug.Log("BossDead");
        SceneNavigater.Instance.Change("GamePlay");
    }
}
