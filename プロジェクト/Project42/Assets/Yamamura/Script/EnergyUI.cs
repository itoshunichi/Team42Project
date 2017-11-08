using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyUI : MonoBehaviour {

    public GameObject hammer;
    public Energy energy;
    Image image;
    public float fullEnergy;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        image.fillAmount = energy.GetEnergy() / fullEnergy;
        gameObject.transform.position = hammer.transform.position;
        gameObject.transform.rotation = hammer.transform.rotation;
	}
}
