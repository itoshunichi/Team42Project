using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateParameter{


    [MenuItem("Assets/Create/CreateCommonEnemyParametr")]
    public static void CreateBeDestroyedObjectParameter()
    {
        CreateAsset<Enemy_CommonParametert>();
    }


    public static void CreateAsset<Type>()where Type:ScriptableObject
    {
        Type item = ScriptableObject.CreateInstance<Type>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Data/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }


}
