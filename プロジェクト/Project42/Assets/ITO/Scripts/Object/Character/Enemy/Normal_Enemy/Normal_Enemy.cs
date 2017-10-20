//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Normal_Enemy : Enemy
//{

//    ///// <summary>
//    ///// 気絶するHP
//    ///// </summary>
//    //[SerializeField]
//    //private int stunning_HP;


//    ///// <summary>
//    ///// 回復時間
//    ///// </summary>
//    //[SerializeField]
//    //private float recoveryTime;

//    /// <summary>
//    /// 回復用のタイマー
//    /// </summary>
//    private float timer_Recovery = 0;



//    public override void BeginDamage(int damagePoint)
//    {
//        timer_Recovery = 0;
//        mode = EnemyMode.DAMAGE;
//        hp -= damagePoint;
//    }

//    /// <summary>
//    /// 死亡処理
//    /// </summary>
//    protected override void Dead()
//    {
//        //体力が0より上だったら
//        if (hp > 0) return;
//        //ダメージになったら
//        if (mode == EnemyMode.DAMAGE)
//        {
//            //ダメージのアニメーションが終わったらとかに変更
//            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossEnemy>().RemoveEnemy(gameObject);
//            Destroy(gameObject);
//        }
//    }

//    void Start()
//    {
//        type = parameter.type;
//        hp = parameter.maxHp;
//        adrenalinPoint = parameter.adrenalinPoint;
//    }

//    void Update()
//    {
//        Debug.Log(hp);
//        Dead();
//        SetColor();
//        Stunning();
//        Recovry();
//    }

//    /// <summary>
//    /// 気絶の処理
//    /// </summary>
//    private void Stunning()
//    {
//        //HPが気絶するHP以下だったら気絶モードにする
//        if (hp <= parameter.stunning_HP) mode = EnemyMode.STUNNING;
//    }

//    /// <summary>
//    /// 回復の処理
//    /// </summary>
//    private void Recovry()
//    {
//        //現在のHPが最大値より小さかったら
//        if (hp < parameter.maxHp)
//        {
//            timer_Recovery += Time.deltaTime;

//            if (timer_Recovery > parameter.recoveryTime)
//            {
//                hp++;
//                timer_Recovery = 0;
//            }
//        }
//    }


//    /// <summary>
//    /// 色の設定
//    /// </summary>
//    private void SetColor()
//    {
//        switch (hp)
//        {
//            //HPが１だったら
//            case 1:
//                //色を赤
//                GetComponent<SpriteRenderer>().color = Color.red;
//                break;
//            //HPが２だったら
//            case 2:
//                //色を黄色
//                GetComponent<SpriteRenderer>().color = Color.yellow;
//                break;
//            //HPが３だったら
//            case 3:
//                //色を青
//                GetComponent<SpriteRenderer>().color = Color.blue;
//                break;
//        }


//    }
//}
