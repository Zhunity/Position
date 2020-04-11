using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocalPosition : MonoBehaviour
{
	public Transform Cube;
	public Vector3 CubeLocalPosition;
	public Vector3 ImageLocalPosition;
	

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Cube.position = transform.position;
		CubeLocalPosition = Cube.localPosition;
		ImageLocalPosition = transform.localPosition;
	}
}
