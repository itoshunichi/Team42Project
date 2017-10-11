using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateEnemyParameter{

    [MenuItem("Assets/Create/CreateEnemyParameter")]
    public static void CreateAsset()
    {
        CreateAsset<EnemyParameter>();
    }

    public static void CreateAsset<Type>()where Type:ScriptableObject
    {
        Type item = ScriptableObject.CreateInstance<Type>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Data/EnemyParameter/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }


}
