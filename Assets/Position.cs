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

    public Vector3 position;
    public Vector2 anchoredPosition;
    public Vector3 anchoredPosition3D;
    public Vector3 localPosition;
    
  
    public Vector2 sub;
    public Vector2 anchorSub;
    private void Update()
    {
        position = transform.position;
        anchoredPosition = rect.anchoredPosition;
        localPosition = transform.localPosition;
        anchoredPosition3D = rect.anchoredPosition3D;

        sub = new Vector2(
            (anchoredPosition3D.x - localPosition.x) / rect.rect.size.x,
            (anchoredPosition3D.y - localPosition.y) / rect.rect.size.y);
        
        
        anchorSub = new Vector2(
            (0.5f - (rect.anchorMax.x + rect.anchorMin.x) / 2),// + (0.5f - (rect.anchorMax.x - rect.anchorMin.x) * rect.pivot.x),
            (0.5f - (rect.anchorMax.y + rect.anchorMin.y) / 2f));
        //(0.5f - rect.pivot.x) * rect.rect.size.x,
        //(0.5f - rect.pivot.y) * rect.rect.size.y,
        //0f);
        //parentPivot = transform.parent.GetComponent<RectTransform>().pivot;
        //pivot = rect.pivot;
    }

    Vector3 GetWidgetWorldPoint(RectTransform target)
    {
        //pivot position + item size has to be included
        var pivotOffset = new Vector3(
            (0.5f - target.pivot.x) * target.rect.size.x,
            (0.5f - target.pivot.y) * target.rect.size.y,
            0f);
        var localPosition = target.localPosition + pivotOffset;
        return target.parent.TransformPoint(localPosition);
    }

    Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
    {
        return target.InverseTransformPoint(worldPoint);
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
