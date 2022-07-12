using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] protected float trapDamage;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] private bool needAnim;
    [SerializeField] private string animName;
    
    protected Animator anim;
    protected int animID;

    protected virtual  void Awake()
    {
        if (needAnim)
        {
            anim = GetComponentInChildren<Animator>();
            animID = Animator.StringToHash(animName);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        
    }

    public virtual void HitPlayer01()
    {
        
    }
}
