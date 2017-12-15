using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallController : Player
{
    float accelerator = 0;                  //加速
    public float acceleratorMax;            //加速の最大値
    private bool isMove;                    //止まっているかどうか
    public Energy energy;                   //エネルギー
    private int enemyHitCount = 0;          //エネミーに当たった回数
    public GameObject[] point;              //移動範囲制御
    public GameObject[] effect;             //エフェクト
    private Vector2 spriteSize;             //画像サイズ
    public GameObject gameOverUI;           //ゲームオーバーUI
    public int EnemyHitCount
    { get { return enemyHitCount; } }
    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    /// <summary>
    /// アクションエリアにいるかどうか
    /// </summary>
    private bool isActionEria;
    public bool IsActionEria
    {
        get { return isActionEria; }
    }

    // Use this for initialization
    void Start()
    {
        flickController = flick.GetComponent<FlickController>();
        spriteSize = GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 1.0f) return;
        if (energy.GetEnergy() <= 0)
        {
            if (FindObjectOfType<GamePlayEvent>().IsBossWave()) FindObjectOfType<FormBossStageObject>().AllEnemyStop();
            Instantiate(gameOverUI);
            Instantiate(effect[2], transform.position, effect[2].transform.rotation);
            GameObject.Find("Main Camera").GetComponent<Shake>().ShakeObject();
            Destroy(gameObject);
            Destroy(GameObject.Find("HammerFront"));
        }
        else if (energy.GetEnergy() > 0)
        {
            Move();//移動処理
            Accelerator();//加速処理
            GetComponent<Animator>().SetBool("IsHit", isHit);
        }
    }

    private void Move()
    {
        NotMoveCount();
        if (!isHit) RotationMove();
    }

    //向きに対して移動
    private void RotationMove()
    {
        if (GetComponent<Player_StageOut>().IsStageOut()) return;
        if (transform.rotation.z != flick.transform.rotation.z) GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);

        dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        if (!isMove) return;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + dir.x * (speed + accelerator), point[0].transform.position.x + spriteSize.x, point[1].transform.position.x - spriteSize.x),
            Mathf.Clamp(transform.position.y + dir.y * (speed + accelerator), point[0].transform.position.y, point[1].transform.position.y));
    }
    //加速値を下げる
    private void Accelerator()
    {
        if (accelerator > 0)
        {
            accelerator -= acceleratorMax / 20;
            Instantiate(effect[0], transform.position, effect[1].transform.rotation);
        }
        if (accelerator <= 0 && speed > 0)
        {
            speed -= speedMax / 60;
        }
    }
    //加速数値セット
    public void SetAccelerator()
    {
        accelerator = acceleratorMax;
        speed = speedMax;
    }

    public Vector2 GetDirection()
    {
        return dir;
    }

    public void SetRotationPlayer(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public bool GetHit()
    {
        return isHit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("TopCollider"))
        {
            StartCoroutine(FindObjectOfType<GamePlayEvent>().WaveFinish());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && !isHit)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_DAMAGE);
            GameObject.Find("Main Camera").GetComponent<Shake>().ShakeObject();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            isHit = true;
            speed = 0;
            Instantiate(effect[1], transform.position, effect[1].transform.rotation);
            Vector2 dir = transform.position - col.gameObject.transform.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(dir * collisionPower);
            energy.MinusEnergy();
        }
    }
}
