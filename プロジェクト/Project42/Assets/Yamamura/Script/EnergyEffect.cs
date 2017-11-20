using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEffect : MonoBehaviour {

    public GameObject energyEffect;
    public GameObject energyEffect2;
    public Energy energy;

	// Use this for initialization
	void Start () {
        energyEffect.SetActive(false);
        energyEffect2.SetActive(false);
	}
	
	
    public void EnergyLevelEffect()
    {
        if (energy.level == 2)
        {
            energyEffect.SetActive(true);
            energyEffect2.SetActive(false);
        }
        else if (energy.LevelMax())
        {
            energyEffect.SetActive(false);
            energyEffect2.SetActive(true);
        }
    }
}
