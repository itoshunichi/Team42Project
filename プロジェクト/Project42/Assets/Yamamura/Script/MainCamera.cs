using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public GameObject playerSoul;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(playerSoul.transform.position.x, playerSoul.transform.position.y, -10);

    }
}
