using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeDestroyedObjectParameter :ScriptableObject {

    /// <summary>
    /// 体力の最大値
    /// </summary>
    [Tooltip("体力の最大値")]
    public int maxHp;

    /// <summary>
    /// 与えるエネルギー
    /// </summary>
    [Tooltip("プレイヤーに与えるエネルギー")]
    public float giveEnergyPoint;

    /// <summary>
    /// 破壊するのにエネルギーが必要かどうか
    /// </summary>
    [Tooltip("破壊されるのにエネルギーが必要かどうか")]
    public bool isNecessaryEnergy;

    /// <summary>
    /// 吸収されるまでの時間
    /// </summary>
    [Tooltip("吸収されるまでの時間(秒数)")]
    public float beAbsorptionTime;
}
