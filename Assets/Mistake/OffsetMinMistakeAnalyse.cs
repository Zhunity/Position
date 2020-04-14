using Mathd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetMinMistakeAnalyse : MistakeAnalyse
{
	[SerializeField]
	public Vector2d result2d;

    // Update is called once per frame
    void Update()
    {
		origin = rect.offsetMin;
		result = GetOffsetMinByLocalPosition();
		result2d = GetOffsetMin2dByLocalPosition();
	}

	Vector2 GetOffsetMinByLocalPosition()
	{
		// 通过OffsetMin、OffsetMax，将anchoredPosition和localPosition联系起来
		Vector2 localPosition2D = new Vector2(rect.localPosition.x, rect.localPosition.y);
		Vector2 anchorMinPos = parentRect.rect.min + Vector2.Scale(rect.anchorMin, parentRect.rect.size);
		Vector2 rectMinPos = rect.rect.min + localPosition2D;
		Vector2 offsetMin = rectMinPos - anchorMinPos;

		return offsetMin;
	}

	Vector2d GetOffsetMin2dByLocalPosition()
	{
		// 通过OffsetMin、OffsetMax，将anchoredPosition和localPosition联系起来
		Vector2d localPosition2D = new Vector2d(rect.localPosition.x, rect.localPosition.y);
		Vector2d anchorMinPos = new Vector2d(parentRect.rect.min) + Vector2d.Scale(new Vector2d(rect.anchorMin), new Vector2d(parentRect.rect.size));
		Vector2d rectMinPos = new Vector2d(rect.rect.min) + localPosition2D;
		Vector2d offsetMin = rectMinPos - anchorMinPos;

		return offsetMin;
	}
}
