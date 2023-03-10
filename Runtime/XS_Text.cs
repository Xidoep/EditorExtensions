using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;


public class XS_Text : TextMeshProUGUI
{
    public AnimacioPerCodi_Text animacio;
    public XS_Button button;
    public XS_Slider slider;
    public bool percentatge;

    TMP_Text TMPtext;

    protected override void OnEnable()
    {
        TMPtext = GetComponent<TMP_Text>();

        if (button)
        {
            button.OnEnter += PlayOnEnter;
            button.OnExit += PlayOnExit;
        }
        if (slider)
        {
            slider.onValueChanged.AddListener(CanviarTexte);
        }
        base.OnEnable();
    }

    void CanviarTexte(float value)
    {
        if (!percentatge)
        {
            if (slider.wholeNumbers)
            {
                if (slider.maxValue >= 10)
                    text = value.ToString("00");
                else text = value.ToString("0");
            }
            else
            {
                if(slider.maxValue > 2)
                    text = value.ToString("0.0");
                else text = value.ToString("0.00");
            }
        }
        else
        {
            text = $"{(value * 100).ToString("##0")}%";
        }
    }

    protected override void OnDisable()
    {
        if (button)
        {
            button.OnEnter -= PlayOnEnter;
            button.OnExit -= PlayOnExit;
        }
        if (slider)
        {
            slider.onValueChanged.RemoveListener(CanviarTexte);
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
