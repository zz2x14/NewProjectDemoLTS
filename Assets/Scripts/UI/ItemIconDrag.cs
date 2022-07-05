using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIconDrag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Image itemIcon;

    private int originalIndex;

    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemIcon.sprite == null) return;//图片为空代表着没有Item
        
        itemIcon.raycastTarget = false;//本身忽略射线检测
        
        originalIndex = transform.GetSiblingIndex();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(itemIcon.sprite == null) return;

        if (originalIndex != PlayerBackpackSystem.Instance.BackpackCapacity)
        {
            transform.SetSiblingIndex(PlayerBackpackSystem.Instance.BackpackCapacity -1);
        }
        
        transform.GetChild(0).position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemIcon.sprite == null || eventData.pointerCurrentRaycast.gameObject == null)
        {
            Restore();
            return;
        }

        if (originalIndex == 0)
        {
            PlayerBackpackSystem.Instance.SwitchItem
                (originalIndex,eventData.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex() + 1);
        }
        else
        {
            PlayerBackpackSystem.Instance.SwitchItem
                (originalIndex,eventData.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex());
        }
       
        
        Restore();
    }

    public void Restore()//还原
    {
        transform.GetChild(0).GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
        transform.SetSiblingIndex(originalIndex);

        itemIcon.raycastTarget = true;
    }
}
