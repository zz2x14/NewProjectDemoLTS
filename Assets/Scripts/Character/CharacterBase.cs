using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBase : MonoBehaviour,ITakenDamage
{
    public event UnityAction OnHurt = delegate { };
    public event UnityAction OnDeath = delegate { };
    
    public virtual void TakenDamage(float value)
    {
        OnHurt.Invoke();
    }

    protected void Death()
    {
        OnDeath.Invoke();
    }
    
    //public void 

   
}
