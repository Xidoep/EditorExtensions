using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;


[CustomEditor(typeof(XS_Button))]
[CanEditMultipleObjects]
public class XS_ScrollRectEditor : ScrollRectEditor
{

    XS_ScrollRect t;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (XS_ScrollRect)target;

        if (t.content == null || t.viewport == null || t.horizontalScrollbar == null || t.verticalScrollbar == null)
        {
            foreach (var item in t.GetComponentsInChildren<RectTransform>())
            {
                if (item.name == "Content") t.content = item;
                if (item.name == "Viewport") t.viewport = item;
                //if (item.name == "Scrollbar Horizontal") t.horizontalScrollbar = item;
                //if (item.name == "Scrollbar Vertical") t.verticalScrollbar = item;
            }
        }


    }


    [MenuItem("CONTEXT/ScrollRect/Convert to XS_ScrollRect", priority = 501)]
    static void To_XS_ScrollRect(MenuCommand command)
    {
        GameObject gameObject = ((Component)command.context).gameObject;
        DestroyImmediate((command.context));
        gameObject.AddComponent<XS_ScrollRect>();
    }
}
