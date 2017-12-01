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
    private List<GameObject> formObjects;

    /// <summary>
    /// ランダムで生成するオブジェクトのグループ
    /// </summary>
    [SerializeField]
    private List<GameObject> randomFormObjectsGroup;

    /// <summary>
    /// オブジェクトをランダムで生成する位置の候補
    /// </summary>
    [SerializeField]
    private List<Vector2> randomFormPoints;

    /// <summary>
    /// ランダムでオブジェクトを生成する間隔
    /// </summary>
    [SerializeField]
    private float randomFormTime;

    



    //生成するエネミーの取得
    public List<GameObject> FormObjects
    {
        get { return formObjects; }
    }


    // Use this for initialization
    void Start()
    {
        StartCoroutine(RandomFormObject());
        //FormEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("リストの数" + formObjects.Count);
    }


    /// <summary>
    /// 途中でオブジェクトーを追加する時など
    /// </summary>
    /// <param name="obj"></param>
    public void AddObject(GameObject obj,Vector2 pos)
    {
        GameObject g = (GameObject)Instantiate(obj,pos,Quaternion.identity);
       // g.transform.position = pos;
        //子オブジェクトがなかったらそのまま生成
        if (g.transform.childCount == 0)
        {
            formObjects.Add(g);
        }
        //子オブジェクトがあったら
        else
        {
            for(int i = 0;i<g.transform.childCount;i++)
            {
                formObjects.Add(g.transform.GetChild(i).gameObject);
            }
        }
       
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

    /// <summary>
    /// 秒間隔でランダムな位置にランダムでオブジェクトを生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator RandomFormObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomFormTime);
            GameObject obj = randomFormObjectsGroup[Random.Range(0, randomFormObjectsGroup.Count)];
            Vector2 pos = randomFormPoints[Random.Range(0, randomFormPoints.Count)];
           // Debug.Log(pos);
            AddObject(obj, pos);
        }
    }


}
