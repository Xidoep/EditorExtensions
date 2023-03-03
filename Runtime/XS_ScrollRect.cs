using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XS_ScrollRect : ScrollRect
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;

    //INTERN
    [SerializeField] bool preparat = false;
    [SerializeField] List<Element> contingut;

    [SerializeField] bool posicionar;
    [SerializeField] [Range(0, 35)] int indexSeleccionat;
    [SerializeField] Vector2 posicio;

    public void Seleccionar(int seleccionat)
    {
        indexSeleccionat = seleccionat;
        Posicionar();
    }

    public void Iniciar() => StartCoroutine(IniciarTemps());
    IEnumerator IniciarTemps()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log(content.rect.size);

        contingut = new List<Element>();
        for (int i = 0; i < content.childCount; i++)
        {
            contingut.Add(new Element((RectTransform)content.GetChild(i), content.rect.size, content.anchoredPosition, viewport.rect.size, gridLayoutGroup.cellSize));

            content.GetChild(i).GetComponent<ContentElement>().Setup(i, Seleccionar);
        }
        ActualitzarVisibles();

        preparat = true;
    }
    void ActualitzarVisibles()
    {
        for (int i = 0; i < contingut.Count; i++)
        {
            contingut[i].ActualitzarVisible(content.anchoredPosition, viewport.rect.size);
        }

        Posicionar();
    }

    void Posicionar() => posicionar = contingut[indexSeleccionat].Visible(horizontal, vertical);

    protected override void LateUpdate()
    {
        if (!preparat)
            return;

        if (posicionar)
        {
            posicio = Vector2.Lerp(posicio, contingut[indexSeleccionat].Factor, Time.unscaledDeltaTime * 2);
            if (horizontal) horizontalNormalizedPosition = posicio.x;
            if (vertical) verticalNormalizedPosition = posicio.y;

            ActualitzarVisibles();
        }

        base.LateUpdate();
    }
    /*private void Update()
    {
        if (!preparat)
            return;

        if (posicionar)
        {
            posicio = Vector2.Lerp(posicio, contingut[indexSeleccionat].Factor, Time.unscaledDeltaTime * 2);
            if (horizontal) horizontalNormalizedPosition = posicio.x;
            if (vertical) verticalNormalizedPosition = posicio.y;

            ActualitzarVisibles();
        }

    }*/






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
