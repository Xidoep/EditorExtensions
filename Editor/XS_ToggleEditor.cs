using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(XS_Toggle))]
[CanEditMultipleObjects]
public class XS_ToggleEditor : ToggleEditor
{
    SerializedProperty animacio;
    SerializedProperty recuadre;

    XS_Toggle t;

    private new void OnEnable()
    {
        animacio = serializedObject.FindProperty("animacio");
        recuadre = serializedObject.FindProperty("recuadre");
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (XS_Toggle)target;
        if (t.transition != Selectable.Transition.None) t.transition = Selectable.Transition.None;
        if (t.toggleTransition != Toggle.ToggleTransition.None) t.toggleTransition = Toggle.ToggleTransition.None;
        if (t.Recuadre == null) 
        {
            foreach (var item in t.GetComponentsInChildren<Image>())
            {
                if (item.name == "Background" || item.name == "Item Background")
                {
                    t.Recuadre = item;
                    break;
                }
            }
        }
        if (t.graphic == null)
        {
            foreach (var item in t.GetComponentsInChildren<Image>())
            {
                if(item.name == "Checkmark" || item.name == "Item Checkmark")
                {
                    t.graphic = item;
                }
            }
        }

        EditorGUILayout.PropertyField(animacio, new GUIContent("Animacio"));
        EditorGUILayout.PropertyField(recuadre, new GUIContent("Recuadre"));

        serializedObject.ApplyModifiedProperties();
    }

    [MenuItem("CONTEXT/Toggle/Convert to XS_Toggle", priority = 501)]
    static void To_XS_Toggle(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        DestroyImmediate((command.context));
        gameObject.AddComponent<XS_Toggle>();
    }
}
