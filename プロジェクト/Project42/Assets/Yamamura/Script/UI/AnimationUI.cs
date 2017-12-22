using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUI : MonoBehaviour {

    public Animator optionBackAnim;
    public Animator tapStartAnim;
	// Use this for initialization
	void Start () {
        AudioManager.Instance.PlayBGM(AUDIO.BGM_TITLE);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AnimationEnd()
    {
        optionBackAnim.SetBool("IsOpen", false);
    }

    public void IsOpen()
    {
        AudioManager.Instance.PlaySE(AUDIO.SE_DECISION);
        if (!optionBackAnim.GetBool("IsOpen"))
        {
            optionBackAnim.SetBool("IsOpen", true);
            tapStartAnim.SetBool("IsStop", true);
        }
        else
        {
            optionBackAnim.SetBool("IsOpen", false);
            tapStartAnim.SetBool("IsStop", false);
        }
    }
}
