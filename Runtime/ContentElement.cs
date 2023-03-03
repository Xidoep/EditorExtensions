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
    /*public void Setup(System.Action<RectTransform> seleccionar)
    {
        this.seleccionarRect = seleccionar;
    }*/

    //[SerializeField] RectTransform rectTransform;
    [SerializeField] XS_Button[] botons;
    [SerializeField] int index;
    System.Action<int> seleccionar;
    //System.Action<RectTransform> seleccionarRect;

    private void OnEnable()
    {
        for (int i = 0; i < botons.Length; i++)
        {
            botons[i].OnEnter += Seleccionar;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < botons.Length; i++)
        {
            botons[i].OnEnter -= Seleccionar;
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
