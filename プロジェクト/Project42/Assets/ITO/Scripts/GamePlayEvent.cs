using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayEvent : MonoBehaviour
{

    [SerializeField, Tooltip("ゲームプレイ中にでるテキスト")]
    private GamePlayText gamePlayText;

    [SerializeField, Tooltip("ウェーブ数")]
    private int waveIndex;

    [SerializeField, Tooltip("背景")]
    private GameObject backGround;

    [SerializeField, Tooltip("ボスのプレファブ")]
    private GameObject bossPrefab;
    private GameObject boss;

    [SerializeField]
    private GameObject resutUI;

    [SerializeField]
    private GameObject finger;


    //private float StageLength = 48

    /// <summary>
    /// 現在のウェーブ
    /// </summary>
    private int currentWave = 1;

    //プレイヤー
    private GameObject player;



    //ステージの半径のY座標
    private float stageRadiusY;

    //衛星のLerp移動関係の変数
    private float satelliteMoveStartTime;



    //矢印の画像
    private GameObject arrow;

    [SerializeField]
    private List<GameObject> formEnemy;

    private bool isGameEnd;
    public bool IsGameEnd
    {
        get { return isGameEnd; }
    }



    void Start()
    {
        SetBackGround();
        //プレイヤーの移動を止める
        player = FindObjectOfType<PlayerSmallController>().gameObject;

        arrow = GameObject.Find("Arrow");
        SetPlayerEnabled(false);
        stageRadiusY = GameObject.Find("TopCollider").transform.position.y;
        StartCoroutine(GameStart());

    }

    void Update()
    {
        //WaveEndPlayerAutoMove();
        //SetWaveText();

    }

    /// <summary>
    /// イベントの進行
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {

        //BGM再生
        AudioManager.Instance.PlayBGM(AUDIO.BGM_GAMEPLAY);

        //ミッションのテキスト表示開始
        StartCoroutine(gamePlayText.StartMissionTextDisplay(waveIndex));

        //gamePlayText.SetMissionText(waveIndex);

        //テキストの表示合計時間待機
        yield return new WaitForSeconds(gamePlayText.TotalStartTextDiplayTime());

        Instantiate(finger);

        //エネミーを生成
        Instantiate(formEnemy[0]);

        //1秒待機
        // yield return new WaitForSeconds(1f);



        //プレイヤーの動きを有効に
        SetPlayerEnabled(true);
    }

    public IEnumerator StartWaveEndPlayerAutoMove()
    {
       
        SetPlayerEnabled(false);
        player.transform.eulerAngles = new Vector3(0, 0, 0);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            //Debug.Log("auto");
            Vector2 pos = player.transform.position;
            pos += new Vector2(0.0f, 0.2f);
            player.GetComponent<Rigidbody2D>().MovePosition(pos);
        }

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
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //矢印の描画を無効
        SetArrowEnabled(false);
        //プレイヤーの動きを止める
        SetPlayerEnabled(false);
        GameObject formEnemy = FindObjectOfType<FormEnemyObject>().gameObject;
        //エネミー生成オブジェクトを削除
        Destroy(formEnemy);
        //カメラのスクロールを開始
        Camera.main.GetComponent<CameraControl>().StartCameraScroll(0, player.transform.position.y, 2);

        yield return new WaitForSeconds(2f);

        StartCoroutine(StartNextWave());
    }

    private void SetBackGround()
    {
        for (int i = 1; i < waveIndex; i++)
        {
            float backGroundRadius = 42.2f;
            //背景の生成
            Vector3 backGroundPos = new Vector3(0, i * backGroundRadius, 0);
            Instantiate(backGround, backGroundPos, Quaternion.identity);
        }
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

        if (IsBossWave()) BossWaveStart();
        gamePlayText.SetCurrentWaveText(currentWave);
        //テキスト表示開始
        StartCoroutine(gamePlayText.StartCurrnetWaveTextDisplay());

        yield return new WaitForSeconds(gamePlayText.DisplayTime);

        //敵の生成
        FormEnemy();
        //ステージの位置の調整
        GameObject.Find("StageWall").transform.position = Camera.main.transform.position;
    }

    private void BossWaveStart()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySE(AUDIO.SE_BOSSWAVESTART);
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
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerSmallController>().IsMove = enabled;

        //フリック有効
        FindObjectOfType<FlickController>().enabled = enabled;
        FindObjectOfType<Energy>().enabled = enabled;

    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameEnd()
    {
        isGameEnd = true;
        //BGM停止
        Instantiate(Resources.Load<GameObject>("Prefab/WhiteFade")).transform.SetParent(GameObject.Find("Canvas").transform, false);
        AudioManager.Instance.StopBGM();
        //プレイヤーを止める
        SetPlayerEnabled(false);

        //ハンマーを止める
        GameObject.Find("HammerFront").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        boss.GetComponent<FormBossStageObject>().AllEnemyStop();
        //ボスの動きを止める
        boss.GetComponent<Boss>().enabled = false;
        boss.GetComponent<LineRenderer>().enabled = false;

        //1秒待機
        yield return new WaitForSeconds(1f);
        Vector3 bossPos = boss.transform.position;
        FindObjectOfType<CameraControl>().StartCameraScroll(bossPos.x, bossPos.y, 0.5f);
        //カメラズーム開始
        StartCoroutine(FindObjectOfType<CameraControl>().CameraZoom());

        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlayBGM(AUDIO.BGM_BOSSSHAKE);
        yield return null;
        SetShakeCamera(2, 2, 4);

        yield return new WaitForSeconds(4f);
        AudioManager.Instance.StopBGM();
        //ボス死亡
        boss.GetComponent<Boss>().Dead();
        Destroy(boss);
        boss.GetComponent<FormBossStageObject>().AllEnemyDead();
        SetShakeCamera(1f, 1f, 1);
        //AudioManager.Instance.se
        AudioManager.Instance.PlaySE(AUDIO.SE_BOSSEXPLOSION);
        StartCoroutine(DisplayResulltUI());
    }

    private IEnumerator DisplayResulltUI()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(resutUI).transform.SetParent(GameObject.Find("Canvas").transform, false);
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
