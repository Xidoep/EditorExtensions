using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class ProvesContent : MonoBehaviour
{


    [SerializeField] bool setuped = false;
    [SerializeField] List<Element> contingut;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] ContentSizeFitter contentSizeFitter;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] RectTransform view;
    [SerializeField] RectTransform content;
    //[SerializeField] Vector2 viewSize;
    //[SerializeField] Vector2 contentSize;
    //[SerializeField] Vector2 contentPosition;

    [Space(20)]
    [SerializeField] List<RectTransform> elements;
    [SerializeField] List<Vector2> posicionsV2;
    [SerializeField] List<Vector2> factorsv2;
    [SerializeField] List<Vector2> factorsFromViewv2;
    [SerializeField] List<bool> visiblesX;
    [SerializeField] List<bool> visiblesY;

    [SerializeField][Range(0,35)] int indexSeleccionat;
    [SerializeField] float factorAnterior;
    [SerializeField] float factorActual;
    [SerializeField] Vector2 factorActualv2;
    [SerializeField] bool posicionar;
    [SerializeField] bool posicionarContingut;
    [SerializeField] bool vertical;
    [SerializeField] bool horitzontal;

    public void Seleccionar(RectTransform seleccionat) => indexSeleccionat = elements.IndexOf(seleccionat);
    public void Seleccionar(int seleccionat) 
    { 
        indexSeleccionat = seleccionat;
        Posicionar();
    } 

    private void OnEnable()
    {
        indexSeleccionat = 0;
        //Provar();
    }

    public void Add(GameObject element)
    {
        if (contingut == null) contingut = new List<Element>();
        if (contentSizeFitter) 
        {
            contentSizeFitter.SetLayoutVertical();
            contentSizeFitter.SetLayoutHorizontal();
        }
        Debug.Log(content.rect.size);
        contingut.Add(new Element(element.GetComponent<RectTransform>(), content.rect.size, content.anchoredPosition, view.rect.size, gridLayoutGroup.cellSize));
        element.GetComponent<ContentElement>().Setup(contingut.Count - 1, Seleccionar, null);

        //ActualitzarVisibles();
    }

    [ContextMenu("Provar")]
    public void Provar()
    {
        StartCoroutine(ProvarTemps());

        return;

        Debug.Log(content.rect.size);
        // = view.rect.size;
        //content.rect.size = content.rect.size;

        elements = new List<RectTransform>();
        posicionsV2 = new List<Vector2>();
        factorsv2 = new List<Vector2>();
        factorsFromViewv2 = new List<Vector2>();
        visiblesX = new List<bool>();
        visiblesY = new List<bool>();

        contingut = new List<Element>();
        for (int i = 0; i < content.childCount; i++)
        {
            contingut.Add(new Element((RectTransform)content.GetChild(i), content.rect.size, content.anchoredPosition, view.rect.size, gridLayoutGroup.cellSize));

            content.GetChild(i).GetComponent<ContentElement>().Setup(i, Seleccionar, null);
        }


        for (int i = 0; i < content.childCount; i++)
        {
            elements.Add((RectTransform)content.GetChild(i));
            float factor = ((RectTransform)content.GetChild(i)).localPosition.x / (content.rect.size.x);
            Vector2 factorv2 = ((RectTransform)content.GetChild(i)).anchoredPosition / (content.rect.size);
            posicionsV2.Add(((Vector2)((RectTransform)content.GetChild(i)).anchoredPosition) - (gridLayoutGroup.cellSize / 2f) + ((gridLayoutGroup.cellSize) * factorv2)) ;
            factorsv2.Add(posicionsV2[i] / content.rect.size);

            factorsFromViewv2.Add((elements[i].anchoredPosition + content.anchoredPosition) / (view.rect.size));
            visiblesX.Add(factorsFromViewv2[i].x == Mathf.Clamp(factorsFromViewv2[i].x, 0.1f, 0.9f));
            visiblesY.Add(factorsFromViewv2[i].y == Mathf.Clamp(factorsFromViewv2[i].y, 0.1f, 0.9f));

            //factorsFromView.Add((((RectTransform)content.GetChild(i)).localPosition.x + contentPosition.x) / (viewSize.x));
            //visibles.Add(factorsFromView[i] == Mathf.Clamp(factorsFromView[i], 0.1f, 0.9f));
        }
        ActualitzarVisibles();
    }

    IEnumerator ProvarTemps()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log(content.rect.size);
        // = view.rect.size;
        //content.rect.size = content.rect.size;

        elements = new List<RectTransform>();
        posicionsV2 = new List<Vector2>();
        factorsv2 = new List<Vector2>();
        factorsFromViewv2 = new List<Vector2>();
        visiblesX = new List<bool>();
        visiblesY = new List<bool>();

        contingut = new List<Element>();
        for (int i = 0; i < content.childCount; i++)
        {
            contingut.Add(new Element((RectTransform)content.GetChild(i), content.rect.size, content.anchoredPosition, view.rect.size, gridLayoutGroup.cellSize));

            content.GetChild(i).GetComponent<ContentElement>().Setup(i, Seleccionar, null);
        }


        for (int i = 0; i < content.childCount; i++)
        {
            elements.Add((RectTransform)content.GetChild(i));
            float factor = ((RectTransform)content.GetChild(i)).localPosition.x / (content.rect.size.x);
            Vector2 factorv2 = ((RectTransform)content.GetChild(i)).anchoredPosition / (content.rect.size);
            posicionsV2.Add(((Vector2)((RectTransform)content.GetChild(i)).anchoredPosition) - (gridLayoutGroup.cellSize / 2f) + ((gridLayoutGroup.cellSize) * factorv2));
            factorsv2.Add(posicionsV2[i] / content.rect.size);

            factorsFromViewv2.Add((elements[i].anchoredPosition + content.anchoredPosition) / (view.rect.size));
            visiblesX.Add(factorsFromViewv2[i].x == Mathf.Clamp(factorsFromViewv2[i].x, 0.1f, 0.9f));
            visiblesY.Add(factorsFromViewv2[i].y == Mathf.Clamp(factorsFromViewv2[i].y, 0.1f, 0.9f));

            //factorsFromView.Add((((RectTransform)content.GetChild(i)).localPosition.x + contentPosition.x) / (viewSize.x));
            //visibles.Add(factorsFromView[i] == Mathf.Clamp(factorsFromView[i], 0.1f, 0.9f));
        }
        ActualitzarVisibles();

        setuped = true;
    }

    void ActualitzarVisibles()
    {
        for (int i = 0; i < contingut.Count; i++)
        {
            contingut[i].ActualitzarVisible(content.anchoredPosition, view.rect.size);
        }


        if (factorsFromViewv2 == null) factorsFromViewv2 = new List<Vector2>();
        if (visiblesX == null) visiblesX = new List<bool>();
        if (visiblesY == null) visiblesY = new List<bool>();

        for (int i = 0; i < elements.Count; i++)
        {
            factorsFromViewv2[i] = (elements[i].anchoredPosition + content.anchoredPosition) / (view.rect.size);
            if (horitzontal) visiblesX[i] = factorsFromViewv2[i].x == Mathf.Clamp(factorsFromViewv2[i].x, 0.1f, 0.9f);
            if (vertical) visiblesY[i] = factorsFromViewv2[i].y == Mathf.Clamp(factorsFromViewv2[i].y, 0.1f, 0.9f);
        }
        Posicionar();
    }

    [ContextMenu("Posicionar")]
    public void Posicionar()
    {
        //posicionar = (horitzontal ? !visiblesX[indexSeleccionat] : false) || (vertical ? !visiblesY[indexSeleccionat] : false);
        posicionarContingut = contingut[indexSeleccionat].Visible(horitzontal, vertical);
    }

    private void Update()
    {
        if (!setuped)
            return;

        //contentPosition = content.anchoredPosition;

        if (posicionar || posicionarContingut)
        {
            //factorActualv2 = Vector2.Lerp(factorActualv2, factorsv2[indexSeleccionat], Time.unscaledDeltaTime * 2);
            factorActualv2 = Vector2.Lerp(factorActualv2, contingut[indexSeleccionat].Factor, Time.unscaledDeltaTime * 2);
            if (horitzontal) scrollRect.horizontalNormalizedPosition = factorActualv2.x;
            if (vertical) scrollRect.verticalNormalizedPosition = factorActualv2.y;

            ActualitzarVisibles();
        }

    }


    private void OnValidate()
    {
        //Provar();
        if(Application.isPlaying)
            Posicionar();
    }

    [System.Serializable]
    public class Element
    {
        public Element(RectTransform rectTransform, Vector2 contentSize, Vector2 contentPosition, Vector2 viewSize, Vector2 cellSize)
        {
            this.rectTransform = rectTransform;
            this.factor = rectTransform.anchoredPosition / contentSize;
            posicio = rectTransform.anchoredPosition - (cellSize / 2f) + (cellSize * this.factor);
            this.factor = posicio / contentSize;

            ActualitzarVisible(contentPosition, viewSize);
        }

        [SerializeField] RectTransform rectTransform;
        [SerializeField] Vector2 posicio;
        [SerializeField] Vector2 factor;
        [SerializeField] Vector2 factorVisible;
        [SerializeField] bool visibleX;
        [SerializeField] bool visibleY;

        public void ActualitzarVisible(Vector2 contentPosition, Vector2 viewSize)
        {
            factorVisible = ((rectTransform.anchoredPosition + contentPosition) / (viewSize));
            visibleX = factorVisible.x == Mathf.Clamp(factorVisible.x, 0.1f, 0.9f);
            visibleY = factorVisible.y == Mathf.Clamp(factorVisible.y, 0.1f, 0.9f);
        }

        public bool Visible(bool horitzontal, bool vertical) => (horitzontal ? !visibleX : false) || (vertical ? !visibleY : false);

        public Vector2 Factor => factor;
    }
}
