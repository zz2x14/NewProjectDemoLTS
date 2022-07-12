using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDestroyByAnimTool : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string animName;
    [SerializeField] private float delayTime;
    
    private AnimatorStateInfo animInfo;

    private bool isOver;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(WaitAnimOverAndDestroyCor));
    }

    IEnumerator WaitAnimOverAndDestroyCor()
    {
        while (!isOver)
        {
            animInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animInfo.IsName(animName) && animInfo.normalizedTime >= 1f + delayTime)
            {
                isOver = true;
            }
            yield return null;
        }
        
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
