using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayEvent : MonoBehaviour
{

    /// <summary>
    /// ウェーブ数
    /// </summary>
    [SerializeField]
    private int waveIndex;

    /// <summary>
    /// 現在のウェーブ
    /// </summary>
    private int currentWave = 1;

    [SerializeField]
    private float startMissionTextTime = 0.5f;

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
    private GameObject player;

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

    private GameObject bossPrefab;
    private GameObject boss;

    private FlickController flickController;

    private Energy energy;




    void Start()
    {
        //プレイヤーの移動を止める
        player = FindObjectOfType<PlayerSmallController>().gameObject;



        textBack = GameObject.Find("TextBack").GetComponent<Image>();
        missionText = GameObject.Find("MissionText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        descriptionText = GameObject.Find("DescriptionText");
        arrow = GameObject.Find("Arrow");

        bossPrefab = Resources.Load<GameObject>("Prefab/Boss");
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
        yield return new WaitForSeconds(startMissionTextTime);
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
        Camera.main.GetComponent<CameraControl>().StartCameraScroll(0, player.transform.position.y, 2);

        yield return new WaitForSeconds(2f);

        StartCoroutine(StartNextWave());
    }



    /// <summary>
    ///次のウェーブの開始
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartNextWave()
    {
        SetStageWallTrigger(false);
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
            boss = Instantiate(bossPrefab, (Vector2)Camera.main.transform.position + (Vector2)bossPrefab.transform.position, Quaternion.identity);

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
    public bool IsBossWave()
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

    public void SetStageWallTrigger(bool isTrigger)
    {
        Collider2D[] walls = GameObject.Find("StageWall").transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].isTrigger = isTrigger;
        }
    }

    /// <summary>
    /// 全てのプレイヤーの動きを変更
    /// </summary>
    public void SetPlayerEnabled(bool enabled)
    {

        player.GetComponent<PlayerSmallController>().IsMove = enabled;

        flickController.enabled = enabled;
        energy.enabled = enabled;

    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameEnd()
    {

        //BGM停止
        Instantiate(Resources.Load<GameObject>("Prefab/WhiteFade")).transform.SetParent(GameObject.Find("Canvas").transform, false);
        AudioManager.Instance.StopBGM();
        //プレイヤーを止める
        SetPlayerEnabled(false);
        //ハンマーを止める
        GameObject.Find("HammerFront").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //ボスの動きを止める
        boss.GetComponent<Boss>().enabled = false;
        boss.GetComponent<LineRenderer>().enabled = false;
        boss.GetComponent<FormBossStageObject>().AllEnemyStop();
       // Camera.main.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y,-10);
        //1秒待機
        yield return new WaitForSeconds(1f);     
        Vector3 bossPos = boss.transform.position;
        FindObjectOfType<CameraControl>().StartCameraScroll(bossPos.x, bossPos.y, 0.5f);
        //カメラズーム開始
        StartCoroutine(FindObjectOfType<CameraControl>().CameraZoom());

        yield return new WaitForSeconds(1f);
        SetShakeCamera(2,2,4);
        //boss.GetComponent<Shake>().ShakeObject();

        yield return new WaitForSeconds(4f);
        //ボス死亡
        boss.GetComponent<Boss>().Dead();
        Destroy(boss);
        boss.GetComponent<FormBossStageObject>().AllEnemyDead();
        SetShakeCamera(1f, 1f, 1);
        yield return new WaitForSeconds(1f);
        Instantiate(Resources.Load<GameObject>("Prefab/Text/ResultUI")).transform.SetParent(GameObject.Find("Canvas").transform);
    }

    private void SetShakeCamera(float x, float y, float time)
    {
        Shake shake = Camera.main.GetComponent<Shake>();
        //カメラ振動の値を設定
        shake.x = x;
        shake.y = y;
        shake.shakeTime = time;
        shake.ShakeObject();
    }
}
