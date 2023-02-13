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
        button.OnEnter.AddListener(PlayOnEnter);
        button.OnExit.AddListener(PlayOnExit);
        base.OnEnable();
    }


    protected override void OnDisable()
    {
        button.OnEnter.RemoveListener(PlayOnEnter);
        button.OnExit.RemoveListener(PlayOnExit);
        base.OnDisable();
    }

    public void PlayOnEnter() 
    {
        if (!animacio)
            return;

        animacio.PlayOnEnter(text);
    }
    public void PlayOnExit() 
    {
        if (!animacio)
            return;

        animacio.PlayOnExit(text);
    } 


}
