using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;


public class XS_Text : TextMeshProUGUI
{
    public AnimacioPerCodi_Text animacio;
    public XS_Button button;

    TMP_Text TMPtext;

    protected override void OnEnable()
    {
        TMPtext = GetComponent<TMP_Text>();

        if (button)
        {
            button.OnEnter.AddListener(PlayOnEnter);
            button.OnExit.AddListener(PlayOnExit);
        }
        base.OnEnable();
    }


    protected override void OnDisable()
    {
        if (button)
        {
            button.OnEnter.RemoveListener(PlayOnEnter);
            button.OnExit.RemoveListener(PlayOnExit);
        }
        base.OnDisable();
    }

    public void PlayOnEnter() 
    {
        if (!animacio)
            return;

        animacio.PlayOnEnter(TMPtext);
    }
    public void PlayOnExit() 
    {
        if (!animacio)
            return;

        animacio.PlayOnExit(TMPtext);
    } 


}
