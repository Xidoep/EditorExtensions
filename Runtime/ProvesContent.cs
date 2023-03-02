using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvesContent : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] RectTransform view;
    [SerializeField] RectTransform content;
    [SerializeField] Vector2 viewSize;
    [SerializeField] Vector2 contentSize;
    [SerializeField] Vector2 contentPosition;

    [Space(20)]
    [SerializeField] List<RectTransform> elements;
    [SerializeField] List<float> posicions;
    [SerializeField] List<float> factors;
    [SerializeField] List<float> factorsFromView;
    [SerializeField] List<bool> visibles;
    [SerializeField][Range(0,35)] int indexSeleccionat;
    [SerializeField] float factorAnterior;
    [SerializeField] float factorActual;
    [SerializeField] bool posicionar;

    float FactorActual
    {
        set
        {
            factorAnterior = factorActual;
            factorActual = value;
        }
    }


    [ContextMenu("Provar")]
    public void Provar()
    {
        viewSize = view.rect.size;
        contentSize = content.rect.size;

        elements = new List<RectTransform>();
        posicions = new List<float>();
        factors = new List<float>();
        factorsFromView = new List<float>();
        visibles = new List<bool>();

        for (int i = 0; i < content.childCount; i++)
        {
            elements.Add((RectTransform)content.GetChild(i));
            float factor = ((RectTransform)content.GetChild(i)).localPosition.x / (contentSize.x);
            posicions.Add(((RectTransform)content.GetChild(i)).localPosition.x - (gridLayoutGroup.cellSize.x / 2f) + ((gridLayoutGroup.cellSize.x) * factor)) ;
            factors.Add(posicions[i] / (contentSize.x));
            factorsFromView.Add((((RectTransform)content.GetChild(i)).localPosition.x + contentPosition.x) / (viewSize.x));
            visibles.Add(factorsFromView[i] == Mathf.Clamp(factorsFromView[i], 0.1f, 0.9f));
        }

    }

    void ActualitzarVisibles()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            factorsFromView[i] = (elements[i].localPosition.x + contentPosition.x) / (viewSize.x);
            visibles[i] = factorsFromView[i] == Mathf.Clamp(factorsFromView[i], 0.1f, 0.9f);
        }
        posicionar = !visibles[indexSeleccionat];
    }

    [ContextMenu("Posicionar")]
    public void Posicionar()
    {
        posicionar = !visibles[indexSeleccionat];
        if (visibles[indexSeleccionat])
        {
            //FactorActual = factors[indexSeleccionat];
        }
        else
        {
            //scrollRect.horizontalNormalizedPosition = Mathf.Lerp(factorActual, factors[indexSeleccionat], 0.001f);
            //FactorActual = factors[indexSeleccionat];
            //ActualitzarVisibles();
        }
        
    }

    private void Update()
    {
        contentPosition = content.anchoredPosition;

        if (posicionar && Mathf.Abs(factorActual - factors[indexSeleccionat]) > 0.01f)
        {
            factorActual = Mathf.Lerp(factorActual, factors[indexSeleccionat], Time.unscaledDeltaTime * 2);
            scrollRect.horizontalNormalizedPosition = factorActual;
            ActualitzarVisibles();
        }
        /*for (int i = 0; i < length; i++)
        {
            factors.Add(((RectTransform)content.GetChild(i)).localPosition.x / (contentSize.x));
        }*/
    }


    private void OnValidate()
    {
        Provar();
        if(Application.isPlaying)
            Posicionar();
    }

    [System.Serializable]
    public struct Element
    {

    }
}
