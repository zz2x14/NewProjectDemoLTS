using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUICamera : MonoBehaviour
{
    private Canvas canvas;
  
    private void OnEnable()
    {
        canvas = GetComponent<Canvas>();
        
        canvas.worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }
}
