using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnchoredPosition : MonoBehaviour
{
    RectTransform rect;
    RectTransform parentRect;

    public Vector2 anchoredPosition;
    public Vector2 result;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        anchoredPosition = rect.anchoredPosition;
        result = GetAnchoredPositionByLocalPosition();
    }

    Vector2 GetAnchoredPositionByLocalPosition()
    {
        // 通过OffsetMin、OffsetMax，将anchoredPosition和localPosition联系起来
        Vector2 localPosition2D = new Vector2(rect.localPosition.x, rect.localPosition.y);
        Vector2 anchorMinPos = GetAnchorLocal(parentRect, rect.anchorMin);
        Vector2 rectMinPos = rect.rect.min + localPosition2D;
        Vector2 offsetMin = rectMinPos - anchorMinPos;

        Vector2 anchorMaxPos = GetAnchorLocal(parentRect, rect.anchorMax); 
        Vector2 rectMaxPos = rect.rect.max + localPosition2D;
        Vector2 offsetMax = rectMaxPos - anchorMaxPos;

        Vector2 sizeDelta = offsetMax - offsetMin;

        Vector2 anchoredPosition = offsetMin + Vector2.Scale(sizeDelta, rect.pivot); ;
		/*
					offsetMax			* pivot	+			offsetMin			* (1 - pivot)
			(rectMaxPos - anchorMaxPos) * pivot + (rectMinPos - anchorMinPos)	* (1 - pivot)
			rectMaxPos * pivot - anchorMaxPos * pivot + rectMinPos * (1 - pivot) - anchorMinPos * (1 - pivot)
			rectMaxPos * pivot - anchorMaxPos * pivot + rectMinPos - rectMinPos * pivot - anchorMinPos + anchorMinPos * pivot
			rectMaxPos * pivot - rectMinPos * pivot - anchorMaxPos * pivot + anchorMinPos * pivot + rectMinPos - anchorMinPos
			rectSize * pivot - anchorSize * pivot + rectMinPos - anchorMinPos
			(rectMinPos + rectSize * pivot) - anchorSize * pivot - anchorMinPos
			...
			localPosition - (anchorMinPos + anchorSize * pivot)
			localPosition - anchor.position
		*/
		return anchoredPosition;
    }

	Vector3 GetAnchorLocal(RectTransform guiParent, Vector2 anchor)
	{
		return NormalizedToPointUnclamped(guiParent.rect, anchor);
	}

	static Vector2 NormalizedToPointUnclamped(Rect rectangle, Vector2 normalizedRectCoordinates)
	{
		return new Vector2(
			Mathf.LerpUnclamped(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x),
			Mathf.LerpUnclamped(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y)
		);
	}
}
