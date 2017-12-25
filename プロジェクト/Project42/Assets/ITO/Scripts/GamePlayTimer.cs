using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayTimer:MonoBehaviour {

    //分
    private int minite;
    //秒
    private float second;
    private int oldSecond;
    private bool timerFlag;
    public bool TimerFlag
    {
        set { timerFlag = value; }
    }


    private string text;
    public string Text
    {
        get { return text; }
    }


 //   private Text timerText;
  





	void Start () {
        minite = 0;
        second = 0;
        oldSecond = 0;
        timerFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!timerFlag) return;
        if(Time.timeScale>0)
        {
            second += Time.deltaTime;
            if(second>=60.0f)
            {
                minite++;
                second = second - 60;
            }
            if((int)second != oldSecond)
            {
               text = minite.ToString("00") + ":" + ((int)second).ToString("00");
            }
            oldSecond = (int)second;
        }

        //Debug.Log(text);
		
	}


}
