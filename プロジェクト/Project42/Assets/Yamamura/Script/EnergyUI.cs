using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Energy energy;
    Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        FillAmout();
        if (energy.GetEnergy() <= 0)
        {
            SceneNavigater.Instance.Change("GameOver");
        }
    }

    //円ゲージ
    private void FillAmout()
    {
        image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
    }
}
