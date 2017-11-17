using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    //プレイヤー
    private GameObject player;
   


    void Start()
    {
        player = GameObject.Find("PlayerSmall");
        //player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerSmallController>().IsMoveStop = true;
        player.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(InstantiateBoss());
    }

    void Update()
    {

    }

    /// <summary>
    /// MiniBossの生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator InstantiateBoss()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Resources.Load<GameObject>("Prefab/MiniBoss"));
        yield return new WaitForSeconds(1f);
        //ズーム開始
        StartCoroutine(Camera.main.GetComponent<CameraControl>().CameraZoom());
        StartCoroutine(MovePlayer());
        yield return null;

    }

    /// <summary>
    /// プレイヤーの管理
    /// </summary>
    /// <returns></returns>
    private IEnumerator MovePlayer()
    {
        //player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerSmallController>().IsMoveStop = false;
        yield return new WaitForSeconds(2.8f);
        player.GetComponent<PlayerSmallController>().IsMoveStop = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void StartGame()
    {
        player.GetComponent<PlayerSmallController>().IsMoveStop = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<CircleCollider2D>().enabled = true;
        Camera.main.GetComponent<CameraControl>().IsMove = true;
    }
}
