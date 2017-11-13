using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController : MonoBehaviour
{

    public GameObject playerSmall;      //プレイヤースモール
    public GameObject hammer;           //ハンマー
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
    public float radianMaxOne = 30;
    public float radianMaxTwo = 60;
    public float radianMaxThree = 90;
    public float radianMaxFour = 120;
    bool isFlick = false;

    Player_StageOut stageOut;
    PlayerSmallController pcs;

    // Use this for initialization
    void Start()
    {
        pcs = playerSmall.GetComponent<PlayerSmallController>();
        stageOut = playerSmall.GetComponent<Player_StageOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stageOut.IsStageOut()) Flick();

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
            isTap = true;
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
                float radian = afterRadian - beforeRadian;
                if (radian < 0) radian += 360;

                //最短距離の場合(最大距離の場合は逆)
                //radian 179以下 - 時計回り
                //radina 181以上 + 反時計回り
                //radianの値が規定値以下だったら回転しない
                RadianCheck(radian);

                pcs.Reset();//プレイヤーの速度等リセット
                //矢印画像生成
                Instantiate(spriteArrow, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0), transform.rotation);

                flickCount += 1;//フリックした回数をカウント
            }
            tapTimer = 0.0f;
            isTap = false;
        }
    }

    private void RadianCheck(float radian)
    {       //前回の角度と比べて大きさくなるほど回す力を大きくするメソッドを呼ぶ
        if (radian > radianMaxOne && radian < radianMaxTwo)
            hammer.GetComponent<Hammer>().SetRotationForce(RadinaShortest(radian), 0);
        else if (radian > radianMaxTwo && radian < radianMaxThree)
            hammer.GetComponent<Hammer>().SetRotationForce(RadinaShortest(radian), 1);
        else if (radian >= radianMaxThree)
            hammer.GetComponent<Hammer>().SetRotationForce(RadinaShortest(radian), 2);
    }
    //角度の最短距離
    private bool RadinaShortest(float radian)
    {
        if (radian < 180) return true;//時計回り
        return false;//反時計周り
    }

    bool RightFlick(Vector2 start, Vector2 end)
    {
        if (start.x > end.x) return true;
        return false;
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
        transform.localRotation = Quaternion.LookRotation(Vector3.forward, pos);
    }

    public float radian()
    {
        return afterRadian;
    }
}
