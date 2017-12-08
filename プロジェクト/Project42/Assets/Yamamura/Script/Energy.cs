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
    float energyTime;           //時間
    float combTime;             //コンボ判定時間
    int combCount;              //コンボ
    public bool isReduceEnergy = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReduceEnergy) return;
        energyTime += Time.deltaTime;
        if (energyTime >= 0.01f)
        {
            energy -= 0.01f;
            energyTime = 0;
        }
        combTime += Time.deltaTime;
        if (combTime >= 1.5f)
        {
            combTime = 0;
            if (combCount > 0) combCount = 0;
        }
    }

    public void CombPuls()
    {
        combTime = 0;
        combCount++;
    }

    /// <summary>
    /// Energyを足す
    /// </summary>
    public void AddEnergy()
    {
        combTime = 0;
        energy += addEnergy;
    }

    public void MinusEnergy()
    {
        energy -= minusEnergy;
    }

    public void CombAddEnergy()
    {
        energy += addEnergy * (combCount + combCount);
    }

    public int GetCombCount()
    {
        return combCount;
    }

    public float GetEnergy()
    {
        return energy;
    }

}
