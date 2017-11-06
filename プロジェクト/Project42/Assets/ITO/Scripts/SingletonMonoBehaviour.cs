using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviourWithInit where T : MonoBehaviourWithInit
{
    //インスタンス
    private static T instance;

    //インスタンスを外部から参照する用
    public static T Instance
    {
        get
        {
            //インスタンスがまだ作られていない
            if (instance == null)
            {
                //シーン内からインスタンスを取得
                instance = (T)FindObjectOfType(typeof(T));

                //シーン内に存在しない場合はエラー
                if (instance == null)
                {
                    Debug.LogError(typeof(T) + "is nothing");
                }
                //発見した場合は初期化
                else
                {
                    instance.InitIfNeeded();
                }
            }

            return instance;
        }
    }

    


}

/// <summary>
/// 初期化メソッドを備えたMonoBehaviour
/// </summary>
public class MonoBehaviourWithInit : MonoBehaviour
{
    //初期化したかどうかのフラグ(一度しか初期化が走らないようにするため)
    private bool isInitialized = false;
    public void InitIfNeeded()
    {
        if (isInitialized)
        {
            return;
        }
        Init();
        isInitialized = true;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    protected virtual void Init() { }

    protected virtual void Awake() { }
}
