using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{

    private float nextTime;
    /// <summary>
    /// 点滅周期
    /// </summary>
    [SerializeField]
    private float interval = 1.0f;

    private Image image;

    void Start()
    {
        nextTime = Time.time;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            image.enabled = !image.enabled;

            nextTime += interval;
        }
    }
}
