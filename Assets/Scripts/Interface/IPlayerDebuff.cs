using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDebuff
{
    //void LimitMoveSpeed();
    void LimitRoll(float duration);
    
    public void TakenDamageOverTime(float duration,float damage);
    public IEnumerator TakenDamageOverTimeCor(float duration,float damage);
}
