using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentElement : MonoBehaviour
{
    public void Setup(int index, System.Action<int> seleccionar)
    {
        this.seleccionar = seleccionar;
        this.index = index;
    }
    [SerializeField] XS_Button[] botons;
    [SerializeField] XS_Toggle[] toggles;
    [SerializeField] int index;
    System.Action<int> seleccionar;

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

    void Seleccionar()
    {
        seleccionar?.Invoke(index);
        //seleccionarRect?.Invoke(rectTransform);
    }

    private void OnValidate()
    {
       // if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
    }
}
