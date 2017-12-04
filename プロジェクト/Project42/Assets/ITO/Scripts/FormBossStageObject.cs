using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormBossStageObject : MonoBehaviour
{


    [SerializeField]
    private List<GameObject> enemyObjectTypes;
    /// <summary>
    /// 最初に生成するコアの数
    /// </summary>
    [SerializeField]
    private int initEnemyIndex;
    /// <summary>
    /// 生成するコアのリスト
    /// </summary>
    private List<GameObject> cores = new List<GameObject>();
    public List<GameObject> Cores
    {
        get { return cores; }
    }

    /// <summary>
    /// 生成するコアの位置
    /// </summary>
    [SerializeField]
    private List<Vector2> formPositions;

    /// <summary>
    /// コアのプレファブ
    /// </summary>
    private GameObject corePrefab;

    [SerializeField]
    private List<Vector2> canFormPositions;


    private void Awake()
    {
        //プレファブの読込
        corePrefab = Resources.Load<GameObject>("Prefab/BeDestroyedObject/Core");


    }

    // Use this for initialization
    void Start()
    {

        canFormPositions = formPositions;
        InitFormEnemy();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// ボス生成時のコアの生成
    /// </summary>
    public void InitFormEnemy()
    {

        for (int i = 0; i < initEnemyIndex; i++)
        {
            FormRandomEnemy();
        }
    }

    /// <summary>
    /// 敵のランダム生成
    /// </summary>
    public void FormRandomEnemy()
    {
        GameObject obj = enemyObjectTypes[Random.Range(0, enemyObjectTypes.Count)];
        Vector2 pos = (Vector2)transform.position + formPositions[Random.Range(0, formPositions.Count)];
        Instantiate(obj, pos, Quaternion.identity);
    }


    /// <summary>
    /// コアの生成
    /// </summary>
    public void FormCore()
    {
        //リストからランダムで位置を決定して生成
        int index = Random.Range(0, canFormPositions.Count);
        Vector2 pos = (Vector2)transform.position + canFormPositions[index];
        GameObject obj = Instantiate(corePrefab, pos, Quaternion.identity);
        canFormPositions.RemoveAt(index);
    }


    /// <summary>
    /// コアの破壊
    /// </summary>
    /// <param name="obj"></param>
    public void BreakCore(GameObject obj)
    {
        //配置可能な位置のListにコアの座標を格納
        canFormPositions.Add((Vector2)obj.transform.position - (Vector2)transform.position);
        //オブジェクトの削除
        // cores.Remove(obj);
        Destroy(obj);
    }


}
