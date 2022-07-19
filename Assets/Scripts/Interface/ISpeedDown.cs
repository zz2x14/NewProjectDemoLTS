using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedDown
{
    void StartSpeedDownCor(float duration);

    IEnumerator SpeedDownCor(float duration);
}
