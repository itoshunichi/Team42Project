using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    float energy = 0;
    public float getEnergy;
    public int level;
    public float levelUpEnergy;
    public float levelMaxEnergy;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Level();
    }

    public void AddEnergy()
    {
        this.energy += getEnergy;
    }

    public float GetEnergy()
    {
        return energy;
    }
    //レベルの上げ下げ
    private void Level()
    {
        if (energy >= levelUpEnergy * 2)
            level = 3;
        else if (energy >= levelUpEnergy)
            level = 2;
        else if (energy < levelUpEnergy)
            level = 1;

        if (energy >= levelMaxEnergy)
            energy = levelMaxEnergy;
    }

    public bool LevelMax()
    {
        if (energy >= levelMaxEnergy)
        {
            return true;
        }
        return false;
    }
}
