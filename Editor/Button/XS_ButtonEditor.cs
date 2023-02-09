using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;


[CustomEditor(typeof(XS_Button))]
[CanEditMultipleObjects]
public class XS_ButtonEditor : ButtonEditor
{
    SerializedProperty animacio;
    SerializedProperty onEnter;
    SerializedProperty onExit;

    private new void OnEnable()
    {
        animacio = serializedObject.FindProperty("animacio");
        onEnter = serializedObject.FindProperty("onEnter");
        onExit = serializedObject.FindProperty("onExit");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //XS_Button button = (XS_Button)target;

        EditorGUILayout.PropertyField(animacio, new GUIContent("Animacio"));
        EditorGUILayout.PropertyField(onEnter, new GUIContent("On Enter"));
        EditorGUILayout.PropertyField(onExit, new GUIContent("On Exit"));

        serializedObject.ApplyModifiedProperties();
    }



    [MenuItem("CONTEXT/Button/Convert to XS_Button", priority = 501)]
    static void To_XS_Button(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        DestroyImmediate((command.context));
        gameObject.AddComponent<XS_Button>();
    }
}


