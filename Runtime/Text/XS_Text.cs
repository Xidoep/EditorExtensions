using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;


public class XS_Text : TextMeshProUGUI
{
    public AnimacioPerCodi_Text animacio;
    public XS_Button button;

    TMP_Text text;

    protected override void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        button.onEnter.AddListener(PlayOnEnter);
        button.onExit.AddListener(PlayOnExit);
        base.OnEnable();
    }


    protected override void OnDisable()
    {
        button.onEnter.RemoveListener(PlayOnEnter);
        button.onExit.RemoveListener(PlayOnExit);
        base.OnDisable();
    }

    public void PlayOnEnter() => animacio.PlayOnEnter(text);
    public void PlayOnExit() => animacio.PlayOnExit(text);


}
