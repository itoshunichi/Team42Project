using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormActionEriaPoint : MonoBehaviour {

    //右のエリア
    private GameObject rightEria;
    //左のエリア
    private GameObject leftEria;

    /// <summary>
    /// 生成する間隔
    /// </summary>
    [SerializeField]
    private float instantiateInterval;

	// Use this for initialization
	void Start () {
        leftEria = gameObject.transform.GetChild(0).gameObject;
        rightEria = gameObject.transform.GetChild(1).gameObject;

        StartCoroutine(InstantiateEriaPoint());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator InstantiateEriaPoint()
    {
        while(true)
        {
            yield return new WaitForSeconds(instantiateInterval);

            int rnd = Random.Range(0, 2);
            if(rnd == 0)
            {
                leftEria.GetComponent<ActionEria>().InstantiateEriaPoint();
                if(leftEria.GetComponent<ActionEria>().IsMaxPoint())
                    rightEria.GetComponent<ActionEria>().InstantiateEriaPoint();
            }
            else
            {
                rightEria.GetComponent<ActionEria>().InstantiateEriaPoint();
                if (rightEria.GetComponent<ActionEria>().IsMaxPoint())
                    leftEria.GetComponent<ActionEria>().InstantiateEriaPoint();
            }
        }
    }
}
