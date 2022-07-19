using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICureOverTime
{
    void StartCureOverTimeCor(float interval,float duration,float cureValue);
    IEnumerator CureOverTimeCor(float interval, float duration,float cureValue);
}
