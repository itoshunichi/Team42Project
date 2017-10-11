using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// エネミーを生成するエディタ
/// </summary>
[CustomEditor(typeof(FormEnemyObject))]
public class FormEnemyEdit :Editor
{

    Vector3 snap;
    SerializedProperty enemysType;
    SerializedProperty formEnemys;
    List<GameObject> enemysTypeList = new List<GameObject>();
    string[] enemyNames;
    FormEnemyObject component;

    Vector2 formPos;
    //SerializedProperty positionLists;
    //SerializedProperty enemysLists;

    void OnEnable()
    {
        Debug.Log("ena");
        //SnapSettingsの値を取得する
        var snapX = EditorPrefs.GetFloat("MoveSnapX", 1f);
        var snapY = EditorPrefs.GetFloat("MoveSnapY", 1f);
        snap = new Vector3(snapX, snapY, 0);


        ///////////////////////////////////////////////////////////////////////
        enemysType= serializedObject.FindProperty("enemysType");
        formEnemys = serializedObject.FindProperty("formEnemys");

        foreach (SerializedProperty t in enemysType)
        {
            //既にObjectがリストに入ったいたら処理しない
            if (enemysTypeList.Contains((GameObject)t.objectReferenceValue)) return;                
            enemysTypeList.Add((GameObject)t.objectReferenceValue);
        }
        enemyNames = new string[enemysTypeList.Count];
       for(int i = 0;i<enemysTypeList.Count;i++)
        {
            enemyNames[i] = enemysTypeList[i].name;
        }

    }

    void OnSceneGUI()
    {

        
        component = target as FormEnemyObject;

        // serializedPropertyの更新開始
        serializedObject.Update();
        //formEnemys.arraySize = component.FormEnemys.Count;
        CreateEnemy();
        DeleateEnemy();
        ResetEnemy();
        
        // serializedPropertyの更新を適用
        serializedObject.ApplyModifiedProperties();

        Vector2 tmp = formPos;
        formPos = PositionHandle2D((Vector2)component.transform.position + tmp) - (Vector2)component.transform.position;
        Undo.RegisterCompleteObjectUndo(component, "resetNavi");
       


        Debug.Log(formPos);

        
        //if (positionLists.arraySize <= 1)
        //{
        //    //list.arraySize = 2;
        //}


        //foreach (SerializedProperty l in positionLists)
        //{

        //    Vector2 temp = l.vector2Value;
        //    l.vector2Value = PositionHandle2D((Vector2)component.transform.position + temp) - (Vector2)component.transform.position;
        //}


        //positionLists.arraySize = enemysLists.arraySize;

       

       
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

        selectPop = EditorGUILayout.Popup("生成する敵", selectPop, enemyNames);
        //生成ボタンを押したら
        if(GUILayout.Button("生成",GUILayout.Width(50)))
        {
            //リストの名前と一致するオブジェクトを生成する
           foreach(var t in enemysTypeList)
            {
                if(t.name == enemyNames[selectPop])
                {
                    formEnemys.arraySize += 1;
                    formEnemys.GetArrayElementAtIndex(formEnemys.arraySize - 1).objectReferenceValue = (GameObject)Instantiate(t, (Vector2)component.transform.position + formPos,Quaternion.identity);
                    
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
            if (formEnemys.arraySize <= 0) return;
            int index = formEnemys.arraySize - 1;
            GameObject g = (GameObject)formEnemys.GetArrayElementAtIndex(index).objectReferenceValue;
            DestroyImmediate(g);
            formEnemys.arraySize -= 1;
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
           
            foreach(SerializedProperty e in formEnemys)
            {
                DestroyImmediate(e.objectReferenceValue);
            }

           formEnemys.ClearArray();


        }

        Handles.EndGUI();
    }


}
