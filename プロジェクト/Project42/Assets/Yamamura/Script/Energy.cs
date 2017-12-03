using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    public float energy;
    public float addEnergy;
    public float maxEnergy;
    float energyTime; //時間
    float combTime;
    int combCount;//コンボ

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        energyTime += Time.deltaTime;
        if (energyTime >= 0.01f){
            energy -= 0.01f;
            energyTime = 0;
        }
        combTime += Time.deltaTime;
        if (combTime >= 1.5f)
        {
            combTime = 0;
            Debug.Log("Reset");
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
