using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PortalData",fileName = "NewPortalData")]
public class PortalDataContainer : ScriptableObject
{
    [SerializeField] private PortalData portalData;

    public int TargetSceneID
    {
        get => portalData.targetSceneID;
        set => portalData.targetSceneID = value;
    }
    
    public bool IsLocked
    {
        get => portalData.isLocked;
        set => portalData.isLocked = value;
    }

}

[System.Serializable]
public class PortalData
{
    public int targetSceneID;
    public bool isLocked = true;
}
