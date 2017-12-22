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
    /// <summary>
    /// BGM音量上げる
    /// </summary>
    public void BGMPlus()
    {
        bgmImage.fillAmount += 0.2f;
        AudioManager.Instance.ChangeBGMVolume(bgmImage.fillAmount);
    }
    /// <summary>
    /// SE音量上げる
    /// </summary>
    public void SEPlus()
    {
        seImage.fillAmount += 0.2f;
        AudioManager.Instance.ChangeSEVolume(seImage.fillAmount);
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
    }
    /// <summary>
    /// BGM音量下げる
    /// </summary>
    public void BGMMinus()
    {
        bgmImage.fillAmount -= 0.2f;
        AudioManager.Instance.ChangeBGMVolume(bgmImage.fillAmount);
    }
    /// <summary>
    /// SE音量下げる
    /// </summary>
    public void SEMinus()
    {
        seImage.fillAmount -= 0.2f;
        AudioManager.Instance.ChangeSEVolume(seImage.fillAmount);
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
    }
}
