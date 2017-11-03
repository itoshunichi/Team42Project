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

        //力のリセット
        ResetPlayerVelocity();

        yield return null;

        GetComponent<Rigidbody2D>().AddForce(Direction() * 50f);

        //0.5秒待つ
        yield return new WaitForSeconds(0.5f);

        //加えられてる力をリセット
        ResetPlayerVelocity();
        //反転
        foreach (var c in GetPlayerChildren())
        {
           c.transform.eulerAngles += new Vector3(0, 0, 180);
        }
        yield return null;
        //向いてる方向に力を加える

        GetComponent<Rigidbody2D>().AddForce(Direction() * 200f);
        yield return new WaitForSeconds(0.5f);

        //力のリセット
        ResetPlayerVelocity();

        yield return new WaitForSeconds(0.2f);

        isStageOut = false;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z % 360);
        GameObject.Find("FlickController").transform.rotation = transform.rotation;

        yield break;
    }

    private void FixedUpdate()
    {

    }

    /// <summary>
    /// 加えられてる力のリセット
    /// </summary>
    private void ResetPlayerVelocity()
    {
        foreach (var c in GetPlayerChildren())
        {
            c.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 自身の向きを取得
    /// </summary>
    /// <returns></returns>
    private Vector3 Direction()
    {
        //自身の向きベクトル取得
        float angleDir = transform.eulerAngles.z * (Mathf.PI / 180f);
        Vector3 dir = new Vector3(-Mathf.Sin(angleDir), Mathf.Cos(angleDir), 0.0f);
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
