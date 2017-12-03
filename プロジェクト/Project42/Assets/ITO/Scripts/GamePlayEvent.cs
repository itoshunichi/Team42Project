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
    private GameObject formCoreObject;




    void Start()
    {
        //プレイヤーの移動を止める
        players = FindObjectsOfType<PlayerSmallController>();
        SetPlayerIsMoveStop(true);


        textBack = GameObject.Find("TextBack").GetComponent<Image>();
        missionText = GameObject.Find("MissionText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        descriptionText = GameObject.Find("DescriptionText");
        arrow = GameObject.Find("Arrow");

        boss = Resources.Load<GameObject>("Prefab/Boss");
        formCoreObject = Resources.Load<GameObject>("Prefab/FormBossStageObject");
        missionText.text = "エネルギーがなくなる前に\nWAVE" + waveIndex + "のボスを倒せ";

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
        SetPlayerIsMoveStop(false);
    }

    private void SetWaveText()
    {
        waveText.text = "WAVE" + currentWave;
    }

    public IEnumerator NextWave()
    {
        if (IsBossWave()) yield break;
        if (GameObject.FindObjectOfType<FormEnemyObject>().FormObjects.Count != 0) yield break;
        //矢印の描画を無効
        SetArrowEnabled(false);
        //エネミー生成オブジェクトを削除
        Destroy(GameObject.FindObjectOfType<FormEnemyObject>());
        //プレイヤーの動きを止める
        SetPlayerIsMoveStop(true);
        //背景の生成
        Vector3 backGroundPos = new Vector3(0, Camera.main.transform.position.y + Camera.main.GetComponent<CameraControl>().getScreenTopLeft().y * 1.5f, 0);
        Instantiate(Resources.Load<GameObject>("Prefab/BackGround"), backGroundPos, Quaternion.identity);
        //カメラのスクロールを開始
        Camera.main.GetComponent<CameraControl>().StartCameraScroll();



        yield return new WaitForSeconds(2f);

        //最後のウェーブの一個手前
        if (currentWave == waveIndex - 1)
        {
            AddCurrentWave();
            yield return new WaitForSeconds(waveTextTime);
            SetWaveTextEnabled(false);
            //Instantiate(formEnemy[currentWave - 1], (Vector2)Camera.main.transform.position, Quaternion.identity);
            Instantiate(boss, (Vector2)Camera.main.transform.position, Quaternion.identity);

        }
        else
        {
            AddCurrentWave();
            Instantiate(formEnemy[currentWave - 1], (Vector2)Camera.main.transform.position, Quaternion.identity);
            SetWaveTextEnabled(true);

            yield return new WaitForSeconds(waveTextTime);
            SetWaveTextEnabled(false);
        }

        //プレイヤーの動きを再開させる
        SetPlayerIsMoveStop(false);
        GameObject.Find("StageWall").transform.position = Camera.main.transform.position;

    }

    private void AddCurrentWave()
    {
        currentWave++;
        SetWaveTextEnabled(true);
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
    /// 全てのプレイヤーの動きを停止
    /// </summary>
    private void SetPlayerIsMoveStop(bool enabled)
    {
        foreach(var player in players)
        {
            player.IsMoveStop = enabled;
        }
    }
}
