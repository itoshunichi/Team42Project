using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController : MonoBehaviour
{
    //スクリプト
    public PlayerSmallController[] playerController;
    public Hammer[] hammer;           //ハンマー
    //Object
    public GameObject mainCamera;
    //Vector
    private Vector3 touchStartPos;      //タッチした場所
    private Vector3 touchEndPos;        //タッチ終わりの場所
    private Vector3 beforeEndPos;       //前回のタッチ終わりの場所
    //値
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
    //bool
    private bool isTap = false;         //Tapしたかどうか
    bool isFlick = false;

    public int FlickCount
    {
        get { return flickCount; }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
            if (tapTimer <= flickTime && dir.magnitude <= flickMagnitude)
            {
                // pcs.SetAccelerator();
            }
            else if (dir.magnitude >= flickMagnitude) //&& tapTimer <= flickTime)
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

                //最短距離の場合(最大距離の場合は逆)
                //radian 179以下 - 時計回り
                //radina 181以上 + 反時計回り
                //radianの値が規定値以下だったら回転しない
                RadianCheck(GetRadian());
                foreach (var pc in playerController) pc.Reset();
               
                flickCount += 1;//フリックした回数をカウント
            }
            tapTimer = 0.0f;
            isTap = false;
        }
    }

    public float GetRadian()
    {
        //radian = 値が大きい方 - 値が小さい方
        float radian = 0;
        if (afterRadian > beforeRadian) radian = afterRadian - beforeRadian;
        else radian = beforeRadian - afterRadian;
        return radian;
    }

    private void RadianCheck(float radian)
    {       //前回の角度と比べて大きさくなるほど回す力を大きくするメソッドを呼ぶ
        foreach (var h in hammer)
            //if (radian >= radianMaxOne && radian < radianMaxTwo)
            //  //  h.SetRotationForce(RadinaShortest(), 0);
            //else if (radian >= radianMaxTwo && radian < radianMaxThree)
            // //   h.SetRotationForce(RadinaShortest(), 1);
            //else if (radian >= radianMaxThree)
            //  //  h.SetRotationForce(RadinaShortest(), 2);
           // else 
        if (radian < radianMaxOne)
            {
                foreach (var pc in playerController) pc.SetAccelerator();
                //playerController.SetAccelerator();
            }
    }
    //角度の最短距離
    public bool RadinaShortest()
    {
        //radian = 値が大きい方 - 値が小さい方
        float radian = 0;

        radian = beforeRadian - afterRadian;

        //反時計回り
        //    180以上　0以下ー180以上
        if (radian <= 0)
        {
            if (radian <= -180) return false;
            else if (radian > -180) return true;
        }
        if (radian >= 0)
        {
            if (radian >= 180) return true;
            else if (radian < 180) return false;
        }

        return false;
    }

    public float DebugCheck()
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
        float radian = 0;
        radian = afterRadian - beforeRadian;

        return radian;
    }
}
