using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 破壊されるオブジェクト
/// </summary>
public abstract class BeDestroyedObject : MonoBehaviour
{

    protected Animator animator;

    protected Rigidbody2D rigid;
    //オブジェクトの種類
    //protected ObjectType type;

    [SerializeField]
    protected GameObject breakEffect;


    //public ObjectType Type
    //{
    //    get { return type; }
    //}

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// ダメージを与えられるときに呼び出し
    /// </summary>
    /// <param name="damagePoint"></param>
    public abstract void BeginDamage();


}
