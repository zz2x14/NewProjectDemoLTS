using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : Magic
{
    private ControlMagic controlMagic;

    private float targetProgress;

    [SerializeField] private string disableAnimName;

    private int disableAnimID;

    protected override void Awake()
    {
        base.Awake();

        controlMagic = magic as ControlMagic;
        
        targetProgress = controlMagic.ControlValue;

        disableAnimID = Animator.StringToHash(disableAnimName);
    }

    private void OnEnable()
    {
        EnableLevitation();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void EnableLevitation()
    {
        ComponentProvider.Instance.PlayerAvatar.StartLevitationCor(controlMagic.ControlValue);
        StartCoroutine(nameof(DisableCor));
    }

    IEnumerator DisableCor()
    {
        float t = 0;
        
        while (t < targetProgress)
        {
            t += Time.deltaTime;
            
            if (t >= targetProgress * 0.9f)
            {
                anim.Play(disableAnimID);
            }
            
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
    
    
}
