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
        EditorGUILayout.PropertyField(button, new GUIContent("button", "Si les animacions del texte ha d'estar controlades per un botó, aquí es posa. Compte que només controla les animacions: OnEnter i OnExit"));
        EditorGUILayout.PropertyField(slider, new GUIContent("Slider", "Si el texte serveix per mostrar el valor d'un Slider, aquest s'assigna aquí. No en controla les animacions, només el contingut del texte."));
        EditorGUILayout.PropertyField(percentatge, new GUIContent("%", "Converteix el 'factor' donat per l'Slider controlador en un persentatge."));

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