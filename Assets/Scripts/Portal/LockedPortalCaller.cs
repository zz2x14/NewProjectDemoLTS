using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedPortalCaller : MonoBehaviour
{
    [SerializeField] private int targetSceneID;
    [SerializeField] private bool isLocked = true;
    
    private void OnEnable()
    {
        if (!isLocked)
        {
            SceneController.Instance.Teleport(targetSceneID);
        }
    }

    public void UnlockAndSetSceneID(int id)
    {
        isLocked = false;
        targetSceneID = id;
    }
    
}
