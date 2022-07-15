using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAwakenedPortal : MonoBehaviour
{
    private Canvas teleportTipsCanvas;
    [SerializeField] private GameObject portalCallerGO;

    private void Awake()
    {
        teleportTipsCanvas = GetComponentInChildren<Canvas>();
    }
    
    private void OnEnable()
    {
        if (teleportTipsCanvas != null)
        {
            teleportTipsCanvas.worldCamera = CameraProvider.Instance.PublicUICamera;
        }
        
        StartCoroutine(nameof(EnableCallerCor));
    }

    IEnumerator EnableCallerCor()
    {
        while (gameObject.activeSelf)
        {
            if (ComponentProvider.Instance.PlayerInputAvatar.IsGameConfirmKeyPressed)
            {
                portalCallerGO.SetActive(true);
                gameObject.SetActive(false);
            }

            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        teleportTipsCanvas.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        teleportTipsCanvas.enabled = false;
    }
}
