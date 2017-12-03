using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    private float time;
    private float timer;

    private bool isEnd;
    public bool IsEnd
    {
        get { return isEnd; }
    }

    public Timer(float time)
    {
        this.time = time;
        timer = 0;
    }

    public void Reset()
    {
        isEnd = false;
        timer = 0;
    }

    public void UpdateTimer()
    {
        if (isEnd) return;
        timer += Time.deltaTime;
        if (timer >= time)
        {
            isEnd = true;
        }
    }
}
