using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallController : Player
{
    float accelerator = 0;                  //加速
    public float acceleratorMax;            //加速の最大値
    private bool isMove;                //止まっているかどうか
    public Energy energy;                   //エネルギー
    private int enemyHitCount = 0;          //エネミーに当たった回数
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
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();//移動処理
        Accelerator();//加速処理
    }

    private void Move()
    {
        NotMoveCount();
        RotationMove();
    }

    //向きに対して移動
    private void RotationMove()
    {
        if (GetComponent<Player_StageOut>().IsStageOut()) return;
        if (transform.rotation.z != flick.transform.rotation.z) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.rotation = flick.transform.rotation;
        //自身の向きベクトル取得
        //自身の角度をラジアンで取得
        float angleDirection = transform.eulerAngles.z * (Mathf.PI / 180.0f);

        dir = new Vector3(-Mathf.Sin(angleDirection), Mathf.Cos(angleDirection), 0.0f);
        if (!isMove) return;

        transform.position += dir * (speed + accelerator);
    }
    //加速値を下げる
    private void Accelerator()
    {
        if (accelerator > 0)
        {
            accelerator -= acceleratorMax / 20;
        }
        Debug.Log(speed);
        if (accelerator <= 0 && speed > 0)
        {
            Debug.Log("SPEEDDOWN");
            speed -= speedMax / 60;
        }
    }
    //加速数値セット
    public void SetAccelerator()
    {
        Debug.Log("SET");
        accelerator = acceleratorMax;
        speed = speedMax;
    }

    public Vector2 GetDirection()
    {
        return dir;
    }

    public bool GetHit()
    {
        return isHit;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ActionEria")
        {
            isActionEria = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ActionEria")
        {
            isActionEria = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("TopCollider"))
        {
            //Debug.Log("Top");
            StartCoroutine(FindObjectOfType<GamePlayEvent>().WaveFinish());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "BeDestroyedObject" && !isHit)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_DAMAGE);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            isHit = true;
            speed = 0;
            Vector2 dir = transform.position - col.gameObject.transform.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(dir * collisionPower);
            energy.MinusEnergy();
        }
    }
}
