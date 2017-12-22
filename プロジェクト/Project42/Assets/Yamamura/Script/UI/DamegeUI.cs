using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeUI : MonoBehaviour {

    public PlayerSmallController player;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Damage();
	}

    private void Damage()
    {
        if (player.IsHit())
        {
            anim.SetBool("IsDamage",true);
        }
    }

    public void AnimationEnd()
    {
        anim.SetBool("IsDamage", false);
    }
}
