using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGiveDamageOverTime
{
    public float Duration { get; set; }
    public void GiveDamageOverTime(ITakenDamageOverTime target);
}
