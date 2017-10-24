using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherWave : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Enlargement();
        DestoryWave();
    }

    /// <summary>
    /// 拡大
    /// </summary>
    private void Enlargement()
    {
        //スケールを小さくする値
        float enlargemenScale = 0.06f;
        transform.localScale += new Vector3(enlargemenScale, enlargemenScale, enlargemenScale);
    }

    /// <summary>
    /// GameObjectの削除
    /// </summary>
    private void DestoryWave()
    {
        //スケールが0以下になったら
        if (transform.localScale.x >= 4 && transform.localScale.y >= 4)
        {
            Destroy(gameObject);
        }
    }
}
