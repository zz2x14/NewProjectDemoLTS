using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : TrapBase
{
    [Header("等待")]
    [SerializeField] private string idleAnimName;
    [SerializeField] private float idleWaitTime;

    [Header("移动")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] movePoints;

    private WaitForSeconds idleWFS;
   
    private int idleAnimID;

    private int moveIndex;

    private float defaultSpeed;
    
    protected override void Awake()
    {
        base.Awake();

        idleAnimID = Animator.StringToHash(idleAnimName);

        idleWFS = new WaitForSeconds(idleWaitTime);

        defaultSpeed = moveSpeed;

    }

    private void OnEnable()
    {
        StartCoroutine(nameof(IdleAndMoveCor));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator IdleAndMoveCor()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate((movePoints[moveIndex].position - transform.position).normalized * Time.deltaTime);
            //Sign:朝向移动相减得到的方向向量一定记得归一化 - 匀速移动

            if (Vector3.Distance(transform.position, movePoints[moveIndex].position) <= 0.1f)
            {
                moveSpeed = 0f;
                anim.Play(idleAnimID);
            
                yield return idleWFS;
                
                anim.Play(animID);
                moveSpeed = defaultSpeed;
                
                moveIndex++;
                if (moveIndex > movePoints.Length - 1)
                    moveIndex = 0;
            }
           
            yield return null;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<ITakenDamage>().TakenDamage(trapDamage);
    }
}
