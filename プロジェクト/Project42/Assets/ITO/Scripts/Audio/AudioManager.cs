using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGMとSEの管理をするマネージャー。
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{

    //オーディオファイルのパス
    private const string BGM_PATH = "Audio/BGM";
    private const string SE_PATH = "Audio/SE";

    //ボリューム保存用のkeyとデフォルト値
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const string SE_VOLUME_KEY = "SE_VoLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 1.0f;
    private const float SE_VOLUME_DEFAULT = 1.0f;

    //BGMがフェードするのにかかる時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
    private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

    //次流すBGM名、SE名
    private string _nextBGMName;
    private string _nextSEName;

    //BGMをフェードアウト中か
    private bool _isFadeOut = false;

    //BGM用、SE用に分けてオーディオソースを待つ
    private AudioSource _bgmSource;
    private List<AudioSource> _seSourceList;
    private int SE_SOURCE_NUM;


    //全Audio保持
    private Dictionary<string, AudioClip> _bgmDic, _seDic;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        //シーンを跨いでも消えないように
        DontDestroyOnLoad(this.gameObject);



        //リソースフォルダから全SE&BGMのファイルを読み込みセット
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();

        //ファイルをすべて取得
        object[] bgmList = Resources.LoadAll(BGM_PATH);
        object[] seList = Resources.LoadAll(SE_PATH);

        foreach (AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }

        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }

        SE_SOURCE_NUM = seList.Length;


        //オーディオリスナーおよびオーディオソースをSE+1(BGMの分)作成
        gameObject.AddComponent<AudioListener>();
        for (int i = 0; i < SE_SOURCE_NUM + 1; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }

        //作成したオーディオソースを取得して各変数に設定、ボリュームも設定
        AudioSource[] audioSourceArray = GetComponents<AudioSource>();
        _seSourceList = new List<AudioSource>();

        for (int i = 0; i < audioSourceArray.Length; i++)
        {
            audioSourceArray[i].playOnAwake = false;

            if (i == 0)
            {
                audioSourceArray[i].loop = true;
                _bgmSource = audioSourceArray[i];
                _bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);

            }
            else
            {
                _seSourceList.Add(audioSourceArray[i]);
                audioSourceArray[i].volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFAULT);
            }
        }





    }


    //指定したファイル名のSEを流す。第二引数のdekaに指定しあ時間だけ再生までの間隔をあける
    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }

    private void DelayPlaySE()
    {
        foreach (AudioSource seSource in _seSourceList)
        {
            if (!seSource.isPlaying)
            {
                seSource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
                return;
            }
        }
    }

    /// <summary>
    /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから
    /// </summary>
    /// <param name="bgmName">ファイル名</param>
    /// <param name="fadeSpeedrate">フェードアウトするスピード</param>
    public void PlayBGM(string bgmName, float fadeSpeedrate = BGM_FADE_SPEED_RATE_HIGH)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "という名前のBGMがありません");
            return;
        }

        //現在BGMが流れていないときはそのまま流す
        if (!_bgmSource.isPlaying)
        {
            _nextBGMName = "";
            _bgmSource.clip = _bgmDic[bgmName] as AudioClip;
            _bgmSource.Play();
        }

        //違うBGMが流れている時は、流れているBGMをフェードアウトさせてからフェードアウトさせてから次を流す。
        //同じBGMが流れている時はスルー
        else if (_bgmSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedrate);
        }
    }

    /// <summary>
    /// BGMをすぐに止める
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSppedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    /// <param name="fadeSpeedRate"></param>
    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
    {
        _bgmFadeSpeedRate = fadeSpeedRate;
        _isFadeOut = true;
    }


    void Update()
    {

        if (!_isFadeOut)
        {
            return;
        }

        //徐々にボリュームを下げていき、ボリュームが０になったらボリュームを戻し次の曲を流す
        _bgmSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
        if (_bgmSource.volume <= 0)
        {
            _bgmSource.Stop();
            _bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            _isFadeOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }
    }

    /// <summary>
    /// BGMとSEのボリュームを別々に変更保存
    /// </summary>
    /// <param name="BGMVolume"></param>
    /// <param name="SEVolume"></param>
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        _bgmSource.volume = BGMVolume;
        foreach (AudioSource seSource in _seSourceList)
        {
            seSource.volume = SEVolume;
        }

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }

    /// <summary>
    /// BGMだけ音量を変更保存
    /// </summary>
    /// <param name="BGMVolume"></param>
    public void ChangeBGMVolume(float BGMVolume)
    {
        _bgmSource.volume = BGMVolume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
    }

    /// <summary>
    /// SEだけ音量を変更保存
    /// </summary>
    /// <param name="SEVolume"></param>
    public void ChangeSEVolume(float SEVolume)
    {
        foreach (AudioSource seSource in _seSourceList)
        {
            seSource.volume = SEVolume;
        }
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }
}
