﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormBossStageObject : MonoBehaviour
{
    /// <summary>
    /// 生成するエネミーの種類
    /// </summary>
    [SerializeField]
    private List<GameObject> enemyObjectTypes;

    [SerializeField]
    private List<GameObject> shieldEnemyObjectTypes;

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

    private int tarkingEnemyCount = 0;


    void Start()
    {
        //InitFormEnemy();

        //SetCanFormPositions();
    }

    private void SetCanFormPositions()
    {
        if (canFormPositions.Count == 0)
        {
            canFormPositions = setFormPositions;
        }
    }

    void Update()
    {
        Debug.Log("追尾" + tarkingEnemyCount);
        SetCanFormPositions();
    }

    /// <summary>
    /// ボス生成時の生成
    /// </summary>
    public void InitFormSheildEnemy()
    {

        for (int i = 0; i < initShieldEnemyIndex; i++)
        {
            //オブジェクトの種類をランダムで決定
            GameObject obj = shieldEnemyObjectTypes[Random.Range(0, shieldEnemyObjectTypes.Count)];
            // obj.GetComponent<Enemy>().ChangeMode(EnemyMode.SHIELD);
            //位置を候補からランダムで決定
            //Vector2 randomPos = setFormPositions[Random.Range(0, setFormPositions.Count)];
            Vector2 pos = (Vector2)Camera.main.transform.position + setFormPositions[i];
            //canFormPositions.Remove(pos);
            Quaternion rot = obj.transform.rotation;
            //生成
            GameObject formEnemy = Instantiate(obj, pos, rot);
            //formEnemy.GetComponent<Enemy>().SetShieldModeColor();
            shieldEnemys.Add(formEnemy);
        }
    }

    public void InitFormNormalEnemy()
    {
        for (int i = initShieldEnemyIndex; i < initShieldEnemyIndex + enemyIndex; i++)
        {
            GameObject obj;
            if(tarkingEnemyCount<3)
            {
                obj = enemyObjectTypes[0];
                tarkingEnemyCount++;
            }
            else
            {
                obj = enemyObjectTypes[Random.Range(1, enemyObjectTypes.Count)];
            }
            //オブジェクトの種類をランダムで決定
            //GameObject obj = enemyObjectTypes[Random.Range(0, enemyObjectTypes.Count)];
            //位置を候補からランダムで決定
            //Vector2 randomPos = setFormPositions[Random.Range(0, setFormPositions.Count)];
            Vector2 pos = (Vector2)Camera.main.transform.position + setFormPositions[i];
            //canFormPositions.Remove(pos);
            Quaternion rot = obj.transform.rotation;
            //生成
            GameObject formEnemy = Instantiate(obj, pos, rot);
            // formEnemy.GetComponent<Enemy>().ChangeMode(EnemyMode.TARKING);
            enemys.Add(formEnemy);
        }
    }


    public void ChangeShieldEnemy(GameObject enemy)
    {


        GameObject obj = shieldEnemyObjectTypes[Random.Range(0, shieldEnemyObjectTypes.Count)];
        shieldEnemys.Add(Instantiate(obj, enemy.transform.position, obj.transform.rotation));
        
        DestroyEnemy(enemy, false);


    }



    /// <summary>
    /// 敵のランダム生成
    /// 生成するときの状態を設定
    /// </summary>
    public void FormRandomEnemy()
    {
        if (enemys.Count > enemyIndex) return;
        if (FindObjectOfType<GamePlayEvent>().IsGameEnd) return;
        GameObject obj;
        if (tarkingEnemyCount < 3)
        {
            obj = enemyObjectTypes[0];
            tarkingEnemyCount++;
        }
        else
        {
            obj = enemyObjectTypes[Random.Range(1, enemyObjectTypes.Count)];
        }
        Vector2 pos = (Vector2)Camera.main.transform.position + setFormPositions[Random.Range(0, setFormPositions.Count)];
        canFormPositions.Remove(pos);
        Quaternion rot = obj.transform.rotation;
        //生成
        GameObject formEnemy = Instantiate(obj, pos, rot);
        //状態をシールドに
        //formEnemy.GetComponent<Enemy>().ChangeMode(EnemyMode.TARKING);

        enemys.Add(formEnemy);

    }



    public void DestroyEnemy(GameObject enemy, bool isDamage)
    {
        if (enemy.GetComponent<Enemy>().IsSelectMode(EnemyMode.SHIELD))
        {
            shieldEnemys.Remove(enemy);
            if (shieldEnemys.Count != 0)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_ENERGYGET);
            }
        }
        else
        {
            if (enemy.GetComponent<TarkingEnemy>()) tarkingEnemyCount--;
            enemys.Remove(enemy);
            if (isDamage)
                AudioManager.Instance.PlaySE(AUDIO.SE_ENERGYGET);
        }
        Destroy(enemy);
    }

    public void AllEnemyStop()
    {
        foreach (var e in FindObjectsOfType<Enemy>())
        {
            e.Stop();
        }
    }

    public void AllEnemyDead()
    {
        foreach (var e in FindObjectsOfType<Enemy>())
        {
            e.BeginDamage(false);
        }
    }
}
