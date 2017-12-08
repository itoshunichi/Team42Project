using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormBossStageObject : MonoBehaviour
{
    /// <summary>
    /// 生成するエネミーの種類
    /// </summary>
    [SerializeField]
    private List<GameObject> enemyObjectTypes;

    /// <summary>
    /// 生成した敵のリスト
    /// </summary>
    private List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> Enemys
    {
        get { return enemys; }
    }
    /// <summary>
    /// シールドエネミーのリスト
    /// </summary>
    private List<GameObject> shieldEnemys = new List<GameObject>();
    public List<GameObject> ShieldEnemys
    {
        get { return shieldEnemys; }
    }


    /// <summary>
    /// 最初に生成するシールドエネミーの数
    /// </summary>
    [SerializeField]
    private int initShieldEnemyIndex;

    /// <summary>
    /// 生成するエネミーの数
    /// </summary>
    [SerializeField]
    private int enemyIndex;

    /// <summary>
    /// 生成する位置
    /// </summary>
    [SerializeField]
    private List<Vector2> setFormPositions;

    /// <summary>
    /// 生成できる位置
    /// </summary>
    private List<Vector2> canFormPositions = new List<Vector2>();


    void Start()
    {
        //InitFormEnemy();
        
        SetCanFormPositions();
    }

    private void SetCanFormPositions()
    {
        
        for(int i = 0;i<setFormPositions.Count;i++)
        {
            canFormPositions.Add((Vector2)Camera.main.transform.position + setFormPositions[i]);
        }
    }
   
    void Update()
    {
    }

    /// <summary>
    /// ボス生成時のシールドエネミーの生成
    /// </summary>
    public void InitFormEnemy()
    {

        for (int i = 0; i < initShieldEnemyIndex; i++)
        {
            FormRandomEnemy(EnemyMode.SHIELD);
        }

        SetCanFormPositions();
    }

    /// <summary>
    ///エネミーを同時に生成
    /// </summary>
    public void FormEnemyGroup()
    {
        for(int i = 0;i<enemyIndex;i++)
        {
            FormRandomEnemy(EnemyMode.NORMAL);
        }
        SetCanFormPositions();
    }


    /// <summary>
    /// 敵のランダム生成
    /// 生成するときの状態を設定
    /// </summary>
    public void FormRandomEnemy(EnemyMode mode)
    {
        //オブジェクトの種類をランダムで決定
        GameObject obj = enemyObjectTypes[Random.Range(0, enemyObjectTypes.Count)];
        //位置を候補からランダムで決定
        //Vector2 randomPos = setFormPositions[Random.Range(0, setFormPositions.Count)];
        Vector2 pos = (Vector2)Camera.main.transform.position + setFormPositions[Random.Range(0, setFormPositions.Count)];
        canFormPositions.Remove(pos);
        Quaternion rot = obj.transform.rotation;
        //生成
       GameObject formEnemy =  Instantiate(obj, pos, rot);
        //状態をシールドに
        formEnemy.GetComponent<Enemy>().ChangeMode(mode);
        //リストに保存
        if (mode == EnemyMode.NORMAL)
        {
            enemys.Add(formEnemy);
        }
        if(mode == EnemyMode.SHIELD)
        {
            shieldEnemys.Add(formEnemy);
        }

    }

    public void DestroyEnemy(GameObject enemy)
    {
        if (enemy.GetComponent<Enemy>().IsSelectMode(EnemyMode.NORMAL))
        {
            enemys.Remove(enemy);
        }
        if(enemy.GetComponent<Enemy>().IsSelectMode(EnemyMode.SHIELD))
        {
            shieldEnemys.Remove(enemy);
        }
        Destroy(enemy);
    }
}
