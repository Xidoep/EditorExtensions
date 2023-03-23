using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentElement : MonoBehaviour
{
    public void Setup(int index, System.Action<int> seleccionar, System.Action<RectTransform> onDestroy)
    {
        this.index = index;
        this.seleccionar = seleccionar;
        this.onDestroy = onDestroy;
    }
    [SerializeField] RectTransform rectTransform;
    [SerializeField] XS_Button[] botons;
    [SerializeField] XS_Toggle[] toggles;

    [SerializeField] int index;
    System.Action<int> seleccionar;
    System.Action<RectTransform> onDestroy;

    private void OnEnable()
    {
        for (int i = 0; i < botons.Length; i++)
        {
            botons[i].OnEnter += Seleccionar;
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].OnEnter += Seleccionar;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < botons.Length; i++)
        {
            botons[i].OnEnter -= Seleccionar;
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].OnEnter -= Seleccionar;
        }
    }

    private void OnDestroy()
    {
        onDestroy.Invoke(rectTransform);
    }

    void Seleccionar()
    {
        seleccionar?.Invoke(index);
        //seleccionarRect?.Invoke(rectTransform);
    }

    private void OnValidate()
    {
       if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
    }
}
