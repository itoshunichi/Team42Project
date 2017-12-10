using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Energy energy;
    Image image;
    public Sprite[] sprite;
    public GameObject[] barSide;//エネルギーバーの両端のポイント
    float beforeFillAmout;//値が変わる前
    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
    }

    void Update()
    {
        FillAmout();
        if (energy.GetEnergy() <= 0)
        {
            SceneNavigater.Instance.Change("GameOver");
        }

        barSide[1].transform.position = Vector2.Lerp(barSide[0].transform.position, barSide[2].transform.position, image.fillAmount);

    }

    //円ゲージ
    private void FillAmout()
    {
        image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
        if (image.fillAmount >= 0.5f) image.sprite = sprite[2];
        else if (image.fillAmount >= 0.25f && image.fillAmount < 0.5f) image.sprite = sprite[1];
        else if (image.fillAmount < 0.25f) image.sprite = sprite[0];
    }


}
