using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : ScriptableObject {
    
    /// <summary>
    /// エネミーの種類
    /// </summary>
    [Tooltip("エネミーの種類")]
    public EnemyType type;

    /// <summary>
    /// 体力の最大値
    /// </summary>
    [Tooltip("最大HP"),Range(0,100)]
    public int maxHp;

    /// <summary>
    /// 気絶するHP
    /// </summary>
    [Tooltip("気絶するHP"),Range(0,100)]
    public int stunning_HP;

    /// <summary>
    /// 回復時間
    /// </summary>
    [Tooltip("回復時間"),Range(0,100)]
    public float recoveryTime;


}
