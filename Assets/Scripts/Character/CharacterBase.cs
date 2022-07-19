using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBase : MonoBehaviour,ITakenDamage
{
    public event UnityAction OnHurt = delegate { };
   
    
    public virtual void TakenDamage(float value)
    {
        OnHurt.Invoke();
    }

    public virtual void RecoverHealth(float value)
    {
        
    }

    
}
