using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using XS_Utils;

public class XS_Dropdown : TMP_Dropdown
{
    [SerializeField] AnimacioPerCodi_Boto animacio;

    [SerializeField] SavableVariable<int> variable;

    Coroutine corrutine;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        corrutine = animacio?.OnEnter(image);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        corrutine = animacio?.OnEnter(image);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        corrutine = animacio?.OnClick(image, corrutine);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        corrutine = animacio?.OnClick(image, corrutine);
        Debug.Log("hola");
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        corrutine = animacio?.OnExit(image, corrutine);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        corrutine = animacio?.OnExit(image, corrutine);
    }
    protected override void DestroyDropdownList(GameObject dropdownList)
    {
        dropdownList.GetComponent<AnimacioPerCodi_GameObject_Referencia>().Destroy();
        //base.DestroyDropdownList(dropdownList);
    }

    /*protected override void DestroyItem(DropdownItem item)
    {
        if (animacio)
            XS_Coroutine.StartCoroutine_Ending(0.75f, DestroyItemTime);
        else base.DestroyItem(item);

        void DestroyItemTime() => base.DestroyItem(item);
    }*/


}
