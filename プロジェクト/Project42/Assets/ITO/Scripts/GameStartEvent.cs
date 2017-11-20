using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ゲーム開始時のイベント
/// </summary>
public class GameStartEvent : MonoBehaviour {

    //プレイヤー
    private GameObject player;
    //ボス
    private GameObject boss;
    //衛星
    private GameObject topSatellite;
    private GameObject bottomSatellite;

    //衛星のLerp移動関係の変数
    private float satelliteMoveStartTime;

    

	void Start () {
        //プレイヤーの移動を止める
        player = GameObject.Find("PlayerSmall");
        player.GetComponent<PlayerSmallController>().IsMoveStop = true;

        topSatellite = GameObject.Find("TopPoint");
        bottomSatellite = GameObject.Find("BottomPoint");
   
        StartCoroutine(Event());
        //AudioManager.Instance.PlayBGM(AUDIO.BGM_GAMEPLAY);

	}
	
	void Update () {
        MoveSatellite(topSatellite, new Vector2(0, 13));
        MoveSatellite(bottomSatellite, new Vector2(0, -13));
	}

    /// <summary>
    /// イベントの進行
    /// </summary>
    /// <returns></returns>
    private IEnumerator Event()
    {
        yield return new WaitForSeconds(1.0f);

        //ボスの読み込み
        boss = Resources.Load<GameObject>("Prefab/Boss");

        //生成
        Instantiate(boss).name = "Boss";
        //ズーム開始
        StartCoroutine(Camera.main.GetComponent<CameraControl>().CameraZoom());

        yield return new WaitForSeconds(1.0f);

        //衛星を表示
        topSatellite.GetComponent<SpriteRenderer>().enabled = true;
        bottomSatellite.GetComponent<SpriteRenderer>().enabled = true;
        satelliteMoveStartTime = Time.timeSinceLevelLoad;
       

        //衛星とエリアの生成
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("RightEria").GetComponent<ActionEria>().InstantiateEriaPoint(new Vector2(8,0));

        //エネミーを生成
        yield return new WaitForSeconds(1.0f);
        InstantiateEnemy();

        yield return new WaitForSeconds(5.0f);
        
        GameObject.Find("RightEria").GetComponent<ActionEria>().BreakEriaPoint(GameObject.Find("ActionEriaPoint"));

        //スクリプトを有効化　プレイヤーの移動を開始
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("ActionEria").GetComponent<FormActionEriaPoint>().enabled = true;
        GameObject.Find("FormBeDestroyedObject").GetComponent<FormBeDestroyedObject>().enabled = true;
        GameObject.Find("FlickController").GetComponent<FlickController>().enabled = true;
        player.GetComponent<PlayerSmallController>().IsMoveStop = false;
    }

    /// <summary>
    /// 衛星の移動(引数で指定した位置に移動)
    /// </summary>
    private void MoveSatellite(GameObject satellite,Vector2 targetPos)
    {
        //非常時だったら処理しない
        if (satellite.GetComponent<SpriteRenderer>().enabled == false) return;
        float moveTime = 1;
        var dift = Time.timeSinceLevelLoad - satelliteMoveStartTime;
        var rate = dift / moveTime;

        satellite.transform.position = Vector3.Lerp(Vector3.zero, targetPos, rate);
      
    }

    /// <summary>
    /// エネミーの生成
    /// </summary>
    private void InstantiateEnemy()
    {
        Vector2 pos = new Vector2(10, 0);
        GameObject enemy = Resources.Load<GameObject>("Prefab/BeDestroyedObject/Enemy/Enemy_C");
        Instantiate(enemy,pos,Quaternion.identity);

    }
}
