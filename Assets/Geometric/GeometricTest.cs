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
		SetPivotSmart(self, Pivot.x, 0, true, false);
		SetPivotSmart(self, Pivot.y, 1, true, false);

		SetPivotSmart(anchor, Pivot.x, 0, true, false);
		SetPivotSmart(anchor, Pivot.y, 1, true, false);

		SetAnchorSmart(self, AnchorMin.x, 0, false, true, true, false, false);
		SetAnchorSmart(self, AnchorMin.y, 1, false, true, true, false, false);
		SetAnchorSmart(self, AnchorMax.x, 0, true, true, true, false, false);
		SetAnchorSmart(self, AnchorMax.y, 1, true, true, true, false, false);
	}

	public Vector3 anchorPosition;
	public Vector3 reuslt;
    // Update is called once per frame
    void Update()
    {
		anchorPosition = self.anchoredPosition;
		reuslt = self.position- anchor.position;
	}

	#region Pivot
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
	#endregion

	#region Anchor
	public static void SetAnchorSmart(RectTransform rect, float value, int axis, bool isMax, bool smart, bool enforceExactValue, bool enforceMinNoLargerThanMax, bool moveTogether)
	{
		RectTransform parent = null;
		if (rect.transform.parent == null)
		{
			smart = false;
		}
		else
		{
			parent = rect.transform.parent.GetComponent<RectTransform>();
			if (parent == null)
				smart = false;
		}

		bool clampToParent = !AnchorAllowedOutsideParent(axis, isMax ? 1 : 0);
		if (clampToParent)
			value = Mathf.Clamp01(value);
		if (enforceMinNoLargerThanMax)
		{
			if (isMax)
				value = Mathf.Max(value, rect.anchorMin[axis]);
			else
				value = Mathf.Min(value, rect.anchorMax[axis]);
		}

		float offsetSizePixels = 0;
		float offsetPositionPixels = 0;
		if (smart)
		{
			float oldValue = isMax ? rect.anchorMax[axis] : rect.anchorMin[axis];

			offsetSizePixels = (value - oldValue) * parent.rect.size[axis];

			// Ensure offset is in whole pixels.
			// Note: In this particular instance we want to use Mathf.Round (which rounds towards nearest even number)
			// instead of Round from this class which always rounds down.
			// This makes the position of rect more stable when their anchors are changed.
			float roundingDelta = 0;
			if (ShouldDoIntSnapping(rect))
				roundingDelta = Mathf.Round(offsetSizePixels) - offsetSizePixels;
			offsetSizePixels += roundingDelta;

			if (!enforceExactValue)
			{
				value += roundingDelta / parent.rect.size[axis];

				// Snap value to whole percent if close
				if (Mathf.Abs(Round(value * 1000) - value * 1000) < 0.1f)
					value = Round(value * 1000) * 0.001f;

				if (clampToParent)
					value = Mathf.Clamp01(value);
				if (enforceMinNoLargerThanMax)
				{
					if (isMax)
						value = Mathf.Max(value, rect.anchorMin[axis]);
					else
						value = Mathf.Min(value, rect.anchorMax[axis]);
				}
			}

			if (moveTogether)
				offsetPositionPixels = offsetSizePixels;
			else
				offsetPositionPixels = (isMax ? offsetSizePixels * rect.pivot[axis] : (offsetSizePixels * (1 - rect.pivot[axis])));
		}

		if (isMax)
		{
			Vector2 rectAnchorMax = rect.anchorMax;
			rectAnchorMax[axis] = value;
			rect.anchorMax = rectAnchorMax;

			Vector2 other = rect.anchorMin;
			rect.anchorMin = other;
		}
		else
		{
			Vector2 rectAnchorMin = rect.anchorMin;
			rectAnchorMin[axis] = value;
			rect.anchorMin = rectAnchorMin;

			Vector2 other = rect.anchorMax;
			rect.anchorMax = other;
		}

		if (smart)
		{
			Vector2 rectPosition = rect.anchoredPosition;
			rectPosition[axis] -= offsetPositionPixels;
			rect.anchoredPosition = rectPosition;

			if (!moveTogether)
			{
				Vector2 rectSizeDelta = rect.sizeDelta;
				rectSizeDelta[axis] += offsetSizePixels * (isMax ? -1 : 1);
				rect.sizeDelta = rectSizeDelta;
			}
		}
	}

	static bool AnchorAllowedOutsideParent(int axis, int minmax)
	{
		return true;
	}

	private static bool ShouldDoIntSnapping(RectTransform rect)
	{
		Canvas canvas = rect.gameObject.GetComponentInParent<Canvas>();
		return (canvas != null && canvas.renderMode != RenderMode.WorldSpace);
	}

	static float Round(float value) { return Mathf.Floor(0.5f + value); }
	#endregion
}
