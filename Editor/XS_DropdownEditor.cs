using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using TMPro;

[CustomEditor(typeof(XS_Dropdown))]
[CanEditMultipleObjects]
public class XS_DropdownEditor : DropdownEditor
{
    SerializedProperty animacio;

    SerializedProperty variable;

    XS_Dropdown t;

    private new void OnEnable()
    {
        animacio = serializedObject.FindProperty("animacio");
        variable = serializedObject.FindProperty("variable");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (XS_Dropdown)target;
        if (t.transition != Selectable.Transition.None) t.transition = Selectable.Transition.None;
        if(t.template == null)
        {
            foreach (var item in t.GetComponentsInChildren<RectTransform>(true))
            {
                if (item.name == "Template")
                {
                    t.template = item;
                    break;
                }
            }
        }
        if(t.captionText == null)
        {
            foreach (var item in t.GetComponentsInChildren<TMP_Text>())
            {
                if (item.name == "Label")
                {
                    t.captionText = item;
                    break;
                }
            }
        }
        if (t.itemText == null)
        {
            foreach (var item in t.GetComponentsInChildren<TMP_Text>(true))
            {
                if (item.name == "Item Label")
                {
                    t.itemText = item;
                    break;
                }
            }
        }

        EditorGUILayout.PropertyField(animacio, new GUIContent("Animacio"));
        EditorGUILayout.PropertyField(variable, new GUIContent("Variable"));

        serializedObject.ApplyModifiedProperties();
    }

    [MenuItem("CONTEXT/TMP_Dropdown/Convert to XS_Dropdown", priority = 501)]
    static void To_XS_Dropdown(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        DestroyImmediate((command.context));
        gameObject.AddComponent<XS_Dropdown>();
    }
}
