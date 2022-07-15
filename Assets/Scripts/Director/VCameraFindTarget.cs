using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VCameraFindTarget : MonoBehaviour
{
    private PlayerController playerController;

    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        playerController = FindObjectOfType<PlayerController>();

        virtualCamera.Follow = playerController.transform;
    }

}
