using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Energy energy;               
    Image image;                    
    public Image redGageBar;        
    public Sprite[] sprite;         
    public GameObject[] barSide;    //エネルギーバーの両端のポイント
    float beforeFillAmout;          //値が変わる前
    float damegeGageTime;           //赤ゲージが減る時間
    float delay;                    //赤ゲージが減るまでのディレイ
    void Start()
    {
        image = GetComponent<Image>();
        beforeFillAmout = image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
    }

    void Update()
    {
        FillAmout();
        barSide[1].transform.position = Vector2.Lerp(barSide[0].transform.position, barSide[2].transform.position, image.fillAmount);
        Damage();
    }

    //円ゲージ
    private void FillAmout()
    {
        image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
        if (image.fillAmount >= 0.5f) image.sprite = sprite[2];
        else if (image.fillAmount >= 0.25f && image.fillAmount < 0.5f) image.sprite = sprite[1];
        else if (image.fillAmount < 0.25f) image.sprite = sprite[0];
    }

    /// <summary>
    /// ダメージ受けたときのゲージ処理
    /// </summary>
    public void Damage()
    {
        if (energy.IsDamage)
        {
            if (damegeGageTime >= 1.0f)
            {
                damegeGageTime = 0.0f;
                energy.IsDamage = false;
                delay = 0.0f;
            }
            delay += Time.deltaTime;
            if(delay > 0.5f)damegeGageTime += Time.deltaTime / 2;
            redGageBar.fillAmount = Mathf.Lerp(redGageBar.fillAmount, image.fillAmount, damegeGageTime);
        }
    }
}
