using System;
using System.Collections;
using System.Collections.Generic;
using MyEventSpace;
using UnityEngine;

public class LockedPortalCaller : MonoBehaviour
{
    [SerializeField] private PortalDataContainer portalDataContainer;
    
    private void OnEnable()
    {
        EventManager.Instance.AddEventHandlerListener(EventName.OnSceneTeleport,RestorePortalData);

        if (portalDataContainer.IsLocked) return;

        SceneController.Instance.Teleport(portalDataContainer.TargetSceneID);

        RestorePortalData(null,EventArgs.Empty);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventHandlerListener(EventName.OnSceneTeleport,RestorePortalData);
    }

    public void UnlockAndSetSceneID(int id)
    {
        portalDataContainer.IsLocked = false;
        portalDataContainer.TargetSceneID = id;
    }

    public void RestorePortalData(object o, EventArgs e)
    {
        portalDataContainer.IsLocked = true;
        portalDataContainer.TargetSceneID = -1;
    }
}
