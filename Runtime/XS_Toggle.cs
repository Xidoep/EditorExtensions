using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XS_Toggle : Toggle
{
    [SerializeField] AnimacioPerCodi_Toggle animacio;

    [SerializeField] Image recuadre;
    public Image Recuadre { get => recuadre; set => recuadre = value; }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        animacio?.onEnter?.Play(recuadre);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        animacio?.onEnter?.Play(recuadre);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        animacio?.onClick?.Play((Image)graphic);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        animacio?.onClick?.Play((Image)graphic);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        animacio?.onExit?.Play(recuadre);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        animacio?.onExit?.Play(recuadre);
    }
}
