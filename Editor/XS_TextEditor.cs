using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(XS_Text))]
public class XS_TextEditor : TMPro.EditorUtilities.TMP_EditorPanelUI
{
    SerializedProperty animacio;
    SerializedProperty button;
    SerializedProperty slider;
    SerializedProperty percentatge;

    protected override void OnEnable()
    {
        animacio = serializedObject.FindProperty("animacio");
        button = serializedObject.FindProperty("button");
        slider = serializedObject.FindProperty("slider");
        percentatge = serializedObject.FindProperty("percentatge");
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

       
        EditorGUILayout.PropertyField(animacio, new GUIContent("animacio"));
        EditorGUILayout.PropertyField(button, new GUIContent("button"));
        EditorGUILayout.PropertyField(slider, new GUIContent("Slider"));
        EditorGUILayout.PropertyField(percentatge, new GUIContent("%"));

        serializedObject.ApplyModifiedProperties();
    }
 


    [MenuItem("CONTEXT/TextMeshProUGUI/Convert to XS_Text", priority = 901)]
    static void To_XS_Text(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        DestroyImmediate((command.context));
        gameObject.AddComponent<XS_Text>();
    }
}