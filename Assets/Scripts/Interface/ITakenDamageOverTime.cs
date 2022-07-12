using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakenDamageOverTime
{
    public void TakeDamageOverTime(float duration,float damage);
    public IEnumerator TakeDamageOverTimeCor(float duration,float damage);
}
