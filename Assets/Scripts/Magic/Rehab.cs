using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rehab : Magic
{
    public void ClearPlayerDebuffAnimEvent()
    {
        ComponentProvider.Instance.PlayerAvatar.StopPlayerDebuff();
    }
    
}
