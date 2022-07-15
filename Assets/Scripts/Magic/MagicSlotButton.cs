using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicSlotButton : MonoBehaviour
{
    private NpcMagic npc;
    private Button thisButton;

    private void Awake()
    {
        npc = FindObjectOfType<NpcMagic>();

        thisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        thisButton.onClick.AddListener(OnMagicSlotButtonClick);
    }

    private void OnDisable()
    {
        thisButton.onClick.RemoveAllListeners();
    }

    private void OnMagicSlotButtonClick()
    {
        npc.EnableConfirmMasterGO();
        npc.CurMagicIndex = npc.MagicSlotButtons.IndexOf(thisButton);
    }
}
