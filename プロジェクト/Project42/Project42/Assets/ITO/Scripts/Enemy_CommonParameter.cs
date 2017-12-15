using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの共通のパラメーター
/// </summary>
public class Enemy_CommonParametert:ScriptableObject  {

   // [Tooltip("生成時のエフェクト")]
    public GameObject spawnEffect;

    //[Tooltip("シールド状態の生成時のエフェクト")]
    public GameObject spawnEffect_Shield;

   // [Tooltip("破壊時のエフェクト")]
    public GameObject breakEffect;

    //[Tooltip("シールド状態の破壊時のエフェクト")]
    public GameObject breakEffect_Shield;

   // [Tooltip("エネルギー")]
    public GameObject energy;

    //[Tooltip("生成エフェクトが消えるまでの時間")]
    public float spaenEffectTime;

   // [Tooltip("移動速歩")]
    public float speed;


	
}
