using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XS_Toggle : Toggle
{
    [SerializeField] AnimacioPerCodi_Toggle animacio;
    [SerializeField] Image recuadre;
    Coroutine corrutine;

    public Image Recuadre { get => recuadre; set => recuadre = value; }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        corrutine = animacio?.OnEnter(recuadre);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        corrutine = animacio?.OnEnter(recuadre);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        corrutine = animacio?.OnClick((Image)graphic, corrutine);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        corrutine = animacio?.OnClick((Image)graphic, corrutine);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        corrutine = animacio?.OnExit(recuadre, corrutine);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        corrutine = animacio?.OnExit(recuadre, corrutine);
    }
}
