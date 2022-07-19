using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraProvider : PersistentSingletonTool<CameraProvider>
{
    [SerializeField] private Camera UICamera;

    public Camera PublicUICamera => UICamera;
}
