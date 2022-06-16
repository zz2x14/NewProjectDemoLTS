using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterBase : MonoBehaviour,ITakenDamage
{
    public static event UnityAction OnHurt = delegate { };
    
    public virtual void TakenDamage(float value)
    {
        OnHurt.Invoke();
    }
}
