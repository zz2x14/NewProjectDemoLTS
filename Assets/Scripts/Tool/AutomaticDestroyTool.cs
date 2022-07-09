using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDestroyTool : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private bool needDestroy;

    private WaitForSeconds destroyWFS;
    private void Awake()
    {
        destroyWFS = new WaitForSeconds(destroyTime);
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(AutoDestroyCor));
    }

    IEnumerator AutoDestroyCor()
    {
        yield return destroyWFS;

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
