using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    private LineRenderer line;
    //線の端の位置
    private Vector2 extendLinePoint;
    private float lineExtendTime;
    private Vector2 extendStartPosition;
    private Vector2 extenddEndPosition;
    private bool isExtend;
    public bool IsExtend
    {
        get { return isExtend; }
    }



    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        extendStartPosition = transform.position;
        extenddEndPosition = transform.position;
    }

    private void Update()
    {
        Extend();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, extendLinePoint);
    }
    public void LineUpdate()
    {

    }

    /// <summary>
    /// 線を伸ばすのを開始する
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    public void StartLineExtend(Vector2 startPos, Vector2 targetPos)
    {

        isExtend = true;
        lineExtendTime = Time.timeSinceLevelLoad;
        extendStartPosition = startPos;
        extenddEndPosition = targetPos;
        SetEnabled(true);
    }

    private void Extend()
    {
        if (!isExtend) return;
        float time = 2;
        var dift = Time.timeSinceLevelLoad - lineExtendTime;
        if (dift > time)
        {
            isExtend = false;
        }

        var rate = dift / time;

        extendLinePoint = Vector2.Lerp(extendStartPosition, extenddEndPosition, rate);
    }

    public void SetEnabled(bool enabled)
    {
        line.enabled = enabled;
    }
}
