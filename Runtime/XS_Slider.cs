using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XS_Slider : Slider
{
    [SerializeField] AnimacioPerCodi_Slider animacio;
    [SerializeField] So so;

    [SerializeField] Guardat guardat;
    [SerializeField] string key;
    [SerializeField] bool local;
    [SerializeField] float perDefecte;

    SoControlador controlador;

    public void Interactable(bool interactable) => this.interactable = interactable;

    protected override void OnEnable()
    {
        if (guardat != null) value = (float)guardat.Get(key, perDefecte);
        else value = perDefecte;
        base.OnEnable();
    }


    public void Modificar(float value) => SetValue(this.value + value);
    public void Minim() => SetValue(minValue);
    public void ResetDefault() => SetValue(perDefecte);
    public void Mute(bool mute)
    {
        if (mute) Minim();
        else ResetDefault();
    }
    void SetValue(float value)
    {
        this.value = value;
        Guardar();
        So(false);
        onValueChanged.Invoke(this.value);

        animacio?.PlayModificar(image);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        animacio?.PlayOnEnter(image);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        animacio?.PlayOnEnter(image);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Guardar();
        So(true);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        animacio?.PlayOnDown(image);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        animacio?.PlayOnUp(image);
        Guardar();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        animacio.PlayOnExit(image);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        animacio.PlayOnExit(image);
        Guardar();
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

    void Guardar()
    {
        if (guardat != null) guardat.Set(key, value, local);
    }

}
