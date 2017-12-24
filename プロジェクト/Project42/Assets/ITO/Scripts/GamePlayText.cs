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
    private GameObject textBack;

    private Animator animator;
    /// <summary>
    /// ミッションテキスト
    /// </summary>
    private GameObject missionText;

    /// <summary>
    /// 現在のウェーブのテキスト
    /// </summary>
    private GameObject currentWaveText;


    void Start () {
       
        textBack = transform.GetChild(0).gameObject;
        //animator = transform.GetChild(0).GetComponent<Animator>();
        //animator.enabled = false;
        missionText = transform.GetChild(2).gameObject;
        currentWaveText = transform.GetChild(1).gameObject;	
	}


    public void SetMissionText(int waveIndex)
    {
        missionText.GetComponent<Text>().text = "エネルギーがなくなる前に\nWAVE" + waveIndex + "のボスを倒せ";
    }

    public void SetCurrentWaveText(int currentWave)
    {
        currentWaveText.GetComponent<Text>().text = "WAVE" + currentWave;
    }

    /// <summary>
    /// ミッションテキストの表示開始
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartMissionTextDisplay(int waveIndex)
    {
       // animator.enabled = true;
        yield return new WaitForSeconds(startMissionTextTime);


        SetTextBackEnabled(true);
        SetMissionText(waveIndex);
        SetMissionTextEnabled(true);
        yield return new WaitForSeconds(displayTime);

        SetMissionTextEnabled(false);
        StartCoroutine(StartCurrnetWaveTextDisplay());
    }

    /// <summary>
    /// 現在のウェーブのテキスト表示開始
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCurrnetWaveTextDisplay()
    {
        SetTextBackEnabled(true);
        SetCurrentWaveTextEnabled(true);

        yield return new WaitForSeconds(displayTime);
        SetTextBackEnabled(false);
        SetCurrentWaveTextEnabled(false);
    }

    private void SetTextBackEnabled(bool enabled)
    {
        Animator animator = textBack.GetComponent<Animator>();
        if(enabled)
        {
            animator.enabled = true;
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, 0.0f);
        }
        else
        {
            animator.enabled = false;
        }
        
        textBack.GetComponent<Image>().enabled = enabled;
    }

   

    private void SetCurrentWaveTextEnabled(bool enabled)
    {
        Animator animator = currentWaveText.GetComponent<Animator>();
         if (enabled)
        {
            animator.enabled = true;
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, 0.0f);
        }
        else
        {
            animator.enabled = false;
        }
        currentWaveText.GetComponent<Text>().enabled = enabled;
    }

    private void SetMissionTextEnabled(bool enabled)
    {
        Animator animator = missionText.GetComponent<Animator>();
        if (enabled)
        {
            animator.enabled = true;
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, 0.0f);
        }
        else
        {
            animator.enabled = false;
        }
        missionText.GetComponent<Text>().enabled = enabled;
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
