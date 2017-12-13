using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{

    public GameObject soundButton;
    public GameObject creditButton;
    public GameObject credit;
    public GameObject sound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// サウンド表示ボタン
    /// </summary>
    public void SoundPush()
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
        soundButton.SetActive(false);   //サウンドボタン非表示
        creditButton.SetActive(false);  //クレジットボタン非表示
        sound.SetActive(true);          //サウンド背景表示
    }
    /// <summary>
    /// サウンド閉じるボタン
    /// </summary>
    public void SoundCrosePush()
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
        soundButton.SetActive(true);    //サウンドボタン表示
        creditButton.SetActive(true);   //クレジットボタン表示
        sound.SetActive(false);
    }
    /// <summary>
    /// クレジット表示
    /// </summary>
    public void CreditPush()
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
        soundButton.SetActive(false);   //サウンド表示
        creditButton.SetActive(false);  //クレジットボタン表示
        credit.SetActive(true);         //クレジット背景表示
    }
    /// <summary>
    /// クレジット閉じるボタン
    /// </summary>
    public void CreditCrosePush()
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
        soundButton.SetActive(true);    //サウンドボタン表示
        creditButton.SetActive(true);   //クレジット表示
        credit.SetActive(false);
    }
}
