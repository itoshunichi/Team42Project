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
    private bool isDamage;
    int combCount;              //コンボ
    public bool isReduceEnergy = false;
    public bool IsDamage
    {
        get { return isDamage; }
        set { isDamage = value; }
    }
    // Use this for initialization
    void Start()
    {
        isReduceEnergy = true;
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
        if (combTime >= combTimeLimt)
        {
            combTime = 0;
            if (combCount > 0) combCount = 0;
        }
        energy = Mathf.Clamp(energy, 0, maxEnergy);
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
        isDamage = true;
        energy -= minusEnergy;
    }

    public void CombAddEnergy()
    {
        energy += addEnergy + (combCount / 2) + 1;
    }

    public int GetCombCount()
    {
        return combCount;
    }

    public void SetCombCount(int setNum)
    {
        combCount = setNum;
    }

    public float GetEnergy()
    {
        return energy;
    }
}
