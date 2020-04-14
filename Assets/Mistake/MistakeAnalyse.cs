using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakeAnalyse : MonoBehaviour
{
	protected RectTransform rect;
	protected RectTransform parentRect;

	public Vector2 origin;
	public Vector2 result;


	void Start()
	{
		rect = GetComponent<RectTransform>();
		parentRect = transform.parent.GetComponent<RectTransform>();
	}
}
