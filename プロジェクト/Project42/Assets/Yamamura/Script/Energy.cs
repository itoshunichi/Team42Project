using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    float energy = 0;
    public int level = 1;
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

    public void AddEnergy(float energy)
    {
        this.energy += energy;
    }

    public float GetEnergy()
    {
        return energy;
    }
    //レベルの上げ下げ
    private void Level()
    {
        if (energy >= levelUpEnergy && level < 2)
        {
            level = 2;
        }
        else if (energy < levelUpEnergy && level >= 2)
        {
            level = 1;
        }
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
