using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSceneByTalk : MonoBehaviour
{
    [SerializeField] private LockedPortalCaller managedPortal;

    public void UnlockPortalByTalk(int sceneID)
    {
        managedPortal.UnlockAndSetSceneID(sceneID);
    }
   
}
