using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{


    /// <summary>
    /// エネルギーの最大値
    /// </summary>
    [SerializeField]
    private float maxEnergy;


    /// <summary>
    /// エネルギー
    /// </summary>
    private float energy = 0;


    private Animator animator;


   protected virtual void Start()
    {
        
        AudioManager.Instance.PlaySE(AUDIO.SE_MATHERSPAWN);
        StartCoroutine(AnimationStart());
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
  protected virtual  void Update()
    {
        AwakeFade();
        SetScale();
    }


    public virtual void Dead()
    {
        SceneNavigater.Instance.Change("GameClear");
    }

    /// <summary>
    /// スケールの設定
    /// </summary>
    private void SetScale()
    {
        //エネルギーが0以下だったらスケールは変わらない
        if (energy <= 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
        else
        {
            float scale = energy / 100;
            transform.localScale = new Vector3(1f + scale, 1f + scale, 1);
        }
    }

    #region　エネルギー関係

    /// <summary>
    /// エネルギーの追加
    /// </summary>
    public void AddEnergy()
    {
        energy += 20;
        MaxEnergy();
    }

    /// <summary>
    /// エネルギーが最大まで達したときの処理
    /// </summary>
    private void MaxEnergy()
    {
        //エネルギーが最大以上になったら
        if (energy >= maxEnergy)
        {
            //ゲームオーバーシーンに
            SceneNavigater.Instance.Change("GameOver");
        }
    }

    #endregion


    

    /// <summary>
    /// 生成時のフェード
    /// </summary>
    private void AwakeFade()
    {   
       
        Color color = GetComponent<SpriteRenderer>().color;
        
        if (color.a < 1)
        {
            color.a += 0.05f;
            GetComponent<SpriteRenderer>().color = color;
            animator.speed = 0f;
        }    
    }

    private IEnumerator AnimationStart()
    {      
        yield return new WaitForSeconds(1.5f);
        animator.speed = 1f;
    }

}
