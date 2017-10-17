using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    /// <summary>
    /// 体力
    /// </summary>
    [SerializeField]
    private int hp;

    /// <summary>
    /// エネルギーの最大値
    /// </summary>
    [SerializeField]
    private float maxEnergy;

    /// <summary>
    /// ウェーブ
    /// </summary>
    private GameObject wave;
    private GameObject wavePrefab;

    /// <summary>
    /// ウェーブの間隔
    /// </summary>
    [SerializeField]
    private int waveTime;

    /// <summary>
    /// ウェーブ準備開始時間
    /// </summary>
    [SerializeField]
    private int startReadyWaveTime;

    /// <summary>
    /// ウェーブ用のタイマー
    /// </summary>
    private float waveTimer = 0;


    /// <summary>
    /// エネルギー
    /// </summary>
    private float energy = 0;


    void Start()
    {

        wavePrefab = Resources.Load<GameObject>("Prefab/MotherWave");
    }

    // Update is called once per frame
    void Update()
    {

        GameObject.Find("BossEnergyText").GetComponent<Text>().text = "ボスエネルギー" + energy;
        InstantiateWave();

    }

    /// <summary>
    /// エネルギーの追加
    /// </summary>
    public void AddEnergy(float energy)
    {
        this.energy += energy;

    }

    /// <summary>
    /// ウェーブの生成
    /// </summary>
    private void InstantiateWave()
    {
        if (waveTimer <= waveTime)
        {
            waveTimer += Time.deltaTime;
            Debug.Log((int)waveTimer);
        }
        else
        {
            Debug.Log("end");
            wave = (GameObject)Instantiate(wavePrefab);
            waveTimer = 0;
        }
    }

    public void BeginDamage()
    {
        hp -= 1;
        InterruptWave();
    }

    /// <summary>
    /// ウェーブ中断
    /// </summary>
    private void InterruptWave()
    {
        //ウェーブ中断時間だったら
        if (IsReadyWaveTime())
        {
            Destroy(wave);//ウェーブ削除
        }
    }

    /// <summary>
    /// ウェーブ準備時間に突入したかどうか
    /// </summary>
    /// <returns></returns>
    private bool IsReadyWaveTime()
    {
        return waveTimer>=startReadyWaveTime;
    }





}
