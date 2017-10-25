using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{

    public GameObject player;
    public GameObject p;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var screenMousePos = Input.mousePosition;
        screenMousePos.z = -Camera.main.transform.position.z;
        var curPoint = Camera.main.ScreenToWorldPoint(screenMousePos);
        Vector2 dis = curPoint - player.transform.position;
        var ro = p.transform.position - player.transform.position;
        var rotationZ = Mathf.Atan2(ro.y,ro.x)*Mathf.Rad2Deg;
        if(rotationZ < 0)rotationZ += 360;

        text.text = "Small:" + ro.normalized + "Big:" + rotationZ;
    }
}
