using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class FormEnemyTable : Serialize.TableBase<GameObject, int, FormEnemyPair>
//{

//}

//[System.Serializable]
//public class FormEnemyPair : Serialize.KeyAndValue<GameObject, int>
//{
//    public FormEnemyPair(GameObject key, int value) : base(key, value)
//    {
//    }
//}
public class FormEnemyObject : MonoBehaviour
{


    /// <summary>
    /// エネミーの種類のリスト
    /// </summary>
    [SerializeField]
    private List<GameObject> enemysType;

    /// <summary>
    /// 生成するエネミーのリスト
    /// </summary>
    [SerializeField]
    protected List<GameObject> formEnemys;


    //生成するエネミーの取得
    public List<GameObject> FormEnemys
    {
        get { return formEnemys; }
    }


    // Use this for initialization
    void Start()
    {
        
        //FormEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("リストの数" + formEnemys.Count);
    }


    /// <summary>
    /// 途中でエネミーを追加する時など
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemy(GameObject enemy,Vector2 pos)
    {
        GameObject g = (GameObject)Instantiate(enemy);
        g.transform.position = pos;
        formEnemys.Add(g);
        Debug.Log("エネミーの数" + formEnemys.Count);
    }

    /// <summary>
    /// リストからエネミーを削除
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveNenemy(GameObject enemy)
    {
        formEnemys.Remove(enemy);
    }

    /// <summary>
    /// 全てのエネミーの削除
    /// </summary>
    public void AllDestroyEnemy()
    {
        foreach(var e in formEnemys)
        {
            Destroy(e);
        }
        formEnemys.Clear();
    }
}
