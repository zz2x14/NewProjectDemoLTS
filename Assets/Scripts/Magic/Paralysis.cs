using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralysis : Magic
{
    private Rigidbody2D rb;
    
    [SerializeField] protected string defaultAnimName;
    [SerializeField] private string flyAnimName;
    [SerializeField] private float flySpeed;
    
    private Vector2 flyDir;

    private ControlMagic controlMagic;
    
    private int flyAnimID;
    private bool chargedOver;

    protected override void Awake()
    {
        base.Awake();
        
        controlMagic = magic as ControlMagic;

        rb = GetComponent<Rigidbody2D>();

        flyDir = new Vector2(flySpeed, 0f);
        
        flyAnimID = Animator.StringToHash(flyAnimName);
    }

    private void Update()
    {
        if (CheckAnimProgressTool.AnimOverWithName(anim,animInfo,defaultAnimName, 0.95f))
        {
            chargedOver = true;
            anim.Play(flyAnimID);
        }
    }

    private void FixedUpdate()
    {
        if(!chargedOver) return;
        rb.velocity = transform.right * ComponentProvider.Instance.PlayerPos.localScale.x * flyDir;
    }
    
    private void OnDisable()
    {
        chargedOver = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<ILimitAction>().StartLimitActionCor(
            col.GetComponent<EnemyController>().enemyData.IsBoss ? controlMagic.ControlValue / 2f : controlMagic.ControlValue);
        
        gameObject.SetActive(false);
    }
    
}
