﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeDestroyedObjectParameter :ScriptableObject {

    /// <summary>
    /// オブジェクトの種類
    /// </summary>
    [Tooltip("オブジェクトの種類")]
    public ObjectType type;

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
    [Range(1f,10f)]
    [Tooltip("吸収されるスピード")]
    public float beAbsorptionSpeed;

    public Sprite defaultSprite;
    public Sprite actionSprite;
}
