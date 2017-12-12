using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FormBossStageObject))]
public class FormBossStageObjectEditor : Editor
{
    Vector3 snap;
    FormBossStageObject component;

    /// <summary>
    /// ランダムで生成する位置
    /// </summary>
    SerializedProperty coreFormPositions;

    private void OnEnable()
    {
        //SnapSettingsの値を取得する
        var snapX = EditorPrefs.GetFloat("MoveSnapX", 1f);
        var snapY = EditorPrefs.GetFloat("MoveSnapY", 1f);
        snap = new Vector3(snapX, snapY, 0);
        coreFormPositions = serializedObject.FindProperty("formPositions");
    }

    private void OnSceneGUI()
    {
        component = target as FormBossStageObject;

        //serializedPropertyの更新開始
        serializedObject.Update();

        foreach (SerializedProperty l in coreFormPositions)
        {
            Vector2 tmp = l.vector2Value;
            l.vector2Value = PositionHandle2D((Vector2)component.transform.position + tmp,
                Color.magenta, Color.yellow) - (Vector2)component.transform.position;
        }

        Undo.RegisterCompleteObjectUndo(component, "resetNavi");

        //serializedPropertyの更新を適用
        serializedObject.ApplyModifiedProperties();
    }

    private Vector2 PositionHandle2D(Vector2 position, Color xColor, Color yColor)
    {
        var result = new Vector2(position.x, position.y);

        float size = 2f;

        Handles.color = xColor;
        result = Handles.Slider(result, Vector3.right, size, Handles.ArrowHandleCap, snap.x); //X 軸
        Handles.color = yColor;
        result = Handles.Slider(result, Vector3.up, size, Handles.ArrowHandleCap, snap.y); //Y 軸

        Handles.color = Color.white;
        return result;
    }


}
