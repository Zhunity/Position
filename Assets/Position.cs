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
    public Vector2 guess;
    public Vector2 anchorSub;
    public Vector2 cAndPLocalSub;    // 父节点localPosition与子localPosition的插值
    public Vector2 cAndPAnchoredSub;    // 父节点localPosition与子localPosition的插值
    public Vector2 parentPivot;

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
            (0.5f - (rect.anchorMax.x + rect.anchorMin.x) / 2),                                 // + (0.5f - (rect.anchorMax.x - rect.anchorMin.x) * rect.pivot.x),
            (0.5f - (rect.anchorMax.y + rect.anchorMin.y) / 2f));


        parentPivot = GetParentPivot();

        guess = anchorSub + parentPivot;
    }

    private Vector2 GetParentPivot()
    {
        RectTransform parent = rect.parent as RectTransform;
        cAndPLocalSub = new Vector2(
          (0.5f - rect.pivot.x) * rect.rect.size.x - (0.5f - parent.pivot.x) * parent.rect.size.x,
          (0.5f - rect.pivot.y) * rect.rect.size.y - (0.5f - parent.pivot.y) * parent.rect.size.y
          );

        float x = 0;
        if (parent.pivot.x == 0.5f)
        {
            x = 0;
        }
        else
        {
            x = (rect.pivot.x - 0.5f) * rect.rect.size.x;
        }

        float y = 0;
        if (parent.pivot.y == 0.5f)
        {
            y = 0;
        }
        else
        {
            y = (rect.pivot.y - 0.5f) * rect.rect.size.y;
        }
        cAndPAnchoredSub = new Vector2(
            x, y
            );
        return cAndPAnchoredSub + cAndPLocalSub;
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
