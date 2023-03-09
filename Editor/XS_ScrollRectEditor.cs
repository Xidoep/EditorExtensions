using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;


[CustomEditor(typeof(XS_ScrollRect))]
[CanEditMultipleObjects]
public class XS_ScrollRectEditor : ScrollRectEditor
{
    SerializedProperty gridLayoutGroup;
    SerializedProperty layoutGroup;
    SerializedProperty modeDesplaçament;

    SerializedProperty contingut;
    XS_ScrollRect t;
    private new void OnEnable()
    {
        gridLayoutGroup = serializedObject.FindProperty("gridLayoutGroup");
        layoutGroup = serializedObject.FindProperty("layoutGroup");
        modeDesplaçament = serializedObject.FindProperty("modeDesplaçament");
        contingut = serializedObject.FindProperty("contingut");
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (XS_ScrollRect)target;

        if (t.content == null || t.viewport == null)
        {
            foreach (var item in t.GetComponentsInChildren<RectTransform>())
            {
                if (item.name == "Content") t.content = item;
                if (item.name == "Viewport") t.viewport = item;
            }
        }
        if (t.horizontalScrollbar == null || t.verticalScrollbar == null)
        {
            foreach (var item in t.GetComponentsInChildren<Scrollbar>())
            {
                if (item.name == "Scrollbar Horizontal") t.horizontalScrollbar = item;
                if (item.name == "Scrollbar Vertical") t.verticalScrollbar = item;
            }
        }

        EditorGUILayout.PropertyField(gridLayoutGroup, new GUIContent("Grid Layout Group"));
        EditorGUILayout.PropertyField(layoutGroup, new GUIContent("Layout Group"));
        EditorGUILayout.PropertyField(modeDesplaçament, new GUIContent("Mode Desplaçament"));
        EditorGUILayout.Space(20);
        EditorGUILayout.PropertyField(contingut, new GUIContent("Contingut"));

        serializedObject.ApplyModifiedProperties();
    }


    [MenuItem("CONTEXT/ScrollRect/Convert to XS_ScrollRect", priority = 501)]
    static void To_XS_ScrollRect(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        bool h = ((ScrollRect)command.context).horizontal;
        bool v = ((ScrollRect)command.context).vertical;
        ScrollRect.MovementType m = ((ScrollRect)command.context).movementType;
        ScrollRect.ScrollbarVisibility hsv = ((ScrollRect)command.context).horizontalScrollbarVisibility;
        ScrollRect.ScrollbarVisibility vsv = ((ScrollRect)command.context).verticalScrollbarVisibility;
        float hss = ((ScrollRect)command.context).horizontalScrollbarSpacing;
        float vss = ((ScrollRect)command.context).verticalScrollbarSpacing;

        DestroyImmediate((command.context));

        XS_ScrollRect xssr = gameObject.AddComponent<XS_ScrollRect>();

        xssr.horizontal = h;
        xssr.vertical = v;
        xssr.movementType = m;
        xssr.horizontalScrollbarVisibility = hsv;
        xssr.verticalScrollbarVisibility = vsv;
        xssr.horizontalScrollbarSpacing = hss;
        xssr.verticalScrollbarSpacing = vss;

    }
}
