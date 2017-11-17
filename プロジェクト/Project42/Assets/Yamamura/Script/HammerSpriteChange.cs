using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSpriteChange : MonoBehaviour {

    Energy energy;       //Energy
    public Sprite[] hammerLv;

	// Use this for initialization
	void Start () {
        energy = GetComponent<Energy>();
	}

    //レベルによって見た目を変える
    public void SpriteChange()
    {
        if (energy.GetEnergy() < 100) GetComponent<SpriteRenderer>().sprite = hammerLv[0];
        else if (energy.GetEnergy() >= 100 && energy.GetEnergy() < 200) GetComponent<SpriteRenderer>().sprite = hammerLv[1];
        else if (energy.GetEnergy() >= 200) GetComponent<SpriteRenderer>().sprite = hammerLv[2];
    }

}
