using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PortalCaller : MonoBehaviour
{
    [SerializeField] private int targetSceneID;
    
    private void OnEnable()
    {
        SceneController.Instance.Teleport(targetSceneID);
    }
}
