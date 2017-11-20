using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Boss {

    public override void Dead()
    {
        Debug.Log("BossDead");
        SceneNavigater.Instance.Change("GamePlay");
    }
}
