using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyParameter :ScriptableObject {


    /// <summary>
    /// 吸収されるまでの時間
    /// </summary>
    [Range(1f,10f)]
    [Tooltip("吸収されるスピード")]
    public float beAbsorptionSpeed;

    public Sprite defaultSprite;
    public Sprite actionSprite;
}
