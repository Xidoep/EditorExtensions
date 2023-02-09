using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XS_Utils;


public class XS_Button : Button
{
    public AnimacioPerCodi_Boto animacio;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    Image image;
    Coroutine coroutineLoop;
    WaitForSeconds waitForSeconds;


    public void Interactable(bool interactable) => this.interactable = interactable;

    

    protected override void OnEnable()
    {
        image = GetComponent<Image>();

        if(animacio != null)
            waitForSeconds = new WaitForSeconds(animacio.OnEnter.Temps);

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
        StopLoop();
        animacio.PlayOnClick(image);
    } 
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onEnter.Invoke();
        animacio.PlayOnEnter(image);
        coroutineLoop = StartCoroutine(StartLoop());
    }
    

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onExit.Invoke();
        StopLoop();
        animacio.PlayOnExit(image);
    }

    IEnumerator StartLoop()
    {
        yield return waitForSeconds;
        animacio.PlayLoop(image);
    }
    void StopLoop()
    {
        if (coroutineLoop != null)
        {
            StopCoroutine(coroutineLoop);
            coroutineLoop = null;
        }
    }
}
