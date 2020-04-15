using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectMinMistakeAnalyse : MistakeAnalyse
{

	public Vector3[] fourCornersArray = new Vector3[4];

	// Update is called once per frame
	void Update()
    {
		rect.GetLocalCorners(fourCornersArray);
		for(int i = 0; i < 4; i ++ )
		{
			fourCornersArray[i] += rect.localPosition;
		}
		origin = fourCornersArray[0];
		result = GetRectMinPosByLocalPosition();

	}

	Vector2 GetRectMinPosByLocalPosition()
	{
		Vector2 localPosition2D = new Vector2(rect.localPosition.x, rect.localPosition.y);
		Vector2 rectMinPos = rect.rect.min + localPosition2D;

		return rectMinPos;
	}
}
