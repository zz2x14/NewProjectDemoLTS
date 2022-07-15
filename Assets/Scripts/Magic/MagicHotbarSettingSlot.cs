using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagicHotbarSettingSlot : MonoBehaviour,IPointerClickHandler,IPointerExitHandler
{
    private int magicIndex;

    private void Awake()
    {
        magicIndex = transform.GetSiblingIndex();
    }
  
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerMagicSystem.Instance.IsMagicClicked = true;
        PlayerMagicSystem.Instance.MagicIndex = magicIndex;
        PlayerMagicSystem.Instance.MagicOnClick(magicIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerMagicSystem.Instance.IsMagicClicked = false;
        PlayerMagicSystem.Instance.CancelMagicClick(magicIndex);
    }
}
