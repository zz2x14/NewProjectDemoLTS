using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Amulet : Magic
{
    private CureMagic cureMagic;

    private SpriteRenderer sR;
    private Color enableColor;
    
    protected override void Awake()
    {
        base.Awake();
        
        cureMagic = magic as CureMagic;

        sR = GetComponent<SpriteRenderer>();

        enableColor = new Color(1f, 1f, 1f, 0.75f);
    }

    private void OnEnable()
    {
        sR.color = enableColor;
    }

    public void EnableAmuletAnimEvent()
    {
        ComponentProvider.Instance.PlayerAvatar.CurAmueltTimeNum = (int)cureMagic.CureValue;
    }

    public void UpdateAmuletLayer(int time)
    {
        var colorAlpha = 0.25f * time;
        
        sR.color = new Color(1f, 1f, 1f, colorAlpha);
        
        if (colorAlpha == 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
