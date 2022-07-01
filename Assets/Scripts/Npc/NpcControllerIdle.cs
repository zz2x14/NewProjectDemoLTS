using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcControllerIdle : NpcController
{
    private GameObject eTipGO;

    protected override void Awake()
    {
        base.Awake();

        eTipGO = transform.GetChild(1).GetChild(1).gameObject;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        
        eTipGO.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        eTipGO.SetActive(false);
    }
}
