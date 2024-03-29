using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XS_Slider : Slider
{
    [SerializeField] AnimacioPerCodi_Slider animacio;
    [SerializeField] So so;

    [SerializeField] SavableVariable<float> variable;
    /*[SerializeField] Guardat guardat;
    [SerializeField] string key;
    [SerializeField] bool local;
    [SerializeField] float perDefecte;*/

    SoControlador controlador;
    Coroutine coroutine;

    public void Interactable(bool interactable) => this.interactable = interactable;

    protected override void OnEnable()
    {
        value = variable.Valor;
        base.OnEnable();
    }


    public void Modificar(float value) => SetValue(this.value + value);
    public void Minim() => SetValue(minValue);
    public void ResetDefault() => SetValue(variable.Reset());
    public void Mute(bool mute)
    {
        if (mute) Minim();
        else ResetDefault();
    }
    void SetValue(float value)
    {
        this.value = Mathf.Clamp(value, minValue, maxValue);
        variable.Valor = this.value;
        So(false);
        onValueChanged.Invoke(this.value);

        if (animacio) animacio.Modificar(image);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (animacio) coroutine = animacio.OnEnter(image);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (animacio) coroutine = animacio.OnEnter(image);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        variable.Valor = value;
        So(true);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (animacio) coroutine = animacio.OnDown(image, coroutine);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (animacio) coroutine = animacio.OnUp(image);
        variable.Valor = value;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        if (animacio) coroutine = animacio.OnExit(image, coroutine);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (animacio) coroutine = animacio.OnExit(image, coroutine);
        variable.Valor = value;
    }
    void So(bool limitarRepeticions)
    {
        if (so != null)
        {
            if (limitarRepeticions)
            {
                if (controlador == null || !controlador.IsPlaying)
                    controlador = so.Play(1, Pitch);
            }
            else
                controlador = so.Play(1, Pitch);
        }
    }

    float Pitch => 0.5f + (value - minValue) / (maxValue - minValue);


}
