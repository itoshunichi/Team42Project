using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneNavigater : SingletonMonoBehaviour<SceneNavigater> {

    //フェード中か否か
    public bool IsFading
    {
        get { return fader.IsFading || fader.Alpha != 0; }
    }

    //1個前と現在、次のシーン名
    private string beforeSceneName = "";
    public string BeforeSceneName
    {
        get { return beforeSceneName; }
    }

    private string currentSceneName = "";
    public string CurrentSceneName
    {
        get { return currentSceneName; }
    }

    private string nextSceneName = "";
    public string NextSceneName
    {
        get { return nextSceneName; }
    }

    //フェード後のイベント
    public event Action FadeOutFinished = delegate { };
    public event Action FadeInFinished = delegate { };

    //フェード用クラス
    [SerializeField]
    private CanvasFader fader = null;

    //フェード時間
    // public const float FADE_TIME = 1f;
    [SerializeField]
    private float fadeTime;

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void Init()
    {
        base.Init();

        //実機上やエディタを実行している時にはAddした場合はResetが実行されないので、Initから実行
        if(fader == null)
        {
            Reset();
        }

        

        //最初のシーン名設定
        currentSceneName = SceneManager.GetSceneAt(0).name;

        //永続化し、フェード用のキャンバスを非表示に
        DontDestroyOnLoad(gameObject);
        fader.gameObject.SetActive(false);
    }

    /// <summary>
    /// コンポーネント追加時に自動で実行される(実機上やエディタを実行しているときには動作しない)
    /// </summary>
    private void Reset()
    {
        //オブジェクトの名前を設定
        gameObject.name = "SceneNavigator";

        //フェード用のキャンバス作成
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        fadeCanvas.transform.SetParent(transform);
        fadeCanvas.SetActive(false);

        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        

        fadeCanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        fadeCanvas.AddComponent<GraphicRaycaster>();
        fader = fadeCanvas.AddComponent<CanvasFader>();
        fader.Alpha = 0;
    }

    public void Change(string sceneName)
    {
        if (IsFading)
        {
            Debug.LogError("フェード中です");
            return;
        }
        
      

        //次のシーン名とフェード時間を設定
        nextSceneName = sceneName;
        //this.fadeTime = fadeTime;
        //フェードアウト
        fader.gameObject.SetActive(true);
        
        fader.Play(isFadeOut: false, duration: fadeTime, onFinished:OnFadeOutFinish);
    }

    /// <summary>
    /// フェードアウト終了
    /// </summary>
    private void OnFadeOutFinish()
    {
        FadeOutFinished();

        //シーン読み込み、変更
        SceneManager.LoadScene(nextSceneName);

        //シーン名更新
        beforeSceneName = currentSceneName;
        currentSceneName = nextSceneName;

        //フェードイン開始
        fader.gameObject.SetActive(true);
        fader.Alpha = 1;
        fader.Play(isFadeOut: true, duration: this.fadeTime, onFinished: OnFadeInFinish);
    }

    /// <summary>
    /// フェードイン終了
    /// </summary>
    private void OnFadeInFinish()
    {
        fader.gameObject.SetActive(false);
        FadeInFinished();
    }

    private void Update()
    {
        Debug.Log(IsFading);
    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        

        //イベントにメソッド登録
        SceneNavigater.Instance.FadeOutFinished += OnFinishedFadeOut;
        SceneNavigater.Instance.FadeInFinished += OnFinishedFadeIn;
    }

    /// <summary>
    /// フェードアウトが完了したときに実行
    /// </summary>
    private void OnFinishedFadeOut()
    {
        Debug.Log(SceneNavigater.Instance.CurrentSceneName + "から" + SceneNavigater.Instance.NextSceneName + "へ");
    }

    /// <summary>
    /// フェードインが完了した後に実行される
    /// </summary>
    private void OnFinishedFadeIn()
    {
        Debug.Log(SceneNavigater.Instance.CurrentSceneName + " に移行完了！前のシーンは" + SceneNavigater.Instance.BeforeSceneName);
    }
}
