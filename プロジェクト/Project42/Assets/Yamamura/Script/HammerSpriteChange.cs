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
        if (energy.level == 1) GetComponent<SpriteRenderer>().sprite = hammerLv[0];
        else if (energy.level == 2 && energy.GetEnergy() < 200) GetComponent<SpriteRenderer>().sprite = hammerLv[1];
        else if (energy.level == 3) GetComponent<SpriteRenderer>().sprite = hammerLv[2];
    }

}
