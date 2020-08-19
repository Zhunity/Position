using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricTest : MonoBehaviour
{
	public RectTransform parent;
	public RectTransform self;
	public RectTransform anchor;
	public RectTransform sizeDelta;

	public Vector2 AnchorMax;
	public Vector2 AnchorMin;
	public Vector2 Pivot;

	private void Awake()
	{
		AnchorMax = self.anchorMax;
		AnchorMin = self.anchorMin;
		Pivot = self.pivot;
	}

	private void OnValidate()
	{
		//self.pivot = Pivot;
		//anchor.pivot = Pivot;
		SetPivotSmart(self, Pivot.x, 0, true, false);
		SetPivotSmart(self, Pivot.y, 1, true, false);

		SetPivotSmart(anchor, Pivot.x, 0, true, false);
		SetPivotSmart(anchor, Pivot.y, 1, true, false);
	}

	public Vector3 anchorPosition;
	public Vector3 reuslt;
    // Update is called once per frame
    void Update()
    {
		anchorPosition = self.anchoredPosition;
		reuslt = self.position- anchor.position;
	}


	public static void SetPivotSmart(RectTransform rect, float value, int axis, bool smart, bool parentSpace)
	{
		Vector3 cornerBefore = GetRectReferenceCorner(rect, !parentSpace);

		Vector2 rectPivot = rect.pivot;
		rectPivot[axis] = value;
		rect.pivot = rectPivot;

		if (smart)
		{
			Vector3 cornerAfter = GetRectReferenceCorner(rect, !parentSpace);
			Vector3 cornerOffset = cornerAfter - cornerBefore;
			rect.anchoredPosition -= (Vector2)cornerOffset;

			Vector3 pos = rect.transform.position;
			pos.z -= cornerOffset.z;
			rect.transform.position = pos;
		}
	}

	static Vector3 GetRectReferenceCorner(RectTransform gui, bool worldSpace)
	{
		if (worldSpace)
		{
			var s_Corners = new Vector3[4];
			Transform t = gui.transform;
			gui.GetWorldCorners(s_Corners);
			if (t.parent)
				return t.parent.InverseTransformPoint(s_Corners[0]);
			else
				return s_Corners[0];
		}
		return (Vector3)gui.rect.min + gui.transform.localPosition;
	}
}
