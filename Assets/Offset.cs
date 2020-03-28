using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    RectTransform rect;
    RectTransform parentRect;

    private Vector3 position;
    private Vector3 localPosition;

    public Vector2 offsetMin;
    public Vector2 guessOffsetMin;

    public Vector2 offsetMax;
    public Vector2 guessOffsetMax;

    // public Vector2 offsetMax;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    void Update()
    {
        position = transform.position;
        localPosition = rect.localPosition;

        offsetMin = rect.offsetMin;

        Vector2 anchorMinPos = parentRect.rect.min + Vector2.Scale(rect.anchorMin, parentRect.rect.size);
        Vector2 rectMinPos = rect.rect.min + new Vector2(rect.localPosition.x , rect.localPosition.y);
        guessOffsetMin = rectMinPos - anchorMinPos;

        offsetMax = rect.offsetMax;
        Vector2 anchorMaxPos = parentRect.rect.max - Vector2.Scale(Vector2.one - rect.anchorMax, parentRect.rect.size);
        Vector2 rectMaxPos = rect.rect.max + new Vector2(rect.localPosition.x, rect.localPosition.y);
        guessOffsetMax = rectMaxPos - anchorMaxPos;
    }
}
