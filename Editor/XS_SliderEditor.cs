using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(XS_Slider))]
[CanEditMultipleObjects]
public class XS_SliderEditor : SliderEditor
{
    SerializedProperty animacio;
    SerializedProperty so;

    SerializedProperty variable;

    Slider t;

    private new void OnEnable()
    {
        animacio = serializedObject.FindProperty("animacio");
        so = serializedObject.FindProperty("so");
        variable = serializedObject.FindProperty("variable");

        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (Slider)target;
        if (t.transition != Selectable.Transition.None) t.transition = Selectable.Transition.None;
        if (t.fillRect == null) 
        {
            foreach (var item in t.GetComponentsInChildren<RectTransform>())
            {
                if (item.name == "Fill Area")
                {
                    item.anchorMin = Vector2.zero;
                    item.anchorMax = Vector2.one;
                    break;
                }
            }
            foreach (var item in t.GetComponentsInChildren<RectTransform>())
            {
                if (item.name == "Fill")
                {
                    t.fillRect = item;
                    break;
                }
            }
        }
        if(t.handleRect == null)
        {
            foreach (var item in t.GetComponentsInChildren<RectTransform>())
            {
                if (item.name == "Handle Slide Area")
                {
                    item.anchorMin = Vector2.zero;
                    item.anchorMax = Vector2.one;
                    item.pivot = new Vector2(0, 0.5f);
                    item.anchoredPosition = new Vector2(25, 0);
                    item.sizeDelta = new Vector2(-50, 1);
                    break;
                }
            }
            foreach (var item in t.GetComponentsInChildren<RectTransform>())
            {
                if (item.name == "Handle")
                {
                    t.handleRect = item;
                    t.image = item.GetComponent<Image>();
                    break;
                }
            }
        }

        EditorGUILayout.PropertyField(animacio, new GUIContent("Animacio"));
        EditorGUILayout.PropertyField(so, new GUIContent("So"));
        EditorGUILayout.Space(20);
        EditorGUILayout.PropertyField(variable, new GUIContent("Variable"));
        EditorGUILayout.Space(20);
        /*EditorGUILayout.PropertyField(guardat, new GUIContent("Guardat"));
        EditorGUILayout.PropertyField(key, new GUIContent("Key"));
        EditorGUILayout.PropertyField(local, new GUIContent("Local"));
        EditorGUILayout.PropertyField(perDefecte, new GUIContent("Valor per defecte"));
        */
        serializedObject.ApplyModifiedProperties();
    }

    [MenuItem("CONTEXT/Slider/Convert to XS_Slider", priority = 501)]
    static void To_XS_Slider(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        DestroyImmediate((command.context));
        gameObject.AddComponent<XS_Slider>();
    }
}
