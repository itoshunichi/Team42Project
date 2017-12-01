using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 破壊されるオブジェクト
/// </summary>
public abstract class BeDestroyedObject : MonoBehaviour
{
    [SerializeField]
    protected GameObject breakEffect;
    [SerializeField]
    protected GameObject energy;
    protected ObjectType type;


    public ObjectType Type
    {
        get { return type; }
    }

    protected virtual void Start()
    {

       
    }

    protected virtual void Update()
    {
    }
  
    
    /// <summary>
    /// ダメージを与えられるときに呼び出し
    /// </summary>
    /// <param name="damagePoint"></param>
    public virtual void BeginDamage()
    {
        Instantiate(breakEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        Instantiate(energy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

   
}
