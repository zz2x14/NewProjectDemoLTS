using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDisableCanvasTool : MonoBehaviour
{
    [SerializeField] private float waitDisableCanvasTime;

    private Canvas thisCanvas;

    private Coroutine cor;
    private WaitForSecondsRealtime disableRWFS;

    private void Awake()
    {
        thisCanvas = GetComponent<Canvas>();
        
        disableRWFS = new WaitForSecondsRealtime(waitDisableCanvasTime);
    }

 
    public void StartAutomaticCor()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        
        cor = StartCoroutine(nameof(AutomaticDisableCanvasCor));
    }

    IEnumerator AutomaticDisableCanvasCor()
    {
        thisCanvas.enabled = true;

        yield return disableRWFS;

        thisCanvas.enabled = false;
        
        StopAllCoroutines();
    }
}
