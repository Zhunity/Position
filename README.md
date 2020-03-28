1、rect.rect,size和rect.sizeDelta的关系
2、scale
3、localPosition计算
https://www.jianshu.com/p/dbefa746e50d





sizeDelta = offsetMx - offsetMin

anchorMinPos = parentRect.min + Vector2.Scale(anchorMin, parentRect.size)
rectMinPos = rect.min + localPosition(TODO 不是localPosition,是pivot位置)
offset = rectMinPos - anchorMinPos
