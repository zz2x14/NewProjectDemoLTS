using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGivePlayerDamageOverTime
{
    public void GiveDamageOverTime(IPlayerDebuff target);
}
