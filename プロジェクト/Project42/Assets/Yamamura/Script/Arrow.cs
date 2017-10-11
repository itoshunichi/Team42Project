using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject player;
    private SpriteRenderer sprite;
    private bool isTap = false;
    float rotationZ;
    int rotationCount = 0;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public float flickMagnitude = 100f;
    public float alphaRestriction;
    float tapTimer = 0.0f;
    Vector3 curPoint;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
        rotationZ = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        TapHit();
        if (Input.GetMouseButton(0))
        {
            sprite.enabled = true;
            var cameraPos = Camera.main.WorldToScreenPoint(transform.localPosition);
            var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - cameraPos);
            transform.localRotation = rotation; //マウスの方向に向く
            if (transform.rotation.z < rotationZ) rotationCount--;//左回転
            else if (transform.rotation.z > rotationZ) rotationCount++;//右回転
            else if (transform.rotation.z == rotationZ) rotationCount = 0;//リセット
        }
        if (Input.GetMouseButtonUp(0))
        {
            sprite.enabled = false;
            isTap = false;
            rotationZ = transform.rotation.z;
        }
        touchStartPos = player.transform.position; //タッチ位置に合わせる
    }

    /// <summary>
    /// フリック処理
    /// </summary>
    private void Flick()
    {
        if (Input.GetMouseButtonDown(0)) touchStartPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            tapTimer += 0.01f;
            Debug.Log(tapTimer);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;

            if (tapTimer < 0.15f)
            {
                GameObject obj = new GameObject();
                obj.transform.LookAt(touchEndPos);

            }
            tapTimer = 0.0f;
        }
    }

    #region タップした位置にプレイヤー、エネミーがいるかどうか
    private void TapHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                var hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);

                //プレイヤー
                if (hitObject.collider.gameObject.tag != "UI")
                {
                    isTap = true;
                    Debug.Log("hit object is " + hitObject.collider.gameObject.name);
                }

            }
        }
    }
    #endregion
}
