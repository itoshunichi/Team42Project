using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayEvent : MonoBehaviour {

    /// <summary>
    /// ウェーブ数
    /// </summary>
    [SerializeField]
    private int waveIndex;

    /// <summary>
    /// 現在のウェーブ
    /// </summary>
    private int currentWave = 1;

    /// <summary>
    /// ミッションテキストの表示時間
    /// </summary>
    [SerializeField]
    private float missionTextTime;
    /// <summary>
    /// テキストの表示時間
    /// </summary>
    [SerializeField]
    private float waveTextTime;

    //プレイヤー
    private PlayerSmallController[] players;

    //衛星のLerp移動関係の変数
    private float satelliteMoveStartTime;

    //テキストの背景
    private Image textBack;
    //ミッションのテキスト
    private Text missionText;
    //ウェーブのテキスト
    private Text waveText;
    //操作説明のテキスト
    private GameObject descriptionText;
    //矢印の画像
    private GameObject arrow;

    [SerializeField]
    private List<GameObject> formEnemy;

    private GameObject boss;

    private FlickController flickController;

    private Energy energy;




    void Start()
    {
        //プレイヤーの移動を止める
        players = FindObjectsOfType<PlayerSmallController>();
       


        textBack = GameObject.Find("TextBack").GetComponent<Image>();
        missionText = GameObject.Find("MissionText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        descriptionText = GameObject.Find("DescriptionText");
        arrow = GameObject.Find("Arrow");

        boss = Resources.Load<GameObject>("Prefab/Boss");
        //formCoreObject = Resources.Load<GameObject>("Prefab/FormBossStageObject");
        missionText.text = "エネルギーがなくなる前に\nWAVE" + waveIndex + "のボスを倒せ";
        flickController = FindObjectOfType<FlickController>();
        energy = FindObjectOfType<Energy>();
        SetPlayerEnabled(false);
        StartCoroutine(GameStart());

    }

    void Update()
    {
        SetWaveText();

    }

    /// <summary>
    /// イベントの進行
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1);
        //テキストを表示
        textBack.enabled = true;
        missionText.enabled = true;

        yield return new WaitForSeconds(missionTextTime);

        missionText.enabled = false;
        waveText.enabled = true;

        yield return new WaitForSeconds(waveTextTime);
        SetWaveTextEnabled(false);
        descriptionText.GetComponent<Image>().enabled = true;
        descriptionText.GetComponentInChildren<Text>().enabled = true;
        Instantiate(formEnemy[0]);

        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlayBGM(AUDIO.BGM_GAMEPLAY);
        GameObject.Find("FlickController").GetComponent<FlickController>().enabled = true;
        SetPlayerEnabled(true);
    }

    private void SetWaveText()
    {
        waveText.text = "WAVE" + currentWave;
    }

    /// <summary>
    /// 次のウェーブへ
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaveFinish()
    {
        //既に最終ウェーブだったら処理しない
        if (IsBossWave()) yield break;
        if (FindObjectOfType<FormEnemyObject>().FormObjects.Count != 0) yield break;
        //矢印の描画を無効
        SetArrowEnabled(false);
        //エネミー生成オブジェクトを削除
        Destroy(GameObject.FindObjectOfType<FormEnemyObject>());
        //プレイヤーの動きを止める
        SetPlayerEnabled(false);
        //背景の生成
        Vector3 backGroundPos = new Vector3(0, Camera.main.transform.position.y + Camera.main.GetComponent<CameraControl>().getScreenTopLeft().y * 1.5f, 0);
        Instantiate(Resources.Load<GameObject>("Prefab/BackGround"), backGroundPos, Quaternion.identity);
        //カメラのスクロールを開始
        Camera.main.GetComponent<CameraControl>().StartCameraScroll();

        yield return new WaitForSeconds(2f);

        StartCoroutine(StartNextWave());
       

        
       
        

    }



    /// <summary>
    ///次のウェーブの開始
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartNextWave()
    {
        //現在のウェーブを進める
        currentWave++;
        //テキストを描画
        SetWaveTextEnabled(true);
        //待機
        yield return new WaitForSeconds(waveTextTime);
        //テキスト無効
        SetWaveTextEnabled(false);
        //敵の生成
        FormEnemy();
        //ステージの位置の調整
        GameObject.Find("StageWall").transform.position = Camera.main.transform.position;
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    private void FormEnemy()
    {
        //最後のウェーブ
        if (IsBossWave())
        {
            //ボス生成
            Instantiate(boss, (Vector2)Camera.main.transform.position+(Vector2)boss.transform.position, Quaternion.identity);

        }
        //その他
        else
        {
            //エネミー生成
            Instantiate(formEnemy[currentWave - 1], (Vector2)Camera.main.transform.position, Quaternion.identity);
            //プレイヤーの動きを再開させる
            SetPlayerEnabled(true);
            
        }
    }

    private void SetWaveTextEnabled(bool enabled)
    {
        textBack.enabled = enabled;
        waveText.enabled = enabled;
    }

    /// <summary>
    /// ボスのウェーブにいる状態
    /// </summary>
    /// <returns></returns>
    private bool IsBossWave()
    {
        return currentWave == waveIndex;
    }

    /// <summary>
    /// 矢印の描画
    /// </summary>
    public void SetArrowEnabled(bool enabled)
    {
        arrow.GetComponent<Image>().enabled = enabled;
        arrow.GetComponent<Blinker>().enabled = enabled;

        if (currentWave == 1)
        {
            Destroy(descriptionText);
        }

    }

    /// <summary>
    /// 全てのプレイヤーの動きを変更
    /// </summary>
    public void SetPlayerEnabled(bool enabled)
    {
        foreach(var player in players)
        {
            player.IsMove = enabled;
        }
        flickController.enabled = enabled;
        energy.enabled = enabled;
       
    }
}
