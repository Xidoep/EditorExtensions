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
    SerializedProperty component;
    //SerializedProperty onEnter;
    //SerializedProperty onExit;

    Button t;

    private new void OnEnable()
    {
        animacio = serializedObject.FindProperty("animacio");
        component = serializedObject.FindProperty("component");
        //onEnter = serializedObject.FindProperty("onEnter");
        //onExit = serializedObject.FindProperty("onExit");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (Button)target;
        if (t.transition != Selectable.Transition.None) t.transition = Selectable.Transition.None;

        EditorGUILayout.PropertyField(animacio, new GUIContent("Animacio"));
        EditorGUILayout.PropertyField(component, new GUIContent("Per substituir Image", "Posa un component aqui si vols que sigui substituit per la imatge que fa servir de base."));
        //EditorGUILayout.PropertyField(onEnter, new GUIContent("On Enter"));
        //EditorGUILayout.PropertyField(onExit, new GUIContent("On Exit"));

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


