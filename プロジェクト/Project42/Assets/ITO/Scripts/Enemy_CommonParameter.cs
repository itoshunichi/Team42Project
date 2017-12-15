using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの共通のパラメーター
/// </summary>
public class Enemy_CommonParametert:ScriptableObject  {

    [Tooltip("生成時のエフェクト")]
    public GameObject spawnEffect;

    [Tooltip("破壊時のエフェクト")]
    public GameObject breakEffect;

    [Tooltip("エネルギー")]
    public GameObject energy;

    [Tooltip("生成エフェクトが消えるまでの時間")]
    public float spaenEffectTime;

    [Tooltip("移動速歩")]
    public float speed;


	
}
