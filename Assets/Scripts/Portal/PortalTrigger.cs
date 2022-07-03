using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
