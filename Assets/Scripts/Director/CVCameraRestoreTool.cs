using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CVCameraRestoreTool : MonoBehaviour
{
    private CinemachineVirtualCamera cvCamera;
    private CinemachineFramingTransposer cFT;
    [SerializeField] private CameraDataContainer cameraDataContainer;
    
    private void Awake()
    {
        cvCamera = GetComponent<CinemachineVirtualCamera>();
        cFT = cvCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void OnEnable()
    {
        Restore();
    }

    private void Restore()
    {
        cvCamera.m_Lens.OrthographicSize = cameraDataContainer.DefaultOrthographicSize;
        cvCamera.m_Lens.NearClipPlane = cameraDataContainer.DefaultNearClipPlane;
        
        cFT.m_ScreenX = cameraDataContainer.DefaultScreenX;
        cFT.m_ScreenY = cameraDataContainer.DefaultScreenY;
        
        cFT.m_DeadZoneWidth = cameraDataContainer.DefaultDeadZoneX;
        cFT.m_SoftZoneHeight = cameraDataContainer.DefaultDeadZoneY;

        cFT.m_SoftZoneWidth = cameraDataContainer.DefaultSoftZoneX;
        cFT.m_SoftZoneHeight = cameraDataContainer.DefaultSoftZoneY;

        cFT.m_BiasX = cameraDataContainer.DefaultBasicX;
        cFT.m_BiasY = cameraDataContainer.DefaultBasicY;
    }

    private void OnApplicationQuit()
    {
        Restore();
    }
}
