using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スピードが早くなるエネミー
/// </summary>
public class Enemy_A : BeDestroyedObject {

    protected override void WaveAction()
    {
        base.WaveAction();
        //吸い込まれるスピードを変更
        beAbsorptionSpeed = parameter.beAbsorptionSpeed*2;
    }

    protected override void StopWave()
    {
        base.StopWave();
        //初期のスピードに戻す
        beAbsorptionSpeed = parameter.beAbsorptionSpeed;
    }
}
