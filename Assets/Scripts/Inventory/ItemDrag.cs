using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Image itemIcon;

    private int originalIndex;

    private string originalName;

    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();

        originalName = gameObject.name;
        
        originalIndex = GetComponent<ItemSlot>().SiblingIndex;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemIcon.sprite == null) return;//图片为空代表着没有Item
        
        itemIcon.raycastTarget = false;//本身忽略射线检测
        
        transform.GetChild(1).gameObject.SetActive(false);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(itemIcon.sprite == null) return;
        
        if (originalIndex != PlayerBackpackSystem.Instance.BackpackCapacity)//层级显示作用，若是最后一个槽不变动
        {
            transform.SetSiblingIndex(PlayerBackpackSystem.Instance.BackpackCapacity -1);
        }
        
        transform.GetChild(0).position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemIcon.sprite == null || eventData.pointerCurrentRaycast.gameObject == null || eventData.pointerCurrentRaycast.gameObject.name == originalName)
        {
            Restore();
            return;
        }
        
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<ItemSlot>().SiblingIndex);
        PlayerBackpackSystem.Instance.SwitchItem
            (originalIndex,eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<ItemSlot>().SiblingIndex);
        
        Restore();
    }

    public void Restore()//还原
    {
        transform.SetSiblingIndex(originalIndex);
        
        
        transform.GetChild(0).GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
        
        transform.GetChild(1).gameObject.SetActive(true);

        itemIcon.raycastTarget = true;
    }
}
