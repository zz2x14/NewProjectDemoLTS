using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CVCameraData",fileName = "NewCVCameraData")]
public class CameraDataContainer : ScriptableObject
{
    [SerializeField] private CVCameraData cameraData;

    public float DefaultOrthographicSize
    {
        get => cameraData.defaultOrthographicSize;
        set => cameraData.defaultOrthographicSize = value;
    }
    public float DefaultNearClipPlane
    {
        get => cameraData.defaultNearClipPlane;
        set => cameraData.defaultNearClipPlane = value;
    }
    public float DefaultScreenX{
        get => cameraData.defaultScreenX;
        set => cameraData.defaultScreenX = value;
    }
    public float DefaultScreenY{
        get => cameraData.defaultScreenY;
        set => cameraData.defaultScreenY = value;
    }
    public float DefaultDeadZoneX{
        get => cameraData.defaultDeadZoneX;
        set => cameraData.defaultDeadZoneX = value;
    }
    public float DefaultDeadZoneY{
        get => cameraData.defaultDeadZoneY;
        set => cameraData.defaultDeadZoneY = value;
    }
    public float DefaultSoftZoneX{
        get => cameraData.defaultSoftZoneX;
        set => cameraData.defaultSoftZoneX = value;
    }
    public float DefaultSoftZoneY{
        get => cameraData.defaultSoftZoneY;
        set => cameraData.defaultSoftZoneY = value;
    }
    public float DefaultBasicX{
        get => cameraData.defaultBasicX;
        set => cameraData.defaultBasicX = value;
    }
    public float DefaultBasicY{
        get => cameraData.defaultBasicY;
        set => cameraData.defaultBasicY = value;
    }

}

[System.Serializable]
public class CVCameraData
{
    public float defaultOrthographicSize;
    public float defaultNearClipPlane;
    public float defaultScreenX;
    public float defaultScreenY;
    public float defaultDeadZoneX;
    public float defaultDeadZoneY;
    public float defaultSoftZoneX;
    public float defaultSoftZoneY;
    public float defaultBasicX;
    public float defaultBasicY;
}
