using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    RectTransform rect;
   
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

	private Vector3 position;
	private Vector2 anchoredPosition;
	public Vector3 anchoredPosition3D;
	public Vector3 localPosition;
	public Vector2 sub;
	public Vector2 guess;


	public Vector2 anchorSub;
	private Vector2 cAndPLocalSub;    // 父节点localPosition与子localPosition的插值
	private Vector2 cAndPAnchoredSub;    // 父节点localPosition与子localPosition的插值
	public Vector2 parentPivot; // 因父节点pivot修改导致的子节点localPosition与anchoredPosition间的转换插值
	public Vector2 selfPivot; // 因自己pivot修改导致的变化

	private Vector2 RectSize;
	private Vector2 offsetMax;
	private Vector2 offsetMin;
	private Vector2 ofssetToSize;
	private Vector2 sizeDelta;

	private void Update()
    {
        position = transform.position;
        anchoredPosition = rect.anchoredPosition;
        localPosition = transform.localPosition;
        anchoredPosition3D = rect.anchoredPosition3D;

        sub = new Vector2(
            (anchoredPosition3D.x - localPosition.x),
            (anchoredPosition3D.y - localPosition.y));

       
        
        anchorSub = new Vector2(
            (0.5f - (rect.anchorMax.x + rect.anchorMin.x) / 2) * rect.rect.size.x , 
            (0.5f - (rect.anchorMax.y + rect.anchorMin.y) / 2f) * rect.rect.size.y);


        parentPivot = GetParentPivot();

		selfPivot = new Vector2(
			((rect.anchorMax.x - rect.anchorMin.x) * (0.5f - rect.pivot.x)) * GetSize(rect).x,
			((rect.anchorMax.y - rect.anchorMin.y) * (0.5f - rect.pivot.y)) * rect.rect.size.y
			);

		guess = anchorSub + parentPivot + selfPivot;

		RectSize = rect.rect.size;
		offsetMax = rect.offsetMax;
		offsetMin = rect.offsetMin;
		ofssetToSize = GetSize(rect);
		sizeDelta = rect.sizeDelta;
	}

    private Vector2 GetParentPivot()
    {
        RectTransform parent = rect.parent as RectTransform;
		cAndPLocalSub = new Vector2(
			  (0.5f - rect.pivot.x) * GetSize(rect).x - (0.5f - parent.pivot.x) * GetSize(parent).x,
				 (0.5f - rect.pivot.y) * rect.rect.size.y - (0.5f - parent.pivot.y) * parent.rect.size.y
				 );

		cAndPAnchoredSub = new Vector2(
		 (rect.pivot.x - 0.5f) * GetSize(rect).x,
		 (rect.pivot.y - 0.5f) * rect.rect.size.y
		 );
		return cAndPAnchoredSub + cAndPLocalSub;
    }

	private Vector2 GetSize(RectTransform r)
	{
		return r.rect.size - r.offsetMax + r.offsetMin;
	}

    void PositionAndLocalPosition()
    {
        Debug.Log("position:\t\t" + transform.position);

        Transform t = transform;
        Vector3 pos = Vector3.zero;
        while(t != null)
        {
            pos += t.localPosition;
            t = t.parent;
        }
        Debug.Log("localPositon sum:\t" + pos);
    }

    void AnchoredPositionAndAnchoredPosition3D()
    {
        Debug.Log("anchoredPosition:\t\t" + rect.anchoredPosition);
        Debug.Log("anchoredPosition3D:\t\t" + rect.anchoredPosition3D);
    }

    void LocalPositionAndAnchoredPosition3D()
    {
        Debug.Log("localPosition:\t\t" + transform.localPosition);
        Debug.Log("anchoredPosition3D:\t\t" + rect.anchoredPosition3D);
    }

    void DebugAllPosition()
    {
        Debug.Log("position:\t\t" + transform.position);
        Debug.Log("localPosition:\t\t" + transform.localPosition);
        Debug.Log("anchoredPosition:\t\t" + rect.anchoredPosition);
        Debug.Log("anchoredPosition3D:\t\t" + rect.anchoredPosition3D);
    }


}
