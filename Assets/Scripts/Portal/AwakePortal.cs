using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakePortal : MonoBehaviour
{
    [SerializeField] private GameObject portalGo;

    private void OnDisable()
    {
        if (portalGo != null)
        {
            portalGo.SetActive(true);
        }
    }

}
