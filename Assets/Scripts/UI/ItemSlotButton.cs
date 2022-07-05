using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotButton : MonoBehaviour
{
    private Button itemSlotButton;
    private Item thisItem;

    private void Awake()
    {
        itemSlotButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        itemSlotButton.onClick.AddListener(OnItemSlotButtonClick);
    }

    private void OnDisable()
    {
        itemSlotButton.onClick.RemoveAllListeners();
    }

    public void OnItemSlotButtonClick()
    {
        thisItem = PlayerBackpackSystem.Instance.GetItemInBackpack(transform.GetSiblingIndex());
        
        if (thisItem !=null)
        {
            ItemDescriptionUI.Instance.ShowCurItemDescription(thisItem);
        }
    }
}
