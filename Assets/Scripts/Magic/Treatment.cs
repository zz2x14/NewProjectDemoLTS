using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treatment : Magic
{
    private CureMagic cureMagic;

    [SerializeField] private float cureDuration;
    [SerializeField] private float cureInterval;

    protected override void Awake()
    {
        base.Awake();
        
        cureMagic = magic as CureMagic;
    }

    public void CureOverTimeAnimEvent()
    {
        ComponentProvider.Instance.PlayerAvatar.StartCureOverTimeCor(cureInterval,cureDuration,cureMagic.CureValue);
    }
}
