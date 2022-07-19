using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Magic
{
    private Rigidbody2D rb;
    
    [SerializeField] protected string defaultAnimName;
    [SerializeField] private string flyAnimName;
    [SerializeField] private float flySpeed;

    private int flyAnimID;

    private Vector2 flyDir;

    private bool chargedOver;

    private DamageMagic damageMagic;

    protected override void Awake()
    {
        base.Awake();

        damageMagic = magic as DamageMagic;

        rb = GetComponent<Rigidbody2D>();

        flyAnimID = Animator.StringToHash(flyAnimName);

        flyDir = new Vector2(flySpeed, 0f);
    }


    private void OnDisable()
    {
        chargedOver = false;
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
        if (!chargedOver) return;
        rb.velocity = transform.right * ComponentProvider.Instance.PlayerPos.localScale.x * flyDir;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<ITakenDamage>().TakenDamage(damageMagic.Damage);
        gameObject.SetActive(false);
    }
}
