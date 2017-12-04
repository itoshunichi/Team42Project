using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 山村
/// ハンマーが特定のオブジェに接触した際に
/// 動きが変にならないようにポイントを動かす
/// </summary>
public class LerpRotation : MonoBehaviour
{

    Quaternion beforeQuaternion;
    private float alpha = 0;
    // Use this for initialization
    void Start()
    {
        beforeQuaternion = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        RotationLerp();
    }

    /// <summary>
    /// Z軸を補完して変える
    /// </summary>
    private void RotationLerp()
    {
        alpha += 0.025f;
        Quaternion.Lerp(transform.rotation, beforeQuaternion, alpha);
    }

    /// <summary>
    /// 向きを変える
    /// </summary>
    /// <param name="angle"></param>
    public void SetRotation(float angle)
    {
        alpha = 0;
        beforeQuaternion = transform.rotation;  //セット
        Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));//向きを変える
    }
}
