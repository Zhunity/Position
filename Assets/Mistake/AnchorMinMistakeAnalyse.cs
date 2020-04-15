using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnchorMinMistakeAnalyse : MistakeAnalyse
{

	// Update is called once per frame
	void Update()
	{
		origin = GetAnchorLocal(parentRect, rect.anchorMin);
		result = GetAnchorMinPosByLocalPosition();

	}

	Vector2 GetAnchorLocal(RectTransform guiParent, Vector2 anchor)
	{
		return NormalizedToPointUnclamped(guiParent.rect, anchor);
	}

	Vector2 NormalizedToPointUnclamped(Rect rectangle, Vector2 normalizedRectCoordinates)
	{
		return new Vector2(
			Mathf.LerpUnclamped(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x),
			Mathf.LerpUnclamped(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y)
		);
	}

	Vector2 GetAnchorMinPosByLocalPosition()
	{
		Vector2 anchorMinPos = parentRect.rect.min + Vector2.Scale(rect.anchorMin, parentRect.rect.size);
		return anchorMinPos;
	}

	void AnchorSceneGUI(RectTransform gui, RectTransform guiParent, Transform parentSpace, bool interactive, int minmaxX, int minmaxY, int id)
	{
		//Vector3 curPos = new Vector2();
		//curPos.x = (minmaxX == 0 ? gui.anchorMin.x : gui.anchorMax.x);
		//curPos.y = (minmaxY == 0 ? gui.anchorMin.y : gui.anchorMax.y);
		//curPos = GetAnchorLocal(guiParent, curPos);
		//curPos = parentSpace.TransformPoint(curPos);

		//float size = 0.05f * HandleUtility.GetHandleSize(curPos);

		//if (minmaxX < 2)
		//	curPos += parentSpace.right * size * (minmaxX * 2 - 1);
		//if (minmaxY < 2)
		//	curPos += parentSpace.up * size * (minmaxY * 2 - 1);

		//if (minmaxX < 2 && minmaxY < 2)
		//	//DrawAnchor(curPos, parentSpace.right * size * 2 * (minmaxX * 2 - 1), parentSpace.up * size * 2 * (minmaxY * 2 - 1));

		//if (!interactive)
		//	return;

		//Event evtCopy = new Event(Event.current);

		//EditorGUI.BeginChangeCheck();
		//Vector3 newPos = Handles.Slider2D(id, curPos, parentSpace.forward, parentSpace.right, parentSpace.up, size, (Handles.CapFunction)null, Vector2.zero);

		//if (evtCopy.type == EventType.MouseDown && GUIUtility.hotControl == id)
		//{
		//	s_DragAnchorsTogether = EditorGUI.actionKey;
		//	s_StartDragAnchorMin = gui.anchorMin;
		//	s_StartDragAnchorMax = gui.anchorMax;
		//	RectTransformSnapping.CalculateAnchorSnapValues(parentSpace, gui.transform, gui, minmaxX, minmaxY);
		//}

		//if (EditorGUI.EndChangeCheck())
		//{
		//	Undo.RecordObject(gui, "Move Rectangle Anchors");
		//	Vector2 offset = parentSpace.InverseTransformVector(newPos - curPos);
		//	for (int axis = 0; axis <= 1; axis++)
		//	{
		//		offset[axis] /= guiParent.rect.size[axis];

		//		int minmaxForAxis = (axis == 0 ? minmaxX : minmaxY);
		//		bool isMax = (minmaxForAxis == 1);
		//		float old = isMax ? gui.anchorMax[axis] : gui.anchorMin[axis];
		//		float newValue = old + offset[axis];

		//		// Constraint to valid values
		//		float snappedValue = newValue;
		//		if (!AnchorAllowedOutsideParent(axis, minmaxForAxis))
		//			snappedValue = Mathf.Clamp01(snappedValue);
		//		if (minmaxForAxis == 0)
		//			snappedValue = Mathf.Min(snappedValue, gui.anchorMax[axis]);
		//		if (minmaxForAxis == 1)
		//			snappedValue = Mathf.Max(snappedValue, gui.anchorMin[axis]);

		//		// Snap to sibling anchors
		//		float snapSize = HandleUtility.GetHandleSize(newPos) * 0.05f / guiParent.rect.size[axis];
		//		snapSize *= parentSpace.InverseTransformVector(axis == 0 ? Vector3.right : Vector3.up)[axis];
		//		snappedValue = RectTransformSnapping.SnapToGuides(snappedValue, snapSize, axis);

		//		bool snap = snappedValue != newValue;
		//		newValue = snappedValue;

		//		if (minmaxForAxis == 2)
		//		{
		//			SetAnchorSmart(gui, newValue, axis, false, !evtCopy.shift, snap, false, s_DragAnchorsTogether);
		//			SetAnchorSmart(gui, newValue, axis, true, !evtCopy.shift, snap, false, s_DragAnchorsTogether);
		//		}
		//		else
		//		{
		//			SetAnchorSmart(gui, newValue, axis, isMax, !evtCopy.shift, snap, true, s_DragAnchorsTogether);
		//		}
		//		EditorUtility.SetDirty(gui);
		//		if (gui.drivenByObject != null)
		//			RectTransform.SendReapplyDrivenProperties(gui);
		//	}
		//}

		//SetFadingBasedOnMouseDownUp(ref m_ChangingAnchors, evtCopy);
	}
}
