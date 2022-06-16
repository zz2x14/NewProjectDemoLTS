using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCameraSpace : MonoBehaviour
{
    [SerializeField] private GameObject vcamera;

    private void OnTriggerEnter2D(Collider2D col)
    {
        vcamera.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        vcamera.SetActive(false);
    }
}
