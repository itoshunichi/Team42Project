using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CanvasGroup))]

public class CanvasFader : MonoBehaviour {

    //フェード用のキャンバス
    private CanvasGroup canvasGroupEntity;
    private CanvasGroup canvasGroup
    {
        get
        {
            if(canvasGroupEntity == null)
            {
                canvasGroupEntity = GetComponent<CanvasGroup>();
                if(canvasGroupEntity == null)
                {
                    canvasGroupEntity = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroupEntity;
        }
    }

    public float Alpha
    {
        get
        {
            return canvasGroup.alpha;
        }
        set
        {
            canvasGroup.alpha = value;
        }
    }

    //フェードの状態
    private enum FadeState {
        None,FadeIn,FadeOut
    }

    private FadeState fadeState = FadeState.None;

    //フェード時間
    [SerializeField]
    private float duration;
    public float Duration { get { return duration; } }

    //タイムスケールを無視するか
    [SerializeField]
    private bool ignoreTimeScale = true;

    //フェード終了後のコールバック
    private Action onFinished = null;

    //フェードしているかどうか
    public bool IsFading
    {
        get { return fadeState != FadeState.None; }
    }

	// Use this for initialization
	void Start () {
        
		
	}
	
	void Update () {

        if (!IsFading) return;

        float fadeSpeed = 1f / duration;
        if(ignoreTimeScale)
        {
            fadeSpeed *= Time.unscaledDeltaTime;
        }
        else
        {
            fadeSpeed *= Time.deltaTime;
        }

        Alpha += fadeSpeed * (fadeState == FadeState.FadeIn ? 1f : -1f);

        //フェード終了判定
        if (Alpha > 0 && Alpha < 1) return;

        fadeState = FadeState.None;
        this.enabled = false;

        if (onFinished != null) onFinished();
        
	}

    /// <summary>
    /// フェード開始
    /// </summary>
    public void Play(bool isFadeOut,float duration,bool ignoreTimeScake = true,Action onFinished = null)
    {
        this.enabled = true;

        this.ignoreTimeScale = ignoreTimeScake;
        this.onFinished = onFinished;

        Alpha = isFadeOut ? 1 : 0;
        fadeState = isFadeOut ? FadeState.FadeOut : FadeState.FadeIn;

        this.duration = duration;
    }
    
    /// <summary>
    /// 対象のオブジェクトのフェードを開始する
    /// </summary>
    /// <returns></returns>
    public static void Begin(GameObject target,bool isFadeOut,float duration,bool ignoreTimeScale = true,Action onFinished = null)
    {
        CanvasFader canvasFader = target.AddComponent<CanvasFader>();
        if(canvasFader == null)
        {
            canvasFader = target.AddComponent<CanvasFader>();
        }
        canvasFader.enabled = true;

        canvasFader.Play(isFadeOut, duration);
    }

    /// <summary>
    /// フェード停止
    /// </summary>
    public void Stop()
    {
        fadeState = FadeState.None;
        this.enabled = false;

    }
}
