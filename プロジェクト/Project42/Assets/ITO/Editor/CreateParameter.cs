using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateParameter{

    [MenuItem("Assets/Create/CreateEnemyParameter")]
    public static void CreateEnemyParameter()
    {
        CreateAsset<EnemyParameter>();
    }

    [MenuItem("Assets/Create/CreatePlayerParameter")]
    public static void CreatePlayerParameter()
    {
        //CreateAsset<PlayerParameter>();
    }


    [MenuItem("Assets/Create/CreateBeDestroyedObjectParameter")]
    public static void CreateBeDestroyedObjectParameter()
    {
        CreateAsset<BeDestroyedObjectParameter>();
    }


    public static void CreateAsset<Type>()where Type:ScriptableObject
    {
        Type item = ScriptableObject.CreateInstance<Type>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Parameter/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }


}
