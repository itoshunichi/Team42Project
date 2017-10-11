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
public class FormBeDestroyedObject : MonoBehaviour
{


    /// <summary>
    /// エネミーの種類のリスト
    /// </summary>
    [SerializeField]
    private List<GameObject> objectsType;

    /// <summary>
    /// 生成するエネミーのリスト
    /// </summary>
    [SerializeField]
    protected List<GameObject> formObjects;


    //生成するエネミーの取得
    public List<GameObject> FormObjects
    {
        get { return formObjects; }
    }


    // Use this for initialization
    void Start()
    {
        
        //FormEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("リストの数" + formObjects.Count);
    }


    /// <summary>
    /// 途中でオブジェクトーを追加する時など
    /// </summary>
    /// <param name="enemy"></param>
    public void AddObject(GameObject enemy,Vector2 pos)
    {
        GameObject g = (GameObject)Instantiate(enemy);
        g.transform.position = pos;
        formObjects.Add(g);
        Debug.Log("オブジェクトの数" + formObjects.Count);
    }

    /// <summary>
    /// リストからオブジェクトを削除
    /// </summary>
    /// <param name="enemy"></param>
    public void DestoryObject(GameObject enemy)
    {
        formObjects.Remove(enemy);
        Destroy(enemy);
    }

   
}
