using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUI : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        AudioManager.Instance.PlayBGM(AUDIO.BGM_GAMEPLAY);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void AnimationEnd()
    {
        anim.SetBool("IsOpen", false);
    }

    public void IsOpen()
    {
        if (!anim.GetBool("IsOpen")) anim.SetBool("IsOpen", true);
        else anim.SetBool("IsOpen", false);
    }
}
