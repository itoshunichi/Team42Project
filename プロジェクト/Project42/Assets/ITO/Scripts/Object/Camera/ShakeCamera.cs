using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

    [SerializeField]
    private float shake_decay = 0.002f;
    [SerializeField]
    private float coef_shake_intensity = 0.3f;
    //中心位置
    private Vector3 originPosition;
    private Quaternion originRotation;
    //揺れる強さ
    private float shake_intensity;

    private void Start()
    {
        //Debug.Log(GameObject.Find("FormEnemyObj").GetComponent<FormEnemyObject>().FormEnemys.Count);
        Shake();//テスト
    }


    void Update () {
        if(shake_intensity>0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * 2f,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * 2f,
                0,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * 2f
                );
            shake_intensity -= shake_decay;
        }
		
	}

    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = coef_shake_intensity;
    }
}
