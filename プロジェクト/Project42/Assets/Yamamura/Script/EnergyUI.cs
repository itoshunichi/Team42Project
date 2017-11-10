using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyUI : MonoBehaviour {

    public GameObject hammer;
    public Energy energy;
    Image image;
    public Sprite level1;
    public Sprite level2;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        FillAmout();
        Move();
	}

    //円ゲージ
    private void FillAmout()
    {
        if (energy.level == 1)
        {
            image.sprite = level1;
            image.fillAmount = energy.GetEnergy() / energy.levelUpEnergy;
        }
        else if (energy.level == 2)
        {
            image.sprite = level2;
            image.fillAmount = (energy.GetEnergy() - energy.levelUpEnergy) / energy.levelUpEnergy;
        }
    }

    //移動処理
    private void Move()
    {
        gameObject.transform.position = hammer.transform.position;
        gameObject.transform.rotation = hammer.transform.rotation;
    }
}
