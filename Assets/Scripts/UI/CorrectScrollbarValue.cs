using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectScrollbarValue : MonoBehaviour//TODO:更正确地方法
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Canvas canvas;

    private float timer;
    private float waitTime = 1f;
    
    private void Update()
    {
        if ( canvas.enabled)
        {
            scrollbar.value = 1f;

            if (scrollbar.value > 0f)
            {
                timer += 0.9f;
                if (timer >= waitTime)
                {
                    Destroy(this);
                }
            }
        }
    }

   

     
}
