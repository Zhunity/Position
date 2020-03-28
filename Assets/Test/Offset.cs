using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    RectTransform rect;
    RectTransform parentRect;

    private Vector3 position;
    private Vector3 localPosition;
    public Vector3 anchoredPosition;
    public Vector3 guessAnchoredPositionByMin;
    public Vector3 guessAnchoredPositionByMax;

    public Vector2 offsetMin;
    public Vector2 guessOffsetMin;

    public Vector2 offsetMax;
    public Vector2 guessOffsetMax;

    public Vector2 sizeDelta;
    public Vector2 guessSizeDelta;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    void Update()
    {
        position = transform.position;
        localPosition = rect.localPosition;
        anchoredPosition = rect.anchoredPosition;

        offsetMin = rect.offsetMin;

        Vector2 anchorMinPos = parentRect.rect.min + Vector2.Scale(rect.anchorMin, parentRect.rect.size);
        Vector2 rectMinPos = rect.rect.min + new Vector2(rect.localPosition.x , rect.localPosition.y);
        guessOffsetMin = rectMinPos - anchorMinPos;

        offsetMax = rect.offsetMax;
        Vector2 anchorMaxPos = parentRect.rect.max - Vector2.Scale(Vector2.one - rect.anchorMax, parentRect.rect.size);
        Vector2 rectMaxPos = rect.rect.max + new Vector2(rect.localPosition.x, rect.localPosition.y);
        guessOffsetMax = rectMaxPos - anchorMaxPos;

        sizeDelta = rect.sizeDelta;
        guessSizeDelta = guessOffsetMax - guessOffsetMin;

        guessAnchoredPositionByMin = offsetMin + Vector2.Scale(guessSizeDelta, rect.pivot);
        guessAnchoredPositionByMax = offsetMax - Vector2.Scale(guessSizeDelta, Vector2.one - rect.pivot);
    }

    //public Vector2 offsetMin
    //{
    //    get
    //    {
    //        return anchoredPosition - Vector2.Scale(sizeDelta, pivot);
    //    }
    //    set
    //    {
    //        Vector2 offset = value - (anchoredPosition - Vector2.Scale(sizeDelta, pivot));
    //        sizeDelta -= offset;
    //        anchoredPosition += Vector2.Scale(offset, Vector2.one - pivot);
    //    }
    //}

    //public Vector2 offsetMax
    //{
    //    get
    //    {
    //        return anchoredPosition + Vector2.Scale(sizeDelta, Vector2.one - pivot);
    //    }
    //    set
    //    {
    //        Vector2 offset = value - (anchoredPosition + Vector2.Scale(sizeDelta, Vector2.one - pivot));
    //        sizeDelta += offset;
    //        anchoredPosition += Vector2.Scale(offset, pivot);
    //    }
    //}
}
