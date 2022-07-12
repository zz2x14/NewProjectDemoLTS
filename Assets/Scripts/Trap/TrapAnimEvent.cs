using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapAnimEvent : MonoBehaviour
{
    private TrapBase trapBase;

    private void Awake()
    {
        trapBase = GetComponentInParent<TrapBase>();
        
    }

    public void Hit01()
    {
        trapBase.HitPlayer01();
    }

}
