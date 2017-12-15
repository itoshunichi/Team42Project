using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayText : MonoBehaviour {

    [SerializeField,Tooltip("ミッションのテキストを出すまでの時間")]
    private float startMissionTextTime = 0.5f;

    [SerializeField,Tooltip("テキストの表示時間")]
    private float displayTime = 1f;
    public float DisplayTime
    {
        get { return displayTime; }
    }


    /// <summary>
    /// テキストの背景
    /// </summary>
    private Image textBack;

    /// <summary>
    /// ミッションテキスト
    /// </summary>
    private Text missionText;

    /// <summary>
    /// 現在のウェーブのテキスト
    /// </summary>
    private Text currentWaveText;


    void Start () {
        textBack = GetComponent<Image>();
        missionText = transform.GetChild(1).GetComponent<Text>();
        currentWaveText = transform.GetChild(0).GetComponent<Text>();	
	}


    public void SetMissionText(int waveIndex)
    {
        missionText.text = "エネルギーがなくなる前に\nWAVE" + waveIndex + "のボスを倒せ";
    }

    public void SetCurrentWaveText(int currentWave)
    {
        currentWaveText.text = "WAVE" + currentWave;
    }

    /// <summary>
    /// ミッションテキストの表示開始
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartMissionTextDisplay()
    {
        yield return new WaitForSeconds(startMissionTextTime);

        textBack.enabled = true;
        missionText.enabled = true;

        yield return new WaitForSeconds(displayTime);
        missionText.enabled = false;
        StartCoroutine(StartCurrnetWaveTextDisplay());
    }

    /// <summary>
    /// 現在のウェーブのテキスト表示開始
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCurrnetWaveTextDisplay()
    {
        textBack.enabled = true;
        currentWaveText.enabled = true;

        yield return new WaitForSeconds(displayTime);
        textBack.enabled = false;
        currentWaveText.enabled = false;
    }

    /// <summary>
    /// ゲーム開始時のテキスト表示時間の合計
    /// </summary>
    /// <returns></returns>
    public float TotalStartTextDiplayTime()
    {
        return startMissionTextTime+(displayTime*2);
    }
	
	
}
