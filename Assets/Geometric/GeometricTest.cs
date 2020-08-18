using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricTest : MonoBehaviour
{

	public RectTransform anchor;
	public RectTransform sizeDelta;

	private RectTransform self;

	void Start()
	{
		self = GetComponent<RectTransform>();
	}

	public Vector3 anchorPosition;
	public Vector3 reuslt;
    // Update is called once per frame
    void Update()
    {
		anchorPosition = self.anchoredPosition;


	}
}
