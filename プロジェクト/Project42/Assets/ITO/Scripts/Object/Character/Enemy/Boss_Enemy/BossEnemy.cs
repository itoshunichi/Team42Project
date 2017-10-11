using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class FormEnemyTable:Serialize.TableBase<GameObject,int,FormEnemyPair>
//{

//}

//[System.Serializable]
//public class FormEnemyPair : Serialize.KeyAndValue<GameObject, int>
//{
//    public FormEnemyPair(GameObject key, int value) : base(key, value)
//    {
//    }
//}


/// <summary>
/// ボスエネミーに継承
/// </summary>
public class BossEnemy:Enemy {

    ////生成するエネミーの種類と数
    //[SerializeField]
    //protected FormEnemyTable formEnemyTable;

    //[SerializeField]
   // private List<Vector2> formEnemyPositions = new List<Vector2>();

    //生成するエネミーのリスト
   // protected List<GameObject> formEnemys = new List<GameObject>();

    ////生成するエネミーの取得
    //public List<GameObject> FormEnemys
    //{
    //    get { return formEnemys; }
    //}


    ////バリアを出す判定
    //protected bool isBarrier;

    ////バリア復活時間
    //[SerializeField]
    //protected int attackChanceTime;

    //float attackChance_Timer = 0;


    void Start()
    {
        hp = parameter.maxHp;
       // isBarrier = true;
      //  FormEnemy();
    }

    void Update()
    {
       // Debug.Log("敵の数"+formEnemys.Count);
       // Debug.Log("体力:" + hp);
       // InvalidSwitch();
       // AttackChanceTime();
        Dead();
    }

    


    ///// <summary>
    ///// エネミーの生成
    ///// </summary>
    //private void FormEnemy()
    //{
    //    GameObject enemy;
    //    //生成エネミーテーブルで設定したエネミーをリストに入れる
    //    foreach(KeyValuePair<GameObject,int>pair in formEnemyTable.GetTable())
    //    {
    //       for(int i = 0;i<pair.Value;i++)
    //        {
    //            enemy = (GameObject)Instantiate(pair.Key);
    //            formEnemys.Add(enemy);
    //        }
    //       //座標をSceneViewで設定した位置に設定
    //       for(int i = 0;i<formEnemys.Count;i++)
    //        {
    //            formEnemys[i].transform.position = (Vector2)transform.position+formEnemyPositions[i];
    //        }
    //    }
    //}


    ///// <summary>
    ///// リストからobjを削除する
    ///// </summary>
    ///// <param name="obj"></param>
    //public void RemoveEnemy(GameObject obj)
    //{
    //    formEnemys.Remove(obj);
    //}

    ///// <summary>
    ///// バリアの無効
    ///// </summary>
    //public void InvalidSwitch()
    //{
    //    //エネミーがいなかったらバリア無効
    //    if (formEnemys.Count == 0)
    //    {
    //        isBarrier = false;
    //    }
    //}

    /// <summary>
    /// ダメージの呼び出し
    /// </summary>
    public override void BeginDamage(int damagePoint)
    {
        ////バリアがあったら
        //if (isBarrier)
        //{
        //    //バリアの演出
        //    Debug.Log("バリア!");
        //}
        //else
        //{
        //    //ダメージ状態に
        //    mode = EnemyMode.DAMAGE;
        //    //HPを減らす
        //    hp-=damagePoint;
        //}

        //ダメージ状態に
        mode = EnemyMode.DAMAGE;
        //HPを減らす
        hp -= damagePoint;
    }

    //private void AttackChanceTime()
    //{ 
    //    //バリアが無効の時に
    //    if(!isBarrier)
    //    {
    //        attackChance_Timer += Time.deltaTime;
    //        if (attackChance_Timer>attackChanceTime)
    //        {
    //            isBarrier = true;
    //            FormEnemy();
    //            attackChance_Timer = 0;
    //        }
    //    }
    //}

    protected override void Dead()
    {
        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }
}
