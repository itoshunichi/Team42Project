
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BossMode
{
    // NO,
    WAIT,
    MOVE,
    BREAK_EMEMY,
    DAMEGE,
    SELECT_TARGETENEMY,
    NO,
}

public class Boss : BeDestroyedObject
{
    /// <summary>
    /// 体力
    /// </summary>
    [SerializeField]
    private int hp;

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// ラストステージで生成するオブジェ
    /// </summary>
   // private FormBossStageObject formBossStageObject;

    //シールド
    private GameObject shield;

    //線
    private LineController line;

    /// <summary>
    /// ボスの状態
    /// </summary>
    private BossMode mode;

    [SerializeField]
    private float waitTime;
    private Timer waitTimer;

    [SerializeField]
    private float formEnemyTime;
    private Timer formEnemyTimer;

    //目標のエネミー
    private GameObject targetEnemy;

    /// <summary>
    /// 登場の演出が終わったかどうか
    /// </summary>
    private bool isAppearedInDirectorEnd;
    public bool IsAppearedInDirectorEnd
    {
        get { return isAppearedInDirectorEnd; }
    }

    private bool isShield = true;




    private void Awake()
    {
        formEnemyTimer = new Timer(formEnemyTime);
        waitTimer = new Timer(waitTime);
        line = GetComponent<LineController>();
        //formBossStageObject.GetComponent<FormBossStageObject>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AppearedInDirector());
        //stopModeTimer = new Timer(stopTime);


        shield = transform.GetChild(0).gameObject;

        AudioManager.Instance.PlaySE(AUDIO.SE_MATHERSPAWN);
    }


    private void Update()
    {
        SetShield();
        ModeMagager();
        Debug.Log(mode);
    }

    /// <summary>
    /// 登場演出
    /// </summary>
    private IEnumerator AppearedInDirector()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<FormBossStageObject>().FormEnemyGroup();
        shield.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(1f);
        isAppearedInDirectorEnd = true;
        FindObjectOfType<GamePlayEvent>().SetPlayerEnabled(true);
    }





    public virtual void Dead()
    {
        Instantiate(breakEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        //Destroy(gameObject);
    }


    /// <summary>
    /// ダメージ
    /// </summary>
    public override void BeginDamage()
    {
        if (isShield) return;
        //HPを減らす
        hp -= 1;
        //待機状態に
        mode = BossMode.WAIT;
        if (hp == 0)
        {
           StartCoroutine(FindObjectOfType<GamePlayEvent>().GameEnd());

        }
    }


    /// <summary>
    /// 状態管理
    /// </summary>
    private void ModeMagager()
    {
        //登場の演出が終わってなかったら処理しない
        if (!isAppearedInDirectorEnd) return;
        //待機状態
        if (mode == BossMode.WAIT)
        {
            waitTimer.UpdateTimer();
            line.SetEnabled(false);
            if (waitTimer.IsEnd)
            {
                SetTargetEnemy();
                if (IsExistenceTargetEnemy())
                    waitTimer.Reset();
            }


        }
        //移動状態
        if (CheckMode(BossMode.MOVE))
        {
            //移動
            Move();
            //敵の生成
            FormEnemy();
            //移動の中断
            TargetEnemyBreakMode();
        }
        if (CheckMode(BossMode.BREAK_EMEMY))
        {
            //線が引き終わったら待機状態に
            if (!line.IsExtend)
            {
                mode = BossMode.WAIT;
            }
        }
        //コアに線を引いてる状態
        if (CheckMode(BossMode.SELECT_TARGETENEMY))
        {
            TargetEnemyBreakMode();

            //引き終わったら移動状態にする
            if (!line.IsExtend) mode = BossMode.MOVE;
        }
    }


    /// <summary>
    /// シールドの設定
    /// </summary>
    private void SetShield()
    {
        //シールドエネミーがいなかったら無効
        if (GetComponent<FormBossStageObject>().ShieldEnemys.Count == 0)
        {
            isShield = false;
            shield.GetComponent<ParticleSystem>().Stop();
            shield.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            isShield = true;
            shield.GetComponent<ParticleSystem>().Play();
            shield.GetComponent<Collider2D>().enabled = true;
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        if (!IsExistenceTargetEnemy()) return;
        Vector2 targetPos = targetEnemy.transform.position;
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
        //一定間隔でエネミーを生成
        formEnemyTimer.UpdateTimer();
        if (formEnemyTimer.IsEnd)
        {
            //エネミー生成
            GetComponent<FormBossStageObject>().FormRandomEnemy();
            formEnemyTimer.Reset();
        }
    }

    /// <summary>
    /// 行動中にターゲットにしていエネミーが破壊されたときの処理
    /// </summary>
    private void TargetEnemyBreakMode()
    {
        //エネミーがなくなったら
        if (!IsExistenceTargetEnemy())
        {
            mode = BossMode.BREAK_EMEMY;
            //線を伸ばすのを開始する
            line.StartLineExtend(GetComponent<LineRenderer>().GetPosition(1), transform.position);
            formEnemyTimer.Reset();

        }
    }

    /// <summary>
    /// ウェーブの生成
    /// </summary>
    public void InstantiateWave()
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

    public void ChangeMode(BossMode mode)
    {
        this.mode = mode;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isShield)
        {
            TargetEnemy_ChangeSheild(collision);
        }
    }


    /// <summary>
    /// 目標のエネミーをシールドエネミーに変更
    /// </summary>
    /// <param name="colision"></param>
    public void TargetEnemy_ChangeSheild(Collision2D collision)
    {
        //移動状態かつ当たったオブジェクトがターゲットだったら
        if (CheckMode(BossMode.MOVE) && collision.gameObject == targetEnemy)
        {
            //エネミーのモードをシールドに
            targetEnemy.GetComponent<Enemy>().ChangeMode(EnemyMode.SHIELD);
            //エネミーリストから削除
            GetComponent<FormBossStageObject>().Enemys.Remove(targetEnemy);
            //シールドエネミーのリストに追加
            GetComponent<FormBossStageObject>().ShieldEnemys.Add(targetEnemy);
            //移動再開
            targetEnemy.GetComponent<Enemy>().IsMove = true;

            formEnemyTimer.Reset();
            //待機状態に
            mode = BossMode.WAIT;
        }
    }

    /// <summary>
    /// ターゲットのエネミーを設定
    /// </summary>
    public void SetTargetEnemy()
    {
        List<GameObject> enemys = GetComponent<FormBossStageObject>().Enemys;
        if (enemys.Count == 0) return;
        int index = Random.Range(0, enemys.Count);
        targetEnemy = enemys[index];

        //エネミーの動きを無効
        targetEnemy.GetComponent<Enemy>().Stop();
        //線を伸ばすのを開始する
        line.StartLineExtend(transform.position, targetEnemy.transform.position);
        //エネミーを探す状態にする
        mode = BossMode.SELECT_TARGETENEMY;

    }

    /// <summary>
    ///目標のエネミーが存在するかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsExistenceTargetEnemy()
    {
        return targetEnemy;
    }

   

}
