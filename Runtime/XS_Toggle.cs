using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;
using XS_Utils;

public class XS_Toggle : Toggle
{
    [SerializeField] AnimacioPerCodi_Toggle animacio;

    [SerializeField] Image recuadre;
    [SerializeField] System.Action onEnter;
    [SerializeField] System.Action onExit;

    [SerializeField] TMP_Text estat;
    [SerializeField] LocalizedString True;
    [SerializeField] LocalizedString False;

    [SerializeField] int desplaçamentLateral;

    [SerializeField] SavableVariable<float> variable;

    Coroutine corrutine;


    public Image Recuadre { get => recuadre; set => recuadre = value; }
    public System.Action OnEnter { get => onEnter; set => onEnter = value; }
    public System.Action OnExit { get => onExit; set => onExit = value; }

    protected override void OnEnable()
    {
        onValueChanged.AddListener(SetValue);
        isOn = variable.Valor > 0.1f;
        SetValue(isOn);
        base.OnEnable();
    }

    void SetValue(bool value)
    {
        if(estat != null)
        {
            if (value)
                True.WriteOn(estat);
            else False.WriteOn(estat);
        }

        transform.localPosition = Vector3.right * desplaçamentLateral * (value ? 1 : -1);

        variable.Valor = value ? 1 : 0;
    }



    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        corrutine = animacio?.OnEnter(recuadre);
        onEnter?.Invoke();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        corrutine = animacio?.OnEnter(recuadre);
        onEnter?.Invoke();
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
        onExit?.Invoke();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        corrutine = animacio?.OnExit(recuadre, corrutine);
        onExit?.Invoke();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onValueChanged.RemoveListener(SetValue);
    }
}
