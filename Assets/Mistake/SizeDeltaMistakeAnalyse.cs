﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDeltaMistakeAnalyse : MistakeAnalyse
{

    // Update is called once per frame
    void Update()
    {
		origin = rect.sizeDelta;
		result = GetSizeDeltaByLocalPosition();

	}

	Vector2 GetSizeDeltaByLocalPosition()
	{
		// 通过OffsetMin、OffsetMax，将anchoredPosition和localPosition联系起来
		Vector2 localPosition2D = new Vector2(rect.localPosition.x, rect.localPosition.y);
		Vector2 anchorMinPos = parentRect.rect.min + Vector2.Scale(rect.anchorMin, parentRect.rect.size);
		Vector2 rectMinPos = rect.rect.min + localPosition2D;
		Vector2 offsetMin = rectMinPos - anchorMinPos;

		Vector2 anchorMaxPos = parentRect.rect.max - Vector2.Scale(Vector2.one - rect.anchorMax, parentRect.rect.size);
		Vector2 rectMaxPos = rect.rect.max + localPosition2D;
		Vector2 offsetMax = rectMaxPos - anchorMaxPos;

		Vector2 sizeDelta = offsetMax - offsetMin;
		return sizeDelta;
	}
}
