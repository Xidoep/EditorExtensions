using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XS_Utils;


public class XS_Button : Button
{
    [SerializeField] Component component;
    [SerializeField] AnimacioPerCodi_Boto animacio;
    [SerializeField] System.Action onEnter;
    [SerializeField] System.Action onExit;

    Coroutine coroutineLoop;
    bool destroyingOrdisabling;

    public System.Action OnEnter { get => onEnter; set => onEnter = value; }
    public System.Action OnExit { get => onExit; set => onExit = value; }

    public void Interactable(bool interactable) => this.interactable = interactable;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (component == null) component = image;

        if (animacio) animacio.EnEnable(component);
    }


    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (!interactable)
            return;

        if (animacio) coroutineLoop = animacio.OnEnter(component);
        onEnter?.Invoke();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!interactable)
            return;

        if (animacio) coroutineLoop = animacio.OnEnter(component);
        onEnter?.Invoke();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!interactable)
            return;

        if (animacio) coroutineLoop = animacio.OnClick(component, coroutineLoop);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        if (!interactable)
            return;

        if (animacio) coroutineLoop = animacio.OnClick(component, coroutineLoop);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        if (!interactable)
            return;

        if (animacio) coroutineLoop = animacio.OnExit(component, coroutineLoop);
        onExit?.Invoke();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!interactable)
            return;

        if (animacio) coroutineLoop = animacio.OnExit(component, coroutineLoop);
        onExit?.Invoke();
    }


    public void Destroy() => animacio.Destroy(component, coroutineLoop);
    public void Disable() => animacio.Disable(component, true);


}
