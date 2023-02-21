using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XS_Utils;


public class XS_Button : Button
{
    [SerializeField] AnimacioPerCodi_Boto animacio;
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onExit;

    Coroutine coroutineLoop;

    public UnityEvent OnEnter => onEnter;
    public UnityEvent OnExit => onExit;

    public void Interactable(bool interactable) => this.interactable = interactable;



    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (animacio) coroutineLoop = animacio.OnEnter(image);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (animacio) coroutineLoop = animacio.OnEnter(image);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (animacio) coroutineLoop = animacio.OnClick(image, coroutineLoop);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        if (animacio) coroutineLoop = animacio.OnClick(image, coroutineLoop);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        if (animacio) coroutineLoop = animacio.OnExit(image, coroutineLoop);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (animacio) coroutineLoop = animacio.OnExit(image, coroutineLoop);
    }

}
