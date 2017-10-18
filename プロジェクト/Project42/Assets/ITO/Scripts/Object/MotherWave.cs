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
        Reduction();
        DestoryWave();
    }

    /// <summary>
    /// 縮小
    /// </summary>
    private void Reduction()
    {
        //スケールを小さくする値
        float reductionScale = 0.02f;
        transform.localScale -= new Vector3(reductionScale, reductionScale, reductionScale);
    }

    /// <summary>
    /// GameObjectの削除
    /// </summary>
    private void DestoryWave()
    {
        //スケールが0以下になったら
        if (transform.localScale.x <= 0 && transform.localScale.y <= 0)
        {
            Destroy(gameObject);
        }
    }
}
