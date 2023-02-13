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
    [SerializeField] new Image image;

    Coroutine coroutineLoop;

    public UnityEvent OnEnter => onEnter;
    public UnityEvent OnExit => onExit;

    public void Interactable(bool interactable) => this.interactable = interactable;

    
    
    protected override void OnEnable()
    {
        onClick.AddListener(PlayOnClick);
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        onClick.RemoveAllListeners();
        base.OnDisable();
    }

    void PlayOnClick() 
    {
        if (!animacio)
            return;

        animacio.PlayOnClick(image, ref coroutineLoop);
    } 
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!animacio)
            return;

        onEnter.Invoke();
        animacio.PlayOnEnter(image, ref coroutineLoop);
    }
    

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!animacio)
            return;

        onExit.Invoke();
        animacio.PlayOnExit(image, ref coroutineLoop);
    }

    private void Update()
    {
        if (XS_Input.OnPress(UnityEngine.InputSystem.Key.A)) animacio.PlayOnEnter(image, ref coroutineLoop);
        if (XS_Input.OnPress(UnityEngine.InputSystem.Key.S)) animacio.onEnter.Play(image);
    }
}
