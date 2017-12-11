using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundChange : MonoBehaviour
{
    public Image bgmImage;
    public Image seImage;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //値制御
        Mathf.Clamp(bgmImage.fillAmount, 0.0f, 1.0f);
        Mathf.Clamp(seImage.fillAmount, 0.0f, 1.0f);
    }

    public void BGMPlus()
    {
        bgmImage.fillAmount += 0.25f;
        AudioManager.Instance.ChangeBGMVolume(bgmImage.fillAmount);
    }

    public void SEPlus()
    {
        seImage.fillAmount -= 0.25f;
        AudioManager.Instance.ChangeSEVolume(seImage.fillAmount);
    }

    public void BGMMinus()
    {
        bgmImage.fillAmount -= 0.25f;
        AudioManager.Instance.ChangeBGMVolume(bgmImage.fillAmount);
    }

    public void SEMinus()
    {
        seImage.fillAmount -= 0.25f;
        AudioManager.Instance.ChangeSEVolume(seImage.fillAmount);
    }
}
