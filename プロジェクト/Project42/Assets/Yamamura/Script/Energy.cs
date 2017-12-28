using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public float energy;        //エネルギー
    public float addEnergy;     //足す値
    public float minusEnergy;   //引く値
    public float maxEnergy;     //最大エネルギー
    public float combTimeLimt;  //コンボ制限時間
    float energyTime;           //時間
    float combTime;             //コンボ判定時間
    int energyCount;            //Energyの数
    private bool isDamage;
    int combCount;              //コンボ
    public bool isReduceEnergy = false;
    private bool isImmortality = false;    //不死状態
    private bool isCure;        //エネルギーが回復した判定
    private int maxCombo;
    public int MaxCombo
    {
        get { return maxCombo; }
    }

    public bool IsImmortality
    {
        get { return isImmortality; }
        set { isImmortality = value; }
    }
    public bool IsDamage
    {
        get { return isDamage; }
        set { isDamage = value; }
    }
    float energyStartTime;
    // Use this for initialization
    void Start()
    {
        isReduceEnergy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReduceEnergy)
        {
            energyStartTime += Time.deltaTime;
            energy = Mathf.Lerp(0.1f,maxEnergy,energyStartTime);
            if (energyStartTime >= 1.0f) isReduceEnergy = true;
        }
        else if (isReduceEnergy)
        {
            energyTime += Time.deltaTime;
            if (energyTime >= 0.01f)
            {
                energy -= 0.01f;
                energyTime = 0;
            }
            combTime += Time.deltaTime;
            if (combTime >= combTimeLimt)
            {
                combTime = 0;
                if (combCount > 0) combCount = 0;
            }
            energy = Mathf.Clamp(energy, 0, maxEnergy);

            if (energy == 0)
            {
                GameObject.Find("Energy");
            }
        }
    }

    public void CombPuls()
    {
        combTime = 0;
        combCount++;
        maxCombo = Mathf.Max(combCount);
    }

    /// <summary>
    /// Energyを足す
    /// </summary>
    public void AddEnergy()
    {
        isCure = true;
        combTime = 0;
        energy += addEnergy;
    }

    public void MinusEnergy()
    {
        if (energyCount >= 1 && (energy - minusEnergy) <= 0)
            isImmortality = true;
        else
            isImmortality = false;
        isDamage = true;
        energy -= minusEnergy;
    }

    public void CombAddEnergy()
    {
        energy += (addEnergy + (combCount / 2) + 1) - (minusEnergy - addEnergy * energyCount);
    }

    public int GetCombCount()
    {
        return combCount;
    }

    public void AddEnergyCount(int count)
    {
        energyCount+= count;
    }

    public float GetEnergy()
    {
        return energy;
    }
    public bool IsCure
    {
        get { return isCure; }
        set { isCure = value; }
    }
}
