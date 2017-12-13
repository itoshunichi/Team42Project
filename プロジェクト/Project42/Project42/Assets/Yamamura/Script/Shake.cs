using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    public float x;
    public float y;
    public float shakeTime;

    public void ShakeObject()
    {
        iTween.ShakePosition(gameObject, iTween.Hash("x", x, "y", y, "time", shakeTime));
    }
}
