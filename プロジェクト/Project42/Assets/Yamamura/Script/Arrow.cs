using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject playerSmall;      //プレイヤースモール
    public GameObject playerBig;        //プレイヤービッグ
    private SpriteRenderer sprite;      //矢印画像
    private bool isTap = false;         //Tapしたかどうか
    private Vector3 touchStartPos;      //タッチした場所
    private Vector3 touchEndPos;        //タッチ終わりの場所
    float tapTimer = 0.0f;              //タッチしている時間
    public float flickTime;           //フリック判定時間
    public float flickMagnitude = 100;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //TapHit();
        Flick();

        if (!isTap) transform.position = playerSmall.transform.position; //タッチ位置に合わせる
    }

    /// <summary>
    /// フリック処理
    /// </summary>
    private void Flick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            transform.position = touchStartPos;
            isTap = true;
        }
        if (Input.GetMouseButton(0))
        {
            tapTimer += 0.01f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            Vector2 dir = touchEndPos - touchStartPos;
            if (dir.magnitude >= flickMagnitude && tapTimer <= flickTime)
            {
                var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - touchStartPos);
                transform.localRotation = rotation; //マウスの方向に向く
                if(LineRight(touchStartPos,touchEndPos,playerSmall.transform.position))
                {
                    playerSmall.GetComponent<PlayerController>().R(true);
                }
            }
            tapTimer = 0.0f;
            isTap = false;
        }
    }

    

    //線より右側
    bool LineRight(Vector2 pos1, Vector2 pos2, Vector2 dot)
    {
        Vector2 v1 = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y);
        Vector2 v2 = new Vector2(dot.x - pos1.x, dot.y - pos1.y);
        float det = v1.x * v2.y - v2.x * v1.y;
        if (det < 0) return true;
        return false;
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
                if (hitObject.collider.gameObject.tag != "Player" || hitObject == null)
                {
                    isTap = true;
                    Debug.Log("hit object is " + hitObject.collider.gameObject.name);
                }

            }
        }
    }
    #endregion
}
