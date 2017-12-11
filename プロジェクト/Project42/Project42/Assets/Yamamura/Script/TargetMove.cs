using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour {

    GameObject target;//EnergyUI
    float alpha;            //Lerpのアルファ値
    Energy energy;          //Energy
    bool isBonus = true;   //コンボ中に発生したかどうか
	// Use this for initialization
	void Start () {
        target = GameObject.Find("BarPoint");

        energy = GameObject.Find("bar").GetComponent<Energy>();
        if(energy.GetCombCount() > 1){
            //カラー変更                                     赤　　緑　　青　　アルファ値
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            isBonus = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    //移動処理
    private void Move()
    {
        alpha += 0.02f;
        gameObject.transform.position = Vector2.Lerp(transform.position, target.transform.position, alpha);
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
