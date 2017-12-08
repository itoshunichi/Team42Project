using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BossMode
{
    NO,
    MOVE,
    STOP,
    DAMEGE,
    SELECT_TARGETCORE,
}

public class Boss : BeDestroyedObject
{
    /// <summary>
    /// 体力
    /// </summary>
    [SerializeField]
    private int hp;

    [SerializeField]
    private float speed;


    /// <summary>
    /// ターゲットのコア
    /// </summary>
    private GameObject formBossStageObject;

   [SerializeField]
    private GameObject formCoreObject;


    /// <summary>
    /// コアを生成するオブジェ
    /// </summary>
    private FormBossStageObject formBossStageObjectScript;

    private GameObject sheild;

    private LineController line;

    /// <summary>
    /// ボスの状態
    /// </summary>
    private BossMode mode;

    [SerializeField]
    private float stopTime;
    private Timer stopModeTimer;

    [SerializeField]
    private float formEnemyTime;
    private Timer formEnemyTimer;


    private void Awake()
    {

    }

    protected override void Start()
    {
        base.Start();
        stopModeTimer = new Timer(stopTime);
        formEnemyTimer = new Timer(formEnemyTime);
        sheild = transform.GetChild(0).gameObject;
        line = GetComponent<LineController>();
        AudioManager.Instance.PlaySE(AUDIO.SE_MATHERSPAWN);
        InstantiateFormCore();
    }

    /// <summary>
    /// コアを生成するオブジェクトの生成
    /// </summary>
    private void InstantiateFormCore()
    {
        GameObject obj = Instantiate(formCoreObject, transform.position, Quaternion.identity);
        formBossStageObjectScript = obj.GetComponent<FormBossStageObject>();
    }
    private void Update()
    {
        ModeMagager();
        Debug.Log(mode);
    }



    public virtual void Dead()
    {
        SceneNavigater.Instance.Change("GameClear");
    }


    /// <summary>
    /// ダメージ
    /// </summary>
    public override void BeginDamage()
    {
        if (!CheckMode(BossMode.STOP)) return;
        //タイマーリセット
        stopModeTimer.Reset();
        //シールドを有効に
        sheild.GetComponent<SpriteRenderer>().enabled = true;
        sheild.GetComponent<Collider2D>().enabled = true;
        //コアの生成
        formBossStageObjectScript.FormCore();
        //HPを減らす
        hp -= 1;
        FindObjectOfType<FormBossStageObject>().EnemyActionReset();
        //待機状態に
        mode = BossMode.NO;
    }




    /// <summary>
    /// ターゲットコアの設定
    /// </summary>
    private void SetTargetCore()
    {
        // yield return new WaitForSeconds(1f);
        formBossStageObjectScript.FormCore();
        formBossStageObject = FindObjectOfType<Core>().gameObject;
        //線を伸ばすのを開始する
        line.StartLineExtend(transform.position, formBossStageObject.transform.position);
        //コアを探す状態にする
        mode = BossMode.SELECT_TARGETCORE;

    }

    /// <summary>
    /// 状態管理
    /// </summary>
    private void ModeMagager()
    {
        //待機状態
        if (mode == BossMode.NO)
        {
            line.SetEnabled(false);
            SetTargetCore();
        }
        //移動状態
        if (CheckMode(BossMode.MOVE))
        {
            //移動
            Move();
            //敵の生成
            FormEnemy();
            //移動の中断
            CoreNullMode();
        }
        //コアに線を引いてる状態
        if (CheckMode(BossMode.SELECT_TARGETCORE))
        {
            CoreNullMode();

            //引き終わったら移動状態にする
            if (line.IsEndExtend) mode = BossMode.MOVE;
        }
        if (mode == BossMode.STOP)
        {
            StopMode();
        }
    }
    /// <summary>
    ///止まっている状態の処理
    /// </summary>
    private void StopMode()
    {
        stopModeTimer.UpdateTimer();

        sheild.GetComponent<SpriteRenderer>().enabled = false;
        sheild.GetComponent<Collider2D>().enabled = false;

        if (stopModeTimer.IsEnd)
        {
            stopModeTimer.Reset();
            sheild.GetComponent<SpriteRenderer>().enabled = true;
            sheild.GetComponent<Collider2D>().enabled = true;
            mode = BossMode.NO;
        }


    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        if (formBossStageObject == null) return;
        Vector2 targetPos = formBossStageObject.transform.position;
        //ラジアン
        float rad = Mathf.Atan2(targetPos.y - transform.position.y,
            targetPos.x - transform.position.x);

        Vector2 pos = transform.position;
        pos.x += speed * Mathf.Cos(rad) * Time.deltaTime;
        pos.y += speed * Mathf.Sin(rad) * Time.deltaTime;
        transform.position = pos;

    }

    /// <summary>
    /// エネミーの生成
    /// </summary>
    private void FormEnemy()
    {
        formEnemyTimer.UpdateTimer();
        if (formEnemyTimer.IsEnd)
        {
            //エネミー生成
            formBossStageObjectScript.FormRandomEnemy();
            formEnemyTimer.Reset();
        }
    }

    /// <summary>
    /// 行動中にコアが破壊されたときの処理
    /// </summary>
    private void CoreNullMode()
    {
        //コアがなくなったら
        if (formBossStageObject == null)
        {
            //線を伸ばすのを開始する
            line.StartLineExtend(GetComponent<LineRenderer>().GetPosition(1), transform.position);
            formEnemyTimer.Reset();
            //ウェーブの生成
            InstantiateWave();
            //停止状態に
            mode = BossMode.STOP;
        }
    }

    private void InstantiateWave()
    {
        GameObject obj = Resources.Load<GameObject>("Prefab/Wave/AttackWave");
        Instantiate(obj, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 現在の状態のチェック
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public bool CheckMode(BossMode mode)
    {
        return this.mode == mode;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //移動状態かつ当たったオブジェクトの名前にCoreが含まれていたら
        if (CheckMode(BossMode.MOVE) && collision.gameObject.name.Contains("Core"))
        {
            AbsorptionCore(collision.gameObject);
            formEnemyTimer.Reset();
        }

    }

    /// <summary>
    /// コアの吸収
    /// </summary>
    private void AbsorptionCore(GameObject obj)
    {
        //新しいコアの生成
        //formCore.FormCore();
        //コアを削除
        formBossStageObjectScript.BreakCore(obj);
        //待機状態
        mode = BossMode.NO;
        //ターゲットコアの設定
        SetTargetCore();

    }


}
