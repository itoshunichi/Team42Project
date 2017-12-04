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
    private List<GameObject> objectsType;

    /// <summary>
    /// 生成するエネミーのリスト
    /// </summary>
    [SerializeField]
    private List<GameObject> formObjects;


    //生成するエネミーの取得
    public List<GameObject> FormObjects
    {
        get { return formObjects; }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    /// <summary>
    /// リストからオブジェクトを削除
    /// </summary>
    /// <param name="enemy"></param>
    public void DestoryObject(GameObject enemy)
    {
        formObjects.Remove(enemy);
        if (formObjects.Count == 0)
        {
            FindObjectOfType<GamePlayEvent>().SetArrowEnabled(true);
            Destroy(enemy);
        }
    }


}
