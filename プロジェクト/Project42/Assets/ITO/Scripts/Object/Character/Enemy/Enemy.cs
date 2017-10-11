using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    /// <summary>
    /// エネミーのパラメーター
    /// </summary>
    [SerializeField]
   protected EnemyParameter parameter;

    protected  EnemyType type;
    public EnemyType Type
    {
        get { return type; }
    }



    ////HPの最大値
    //[SerializeField]
    //protected int maxHp;

    //HP
    protected int hp ;
    //状態
    protected EnemyMode mode;

    protected int damagePoint;

    /// <summary>
    /// ダメージの呼び出し
    /// </summary>
    public abstract void BeginDamage(int damagePoint);

    /// <summary>
    /// 死亡処理
    /// </summary>
    protected abstract void Dead();

   
    /// <summary>
    /// エネミーの現在のモードのチェック
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public bool CheckMode(EnemyMode mode)
    {
        return this.mode == mode;
    }
}
