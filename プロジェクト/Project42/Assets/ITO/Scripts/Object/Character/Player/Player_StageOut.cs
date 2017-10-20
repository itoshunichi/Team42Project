using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが画面外にいったときの処理
public class Player_StageOut : MonoBehaviour {


    [SerializeField]
    private bool isMove = true;
    private bool isStageOut;
    public bool IsStageOut
    {
        get { return IsStageOut; }
    }



	void Start () {
    }
	
	void Update () {

        if (!isStageOut&&isMove)
        {

            GetComponent<Rigidbody2D>().MovePosition(transform.position+Direction() * 3f * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Debug.Log("当たった");
            StartCoroutine(StageOut());
        }
    }


    /// <summary>
    /// ステージ外に行ったときの処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator StageOut()
    {
        isStageOut = true;
        //自分の向いてる方向に力を加える
        GetComponent<Rigidbody2D>().AddForce(Direction() * 50f);

        //0.5秒待つ
        yield return new WaitForSeconds(0.5f);
        //加えられてる力をリセット
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
       // GetComponent<Rigidbody2D>().angularVelocity = 0;
        //反転
        transform.eulerAngles += new Vector3(0, 0, 180);
        yield return null;
        //向いてる方向に力を加える
        GetComponent<Rigidbody2D>().AddForce(Direction() * 200f);

        yield return new WaitForSeconds(0.2f);

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        isStageOut = false;
        isMove = false;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z % 360);

       yield break;
    }

    private void FixedUpdate()
    {
       
    }

    /// <summary>
    /// 自身の向きを取得
    /// </summary>
    /// <returns></returns>
    private Vector3 Direction()
    {
        //自身の向きベクトル取得
        float angleDir = transform.eulerAngles.z * (Mathf.PI / 180f);
        Vector3 dir = new Vector3(Mathf.Cos(angleDir), Mathf.Sin(angleDir), 0.0f);
        return dir;
    }
}
