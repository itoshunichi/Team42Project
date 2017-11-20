using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    //プレイヤー
    private GameObject player;
    private bool isGameStart;

    private GameObject flickController;

    //UI
    private GameObject canvas;
    private GameObject tutorialTextBack;
    private GameObject text1;
    private GameObject text2;
    private GameObject text3;
   


    void Start()
    {
        player = GameObject.Find("PlayerSmall");
        //player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerSmallController>().IsMoveStop = true;
        player.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(Event());
        tutorialTextBack = GameObject.Find("TutorialTextBack");
        canvas = GameObject.Find("Canvas");
        text1 = GameObject.Find("TutorialText1");
        text2 = GameObject.Find("TutorialText2");
        text3 = GameObject.Find("TutorialText3");
        flickController = GameObject.Find("FlickController");
        flickController.GetComponent<FlickController>().enabled = false;

    }

    void Update()
    {
        FirstStopPlayer();
        SetText();
    }

    /// <summary>
    /// MiniBossの生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator Event()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Resources.Load<GameObject>("Prefab/MiniBoss"));
        yield return new WaitForSeconds(1f);
        //ズーム開始
        StartCoroutine(Camera.main.GetComponent<CameraControl>().CameraZoom());
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerSmallController>().IsMoveStop = false;
        yield return new WaitForSeconds(3f);
        flickController.GetComponent<FlickController>().enabled = true;
        tutorialTextBack.GetComponent<Image>().enabled = true;
        text1.GetComponent<Text>().enabled = true;
        

        yield return null;

    }

    private void SetText()
    {
        if(flickController.GetComponent<FlickController>().FlickCount == 1)
        {
            text1.GetComponent<Text>().enabled = false;
            text2.GetComponent<Text>().enabled = true;
            text2.GetComponentInChildren<Image>().enabled = true;
        }

        if(GameObject.Find("mother_shield1") == null)
        {
            text2.GetComponent<Text>().enabled = false;
            text2.GetComponentInChildren<Image>().enabled = false;
            text3.GetComponent<Text>().enabled = true;
            text3.GetComponentInChildren<Image>().enabled = true;
        }
    }

     private void  FirstStopPlayer()
    {
        if(!isGameStart&&player.transform.position.y>=-6)
        {
            Debug.Log("check");
            player.GetComponent<PlayerSmallController>().IsMoveStop = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    
    public void StartGame()
    {
        isGameStart = true;
        player.GetComponent<PlayerSmallController>().IsMoveStop = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<CircleCollider2D>().enabled = true;
        Camera.main.GetComponent<CameraControl>().IsMove = true;
        
    }
}
