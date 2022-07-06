using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Item thisItem;
    
    private Image itemSlotImage;
    private Image itemIconImage;

    private Vector3 thisTransformWithOffset;
    
    private Color defaultColor;
    private Color clickColor;
    
    public int SiblingIndex { get; private set; }

    private bool isClilked;
    
    private void Awake()
    {
        itemSlotImage = GetComponent<Image>();
        itemIconImage = transform.GetChild(0).GetComponent<Image>();
        
        defaultColor = Color.white;
        clickColor = new Color(1, 1, 1, 0.5f);
        
        SiblingIndex = transform.GetSiblingIndex();
    }

    private void Update()
    {
        DropItemInBackpack();
    }

    public void DropItemInBackpack()
    {
        if (itemSlotImage.color == clickColor)
        {
            if (PlayerBackpackSystem.Instance.IsDropKeyPressed)
            {
                PlayerBackpackSystem.Instance.RemoveItemFromBackpack(PlayerBackpackSystem.Instance.GetItemInBackpack(SiblingIndex));

                if (itemIconImage.sprite == null)
                {
                    itemSlotImage.color = defaultColor;
                    ItemDescriptionUI.Instance.ClearItemDes();
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isClilked) return;
        
        thisItem = PlayerBackpackSystem.Instance.GetItemInBackpack(transform.GetSiblingIndex());
        
        if (thisItem !=null)
        {
            itemSlotImage.color = clickColor;
            
            ItemDescriptionUI.Instance.ShowCurItemDescription(thisItem);
            
            PlayerBackpackSystem.Instance.SetItemDesImagePos(new Vector3(itemSlotImage.rectTransform.position.x + PlayerBackpackSystem.Instance.OffsetX,
                itemSlotImage.rectTransform.position.y + PlayerBackpackSystem.Instance.OffsetY, 0f));
            
            PlayerBackpackSystem.Instance.EnableItemDesImage();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemSlotImage.color = defaultColor;
        
        PlayerBackpackSystem.Instance.DisableItemDesImage();
        ItemDescriptionUI.Instance.ClearItemDes();
    }
}
