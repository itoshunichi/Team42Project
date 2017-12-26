using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Energy energy;
    Image image;
    public Image redGageBar;        //ダメージ受けたとき用のバー
    public Image greenGageBar;      //回復した時用のバー
    public Sprite[] sprite;
    public GameObject[] barSide;    //エネルギーバーの両端のポイント
    float beforeFillAmout;          //値が変わる前
    float gageTime;                 //ゲージがダメージや回復により変動する時間
    float delay;                    //本ゲージが変動した値までに移動するディレイ時間

    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
    }

    void Update()
    {
        Cure();
        FillAmout();
        barSide[1].transform.position = Vector2.Lerp(barSide[0].transform.position, barSide[2].transform.position, image.fillAmount);
        Damage();
    }

    private void Cure()
    {
        greenGageBar.fillAmount = (energy.GetEnergy() / energy.maxEnergy);//ミドリゲージ
        if (!energy.IsCure) image.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
        else if (energy.IsCure)
        {
            if (gageTime >= 1.0f)
            {
                gageTime = 0.0f;
                energy.IsCure = false;
                delay = 0.0f;
            }
            delay += Time.deltaTime;
            if (delay > 0.5f) gageTime += Time.deltaTime / 2;
            image.fillAmount = Mathf.Lerp(image.fillAmount, greenGageBar.fillAmount, gageTime);
        }
    }

    //円ゲージ
    private void FillAmout()
    {
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
            if (gageTime >= 1.0f)
            {
                gageTime = 0.0f;
                energy.IsDamage = false;
                delay = 0.0f;
            }
            delay += Time.deltaTime;
            if (delay > 0.5f) gageTime += Time.deltaTime / 2;
            redGageBar.fillAmount = Mathf.Lerp(redGageBar.fillAmount, image.fillAmount, gageTime);
        }
        else if (!energy.IsDamage)
        {
            redGageBar.fillAmount = (energy.GetEnergy() / energy.maxEnergy);
        }
    }
}
