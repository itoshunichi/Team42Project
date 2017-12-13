using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{

    public PlayerSmallController player;
    public FlickController flick;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "フリック回数:" + flick.FlickCount + "敵Hit回数:" + player.EnemyHitCount;
    }
}
