using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutUI : MonoBehaviour
{
    private Image fadeOutImage;
    private Canvas sceneFaderCanvas;
    
    private Color faderColor;
    
    [SerializeField] private float faderSpeed;
    
    private void Awake()
    {
        sceneFaderCanvas = GetComponentInParent<Canvas>();
        fadeOutImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        sceneFaderCanvas.enabled = true;
        
        fadeOutImage.color = Color.black;
        faderColor = Color.black;
        
        StartCoroutine(nameof(FadeOutCor));
    }
    
    IEnumerator FadeOutCor()
    {
        while (fadeOutImage.color.a > 0f)
        {
            //Sign:!!! 此处踩雷 让a等于 1减去值 所以会出现不停的从1到某一个值的情况
            faderColor.a = Mathf.Clamp(faderColor.a - faderSpeed * Time.deltaTime, 0f, 1f);
            
            fadeOutImage.color = faderColor;
            
            yield return null;
            
            if (fadeOutImage.color.a <= 0.05f)
            {
                sceneFaderCanvas.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
