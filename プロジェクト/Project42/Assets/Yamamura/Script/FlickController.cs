using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController : MonoBehaviour
{

    public GameObject playerSmall;      //プレイヤースモール
    public GameObject playerBig;        //プレイヤービッグ
    public GameObject mainCamera;
    public GameObject spriteobj;
    public GameObject spriteArrow;
    private bool isTap = false;         //Tapしたかどうか
    private Vector3 touchStartPos;      //タッチした場所
    private Vector3 touchEndPos;        //タッチ終わりの場所
    private Vector3 beforeEndPos;       //前回のタッチ終わりの場所
    int flickCount = 0;
    float tapTimer = 0.0f;              //タッチしている時間
    public float flickTime;           //フリック判定時間
    public float flickMagnitude = 100;
    float beforeRadian = 0; //フリックする前の角度
    float afterRadian = 0;  //フリックした後の角度
    public float radianMax = 30;
    bool isFlick = false;

    PlayerSmallController pcs;
    PlayerBigController pcb;

    // Use this for initialization
    void Start()
    {
        pcs = playerSmall.GetComponent<PlayerSmallController>();
        pcb = playerBig.GetComponent<PlayerBigController>();
    }

    // Update is called once per frame
    void Update()
    {
        //TapHit();
        Flick();

        if (!isTap) transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0); //タッチ位置に合わせる
    }

    /// <summary>
    /// フリック処理
    /// </summary>
    private void Flick()
    {
        if (Input.GetMouseButtonDown(0))
        {   //位置セット
            touchStartPos = Input.mousePosition;
            transform.position = touchStartPos;
            if (isFlick) isFlick = false;
            isTap = true;
            Debug.Log("Tap");
        }
        if (Input.GetMouseButton(0))
        {   //タップカウント
            tapTimer += 0.01f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (flickCount == 0) beforeEndPos = Vector2.up;
            else beforeEndPos = touchEndPos;
            touchEndPos = Input.mousePosition;

            Vector2 dir = touchEndPos - touchStartPos;
            if (dir.magnitude >= flickMagnitude && tapTimer <= flickTime)
            {

                var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - touchStartPos);
                transform.localRotation = rotation; //マウスの方向に向く
                Vector2 afterDirection = touchEndPos - touchStartPos;

                if (flickCount == 0) beforeRadian = 90; //初期90
                else beforeRadian = afterRadian;        //二回目以降afterRadianセット
                //角度取得
                afterRadian = Mathf.Atan2(afterDirection.y, afterDirection.x) * Mathf.Rad2Deg;
                //0より小さかったら+360足す
                if (beforeRadian < 0) beforeRadian += 360;
                if (afterRadian < 0) afterRadian += 360;
                //radian = 値が大きい方 - 値が小さい方
                float radian;
                if (beforeRadian < afterRadian) radian = afterRadian - beforeRadian;
                else radian = beforeRadian - afterRadian;

                isFlick = true;
                //radianの値が規定値以下だったらそれ以降は処理しない
                if (radian > radianMax)
                {

                    //操作キャラのスクリプトを入れる
                    if (playerSmall.GetComponent<PlayerSmallController>().playerMode == PlayerMode.PLAYER)
                    {
                        pcs.Reset();
                        pcb.RotationForce(RightFlick(touchStartPos, touchEndPos));

                    }
                    else if (playerBig.GetComponent<PlayerBigController>().playerMode == PlayerMode.PLAYER)
                    {
                        pcb.Reset();
                        pcs.RotationForce(RightFlick(touchStartPos, touchEndPos));
                    }
                    Instantiate(spriteArrow, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0), transform.rotation);
                }
                flickCount += 1;//フリックした回数をカウント
            }
            tapTimer = 0.0f;
            isTap = false;
        }
    }

    //bool RightFlick(Vector2 start,Vector2 end)
    //{
    //    if (start.x > end.x) return true;
    //    return false;
    //}

    float RightFlick(Vector2 start, Vector2 end)
    {
        if (start.x > end.x) return 1;
        return -1;
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

    public void SetRotation(Vector3 pos)
    {
        transform.localRotation = Quaternion.LookRotation(Vector3.forward,pos);
    }

    public bool GetFlick()
    {
        return isFlick;
    }
}
