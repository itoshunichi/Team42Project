using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    /// <summary>
    /// エネルギーの最大値
    /// </summary>
    private float maxEnergy;

    private float energy = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GameObject.Find("BossEnergyText").GetComponent<Text>().text = "ボスエネルギー" + energy;
		
	}

    /// <summary>
    /// エネルギーの追加
    /// </summary>
    public void AddEnergy(float energy)
    {
        this.energy += energy;
    }
}
