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
    private GameObject attckWave;
    private GameObject stopWave;
    // private GameObject wavePrefab;


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

        attckWave = Resources.Load<GameObject>("Prefab/Wave/AttackWave");
        stopWave = Resources.Load<GameObject>("Prefab/Wave/StopWave");
        StartCoroutine(InstantiateWave());
    }

    // Update is called once per frame
    void Update()
    {

        GameObject.Find("BossEnergyText").GetComponent<Text>().text = "ボスエネルギー" + energy;
        SetScale();
        // InstantiateWave();

    }

    public void BeginDamage()
    {
        // hp -= 1;
        InstantiateStopWave();
        //InterruptWave();
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// スケールの設定
    /// </summary>
    private void SetScale()
    {
        //エネルギーが0以下だったらスケールは変わらない
        if (energy <= 0) transform.localScale = new Vector3(1, 1, 1);

        float scale = energy / 100;
        transform.localScale = new Vector3(1 + scale, 1 + scale, 1);
    }

    #region　エネルギー関係

    /// <summary>
    /// エネルギーの追加
    /// </summary>
    public void AddEnergy(float energy)
    {
        this.energy += energy;
        MaxEnergy();
    }

    /// <summary>
    /// エネルギーが最大まで達したときの処理
    /// </summary>
    private void MaxEnergy()
    {
        //エネルギーが最大以上になったら
        if (energy >= maxEnergy)
        {
            //ゲームオーバーシーンに
            SceneNavigater.Instance.Change("GameOver");
        }
    }

    #endregion

    #region ウェーブ関係
    ///// <summary>
    ///// ウェーブの生成
    ///// </summary>
    //private void InstantiateWave()
    //{
    //    if (waveTimer <= waveTime)
    //    {
    //        waveTimer += Time.deltaTime;
    //        Debug.Log((int)waveTimer);
    //    }
    //    else
    //    {
    //        Debug.Log("end");
    //        wave = (GameObject)Instantiate(wavePrefab);
    //        waveTimer = 0;
    //    }
    //}


    private IEnumerator InstantiateWave()
    {
        

        while(true)
        {
            yield return new WaitForSeconds(waveTime);
            InstantiateAttackWave();
        }
    }

    public void InstantiateAttackWave()
    {
        Instantiate(attckWave);
    }

    private void InstantiateStopWave()
    {
        Instantiate(stopWave);
    }

    ///// <summary>
    ///// ウェーブ中断
    ///// </summary>
    //private void InterruptWave()
    //{
    //    //ウェーブ準備時間だったら
    //    if (IsReadyWaveTime())
    //    {
    //        Destroy(attckWave);//ウェーブ削除
    //    }
    //}

    ///// <summary>
    ///// ウェーブ準備時間に突入したかどうか
    ///// </summary>
    ///// <returns></returns>
    //private bool IsReadyWaveTime()
    //{
    //    return waveTimer >= startReadyWaveTime;
    //}

    #endregion









}
