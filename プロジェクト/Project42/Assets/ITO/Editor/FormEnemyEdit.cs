using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// 破壊されるものを生成するエディタ
/// </summary>
[CustomEditor(typeof(FormBeDestroyedObject))]
public class FormBeDestroyedObjectEdit :Editor
{

    Vector3 snap;

    /// <summary>
    /// オブジェクトの種類
    /// </summary>
    SerializedProperty objectType;
    /// <summary>
    /// 生成するオブジェクト
    /// </summary>
    SerializedProperty formObjects;
    /// <summary>
    /// ランダムで生成する位置
    /// </summary>
    SerializedProperty randomFomrmPoints;

    List<GameObject> objectsTypeList = new List<GameObject>();
    string[] objectNames;
    FormBeDestroyedObject component;


    void OnEnable()
    {
        //SnapSettingsの値を取得する
        var snapX = EditorPrefs.GetFloat("MoveSnapX", 1f);
        var snapY = EditorPrefs.GetFloat("MoveSnapY", 1f);
        snap = new Vector3(snapX, snapY, 0);


        ///////////////////////////////////////////////////////////////////////
        objectType= serializedObject.FindProperty("objectsType");
        formObjects = serializedObject.FindProperty("formObjects");
        randomFomrmPoints = serializedObject.FindProperty("randomFormPoints");

        foreach (SerializedProperty t in objectType)
        {
            //既にObjectがリストに入ったいたら処理しない
            if (objectsTypeList.Contains((GameObject)t.objectReferenceValue)) return;                
            objectsTypeList.Add((GameObject)t.objectReferenceValue);
        }
        objectNames = new string[objectsTypeList.Count];
       for(int i = 0;i<objectsTypeList.Count;i++)
        {
            objectNames[i] = objectsTypeList[i].name;
        }

    }

    void OnSceneGUI()
    {       
        component = target as FormBeDestroyedObject;

        // serializedPropertyの更新開始
        serializedObject.Update();
        CreateEnemy();
        DeleateEnemy();
        ResetEnemy();

        foreach (SerializedProperty l in randomFomrmPoints)
        {

            Vector2 tmp = l.vector2Value;
            l.vector2Value = PositionHandle2D((Vector2)component.transform.position + tmp) - (Vector2)component.transform.position;
        }

        Undo.RegisterCompleteObjectUndo(component, "resetNavi");


        // serializedPropertyの更新を適用
        serializedObject.ApplyModifiedProperties();

       
    }

    /// <summary>
	/// Handle作成
	/// </summary>
	/// <param name="position"></param>
	/// <returns></returns>
	private Vector3 PositionHandle3D(Vector3 position)
    {
        var result = new Vector3(position.x, position.y, position.z);

        float size = 2f;

        Handles.color = Color.magenta;
        result = Handles.Slider(result, Vector3.right, size, Handles.ArrowHandleCap, snap.x); //X 軸
        Handles.color = Color.yellow;
        result = Handles.Slider(result, Vector3.up, size, Handles.ArrowHandleCap, snap.y); //Y 軸
        result.z = 0;

        Handles.color = Color.white;
        return result;
    }

    private Vector2 PositionHandle2D(Vector2 position)
    {
        var result = new Vector2(position.x, position.y);

        float size = 2f;

        Handles.color = Color.magenta;
        result = Handles.Slider(result, Vector3.right, size, Handles.ArrowHandleCap, snap.x); //X 軸
        Handles.color = Color.yellow;
        result = Handles.Slider(result, Vector3.up, size, Handles.ArrowHandleCap, snap.y); //Y 軸

        Handles.color = Color.white;
        return result;
    }


    int selectPop = 0;
    

    /// <summary>
    /// エネミーの生成
    /// </summary>
    private void CreateEnemy()
    {
        Handles.BeginGUI();

        selectPop = EditorGUILayout.Popup("生成する敵", selectPop, objectNames);
        //生成ボタンを押したら
        if(GUILayout.Button("生成",GUILayout.Width(50)))
        {
            //リストの名前と一致するオブジェクトを生成する
           foreach(var t in objectsTypeList)
            {
                if(t.name == objectNames[selectPop])
                {
                    formObjects.arraySize += 1;
                    formObjects.GetArrayElementAtIndex(formObjects.arraySize - 1).objectReferenceValue = 
                        (GameObject)Instantiate(t);
                    
                    //component.AddEnemy(t, (Vector2)component.transform.position + formPos);                   
                }
            }
        }
        Handles.EndGUI();

    }

    /// <summary>
    /// 最新のエネミーを削除
    /// </summary>
    private void DeleateEnemy()
    {
        
        Handles.BeginGUI();

        if(GUILayout.Button("削除",GUILayout.Width(50)))
        {
            if (formObjects.arraySize <= 0) return;
            int index = formObjects.arraySize - 1;
            GameObject g = (GameObject)formObjects.GetArrayElementAtIndex(index).objectReferenceValue;
            DestroyImmediate(g);
            formObjects.arraySize -= 1;
            //formEnemys.DeleteArrayElementAtIndex(formEnemys.arraySize-1);
        }

        Handles.EndGUI();
    }

    /// <summary>
    /// エネミーのリセット
    /// </summary>
    private void ResetEnemy()
    {
        Handles.BeginGUI();

        if(GUILayout.Button("リセット",GUILayout.Width(70)))
        {
           
            foreach(SerializedProperty e in formObjects)
            {
                DestroyImmediate(e.objectReferenceValue);
            }

           formObjects.ClearArray();


        }

        Handles.EndGUI();
    }


}
