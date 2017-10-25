using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが画面外にいったときの処理
public class Player_StageOut : MonoBehaviour
{

    private bool isStageOut = false;
    public bool IsStageOut() { return isStageOut; }




    void Start()
    {
        Debug.Log("子" + GetPlayerChildren().Count);
    }

    void Update()
    {

        Debug.Log(isStageOut);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
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

        foreach (var c in GetPlayerChildren())
        {
            c.GetComponent<Rigidbody2D>().freezeRotation = true;
            //c.GetComponent<Rigidbody2D>().isKinematic = true;
            c.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        yield return null;

        GetComponent<Rigidbody2D>().AddForce(Direction() * 50f);

        //0.5秒待つ
        yield return new WaitForSeconds(0.5f);
        ////加えられてる力をリセット
        foreach (var c in GetPlayerChildren())
        {
            c.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            c.GetComponent<Collider2D>().enabled = false;
           // c.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
        //GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //// GetComponent<Rigidbody2D>().angularVelocity = 0;
        ////反転
        transform.eulerAngles += new Vector3(0, 0, 180);
        //yield return null;
        //向いてる方向に力を加える

        GetComponent<Rigidbody2D>().AddForce(Direction() * 200f);

        //GetComponent<Rigidbody2D>().AddForce(Direction() * 200f);

        yield return new WaitForSeconds(0.2f);

        foreach (var c in GetPlayerChildren())
        {
           // c.GetComponent<Rigidbody2D>().freezeRotation = false;
            //c.GetComponent<Rigidbody2D>().isKinematic = true;
            c.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        //GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //GetComponent<Rigidbody2D>().angularVelocity = 0;
        //GetComponent<Rigidbody2D>().freezeRotation = false;
        isStageOut = false;
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z % 360);

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

    /// <summary>
    /// プレイヤーの子オブジェクトを取得
    /// </summary>
    /// <returns></returns>
    private List<GameObject> GetPlayerChildren()
    {
        List<GameObject> allChildren = new List<GameObject>();
        Transform children = transform.parent.GetComponentInChildren<Transform>();

        foreach (Transform ob in children)
        {
            allChildren.Add(ob.gameObject);
        }

        return allChildren;

    }

}
