using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayEvent : MonoBehaviour
{

    [SerializeField,Tooltip("ゲームプレイ中にでるテキスト")]
    private GamePlayText gamePlayText;

    [SerializeField,Tooltip("ウェーブ数")]
    private int waveIndex;

    [SerializeField,Tooltip("背景")]
    private GameObject backGround;

    [SerializeField,Tooltip("ボスのプレファブ")]
    private GameObject bossPrefab;
    private GameObject boss;



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
        //プレイヤーの移動を止める
        player = FindObjectOfType<PlayerSmallController>().gameObject;

        arrow = GameObject.Find("Arrow");
        SetPlayerEnabled(false);
        stageRadiusY = GameObject.Find("TopCollider").transform.position.y;
        StartCoroutine(GameStart());

    }

    void Update()
    {
        //SetWaveText();

    }

    /// <summary>
    /// イベントの進行
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        gamePlayText.SetMissionText(waveIndex);
        //ミッションのテキスト表示開始
        StartCoroutine(gamePlayText.StartMissionTextDisplay());

        //テキストの表示合計時間待機
        yield return new WaitForSeconds(gamePlayText.TotalStartTextDiplayTime());
        
        //エネミーを生成
        Instantiate(formEnemy[0]);

        //1秒待機
        yield return new WaitForSeconds(1f);

        //BGM再生
        AudioManager.Instance.PlayBGM(AUDIO.BGM_GAMEPLAY);

       
        //プレイヤーの動きを有効に
        SetPlayerEnabled(true);
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
        Vector3 backGroundPos = new Vector3(0, Camera.main.transform.position.y + stageRadiusY * 2, 0);
        Instantiate(backGround, backGroundPos, Quaternion.identity);
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

        gamePlayText.SetCurrentWaveText(currentWave);
        //テキスト表示開始
        StartCoroutine(gamePlayText.StartCurrnetWaveTextDisplay());

        yield return new WaitForSeconds(gamePlayText.DisplayTime);

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

        player.GetComponent<PlayerSmallController>().IsMove = enabled;

        //フリック有効
        FindObjectOfType<FlickController>().enabled = true;
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
