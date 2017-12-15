using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{

    GameObject target;//EnergyUI
    float alpha;            //Lerpのアルファ値
    Energy energy;          //Energy
    bool isBonus = true;   //コンボ中に発生したかどうか
    Vector2[] c = new Vector2[3];
    float time;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("BarPoint");

        energy = GameObject.Find("bar").GetComponent<Energy>();
        if (energy.GetCombCount() > 1)
        {
            //カラー変更                                     赤　　緑　　青　　アルファ値
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 1.0f);

            isBonus = true;
        }
        c[2] = target.transform.position;
        c[1] = transform.position - target.transform.position / 3;
        //ランダムで撮った値に応じて
        if (Random.Range(-1, 1) >= 0)
        {
            c[1].x += 18;
            if (Random.Range(-1, 1) >= 0) iTween.MoveBy(gameObject, iTween.Hash("x", -4, "y", 2, "easeType", iTween.EaseType.easeInOutCubic, "loopType", "none", "time", 0.6f));
            else iTween.MoveBy(gameObject, iTween.Hash("x", -4, "y", -2, "easeType", iTween.EaseType.easeInOutCubic, "loopType", "none", "time", 0.6f));
        }
        else
        {
            c[1].x -= 18;
            if (Random.Range(-1, 1) >= 0) iTween.MoveBy(gameObject, iTween.Hash("x", 4, "y", -2, "easeType", iTween.EaseType.easeInOutCubic, "loopType", "none", "time", 0.6f));
            else iTween.MoveBy(gameObject, iTween.Hash("x", 4, "y", 2, "easeType", iTween.EaseType.easeInOutCubic, "loopType", "none", "time", 0.6f));
        } 
        c[0] = transform.position;
        }

    // Update is called once per frame
    void Update()
    {
        time += 0.02f;
        if (time < 1f)
        {
            c[0] = transform.position;
        }
        else if (time > 1f) Move();
    }

    //移動処理
    private void Move()
    {
        //c[2] = target.transform.position;
        alpha += 0.032f;
        transform.position = BezierCurve2(c[0], c[1], c[2], alpha);
        //iTween.MoveTo(gameObject, BezierCurve2(c, Time.deltaTime), 3);
        // iTween.MoveTo(gameObject, iTween.Hash("c", "c", "time", 2,"easetype" ,iTween.EaseType.easeOutSine));
        Mathf.Clamp(alpha, 0, 1);
    }

    //2次ベジェ曲線 入力は三点
    private Vector2 BezierCurve2(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
       Vector2 pos = new Vector2((1 - t) * (1 - t) * p0.x + 2 * (1 - t) * t * p1.x + t * t * p2.x, 
            (1 - t) * (1 - t) * p0.y + 2 * (1 - t) * t * p1.y + t * t * p2.y);
       return pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "bar")
        {
            energy.AddEnergy();
            Destroy(gameObject);
        }
    }
}
