using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : TrapBase
{
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [Header("检测范围")]
    [SerializeField] private Vector2 triggerRange;
    [SerializeField] private Transform triggerPoint;
    
    [Header("下落")]
    [SerializeField] private Vector2 fallForce;
    
    [Header("是否消失")]
    [SerializeField] private bool willDisappear;

    private bool hasFalled;
    

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        hasFalled = false;

        StartCoroutine(nameof(CheckPlayerAndFallCor));
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<ITakenDamage>().TakenDamage(trapDamage);
        }
        
        if (col.CompareTag("Ground"))
        {
            hasFalled = true;
            rb.velocity = Vector2.zero;

            if (willDisappear)
            {
                gameObject.SetActive(false);
            }
            else
            {
                coll.isTrigger = false;
                rb.bodyType = RigidbodyType2D.Static;
            }
            
        }
    }

    IEnumerator CheckPlayerAndFallCor()
    {
        while (!hasFalled)
        {
            if (Physics2D.OverlapBox(triggerPoint.position, triggerRange, 0, playerLayer))
            {
                anim.Play(animID);
                rb.velocity = fallForce;
            }
            
            yield return null;
        }
        
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(triggerPoint.position,triggerRange);
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }
}
