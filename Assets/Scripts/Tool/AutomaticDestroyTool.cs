using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDestroyTool : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private bool needDestroy;
    [SerializeField] private bool scaledTime;

    private WaitForSeconds destroyWFS;
    private WaitForSecondsRealtime destroyRWFS;
    
    private void Awake()
    {
        if (scaledTime)
        {
            destroyWFS = new WaitForSeconds(destroyTime);
        }
        else
        {
            destroyRWFS = new WaitForSecondsRealtime(destroyTime);//Sign:会受到时间停止的影响，故使用Realtime
        }
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(AutoDestroyCor));
    }

    IEnumerator AutoDestroyCor()
    {
        if (scaledTime)
        {
            yield return destroyWFS;
        }
        else
        {
            yield return destroyRWFS;
        }
        
        if (!needDestroy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
