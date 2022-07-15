using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedPortalCaller : MonoBehaviour
{
    [SerializeField] private PortalDataContainer portalDataContainer;
    
    private void OnEnable()
    {
        if (!portalDataContainer.IsLocked)
        {
            SceneController.Instance.Teleport(portalDataContainer.TargetSceneID);

            RecoverPortalData();
        }
    }

    public void UnlockAndSetSceneID(int id)
    {
        portalDataContainer.IsLocked = false;
        portalDataContainer.TargetSceneID = id;
    }

    public void RecoverPortalData()
    {
        portalDataContainer.IsLocked = true;
        portalDataContainer.TargetSceneID = 0;
    }
}
