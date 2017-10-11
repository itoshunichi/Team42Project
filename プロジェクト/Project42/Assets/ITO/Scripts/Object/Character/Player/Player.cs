//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// 体型
///// </summary>
//public enum BodyType
//{
//    SKINNY,//細身
//    NORMAL,//普通
//    MACHO,//マッチョ
//    PEOPLE_OUTSIDE,//人外級の肉体
//    LORD//魔王級の肉体
//}


//public class Player : MonoBehaviour {

   
//    /// <summary>
//    /// アドレナリンポイント(これによって体系が変化)
//    /// </summary>
//    [SerializeField]
//    private int adrenalinePoint ;

//    /// <summary>
//    /// 体型
//    /// </summary>
//    private BodyType bodyType;

//    /// <summary>
//    /// コンボ
//    /// </summary>
//    private int comboCounter;

//    /// <summary>
//    /// アドレナリンを加えるポイントの合計値
//    /// </summary>
//    private int totalAddAdrenalinPoint;

//    /// <summary>
//    /// 各パラメーター
//    /// </summary>
//    private PlayerParameter skinny_Parameter;
//    private PlayerParameter normal_Parameter;
//    private PlayerParameter macho_Parameter;
//    private PlayerParameter peopleOutside_Parameter;
//    private PlayerParameter lord_Parameter;



//	void Start () {
//        //Resourceフォルダからパラメーターの読み込み
//        skinny_Parameter = Resources.Load<PlayerParameter>("Data/PlayerParameter/1_SKINNY_Parameter");
//        normal_Parameter = Resources.Load<PlayerParameter>("Data/PlayerParameter/2_NORMAL_Parameter");
//        macho_Parameter = Resources.Load<PlayerParameter>("Data/PlayerParameter/3_MACHO_Parameter");
//        peopleOutside_Parameter = Resources.Load<PlayerParameter>("Data/PlayerParameter/4_PEOPLE_OUTSIDE_Parameter");
//        lord_Parameter = Resources.Load<PlayerParameter>("Data/PlayerParameter/5_LORD_Parameter");
//    }


//	void Update () {

//        if(Input.GetKey(KeyCode.Space))
//        {
//            transform.position += new Vector3(0.1f, 0, 0);
//        }

//        SetAnimatorController();
//        SetBodyType();
//        //Debug
//        Debug.Log("アドレナリンポイント" + adrenalinePoint);
//        Debug.Log("体型" + bodyType);
//        Debug.Log("攻撃力"+AttackPower);
//	}

//    /// <summary>
//    /// Animatorの設定
//    /// </summary>
//    private void SetAnimatorController()
//    {
//        //現在の体型によって設定
//        switch (bodyType)
//        {
//            case BodyType.SKINNY:
//                GetComponent<Animator>().runtimeAnimatorController = skinny_Parameter.animatorContoller;
//                break;
//            case BodyType.NORMAL:
//                GetComponent<Animator>().runtimeAnimatorController = normal_Parameter.animatorContoller;
//                break;
//            case BodyType.MACHO:
//                GetComponent<Animator>().runtimeAnimatorController = macho_Parameter.animatorContoller;
//                break;
//            case BodyType.PEOPLE_OUTSIDE:
//                GetComponent<Animator>().runtimeAnimatorController = peopleOutside_Parameter.animatorContoller;
//                break;
//            case BodyType.LORD:
//                GetComponent<Animator>().runtimeAnimatorController = lord_Parameter.animatorContoller;
//                break;
//        }

//    }

//    /// <summary>
//    /// 攻撃力
//    /// </summary>
//    /// <returns></returns>
//    public int AttackPower
//    {
//        get {
//            //現在の体型によって設定
//            switch (bodyType)
//            {
//                case BodyType.SKINNY:
//                    return skinny_Parameter.attackPower;
//                case BodyType.NORMAL:
//                    return normal_Parameter.attackPower;
//                case BodyType.MACHO:
//                    return macho_Parameter.attackPower;
//                case BodyType.PEOPLE_OUTSIDE:
//                    return peopleOutside_Parameter.attackPower;
//                case BodyType.LORD:
//                    return lord_Parameter.attackPower;
//            }
//            return 0;
//        }

       
//    }

//    /// <summary>
//    /// 体型の設定
//    /// </summary>
//    private void SetBodyType()
//    {
//        if (IsBodyTypeRange(skinny_Parameter)) bodyType = BodyType.SKINNY;
//        if (IsBodyTypeRange(normal_Parameter)) bodyType = BodyType.NORMAL;
//        if (IsBodyTypeRange(macho_Parameter)) bodyType = BodyType.MACHO;
//        if (IsBodyTypeRange(peopleOutside_Parameter)) bodyType = BodyType.PEOPLE_OUTSIDE;
//        if (IsBodyTypeRange(lord_Parameter)) bodyType = BodyType.LORD;
//    }

//    /// <summary>
//    /// アドレナリンポイントがパラメータで設定されている値の範囲かどうか
//    /// </summary>
//    /// <param name="parameter"></param>
//    /// <returns></returns>
//    private bool IsBodyTypeRange(PlayerParameter parameter)
//    {
//        int minPoint = parameter.minAdrenalinePoint;
//        int maxPoint = parameter.maxAdrenalinePoint;

//        //アドレナリンポイントがパラメーターの最小値以上最大値以下のとき
//        if(adrenalinePoint >=minPoint&&adrenalinePoint <=maxPoint)
//        {
//            return true;
//        }

//        return false;
//    }

//    /// <summary>
//    /// アドレナリンを加えるポイントの加算
//    ///ハンマーが対象に当たったときに呼ぶ
//    /// </summary>
//    public void AddTotalAddAdrenalinPoint(GameObject obj)
//    {
//        comboCounter += 1;//コンボの加算
//        totalAddAdrenalinPoint+= obj.GetComponent<AdrenalinObject>().AdrenalinPoint;
//    }

//    /// <summary>
//    /// アドレナリンポイントの加算
//    /// 攻撃終了時に呼ぶ
//    /// </summary>
//    public void AddAdrenalinPoint()
//    {
//        //1コンボだったらそのまま合計値を加える
//        if (comboCounter == 1) adrenalinePoint += totalAddAdrenalinPoint;
//        else
//        {
//            int point;
//            point = totalAddAdrenalinPoint * (comboCounter + 10) / 10;
//            adrenalinePoint += point;   
//        }
//        comboCounter = 0;
//    }
//}
