using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricTest : MonoBehaviour
{
	public RectTransform parent;
	public RectTransform self;
	public RectTransform anchor;
	public RectTransform sizeDelta;

	


	public Vector3 anchorPosition;
	public Vector3 reuslt;
    // Update is called once per frame
    void Update()
    {
		anchorPosition = self.anchoredPosition;
		reuslt = self.position- anchor.position;
	}
}
