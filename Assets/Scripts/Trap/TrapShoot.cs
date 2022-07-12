using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShoot : TrapBase
{
    [Header("发射")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootRange;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [Space] 
    [SerializeField] private bool isLoop;
    [SerializeField] private int shootLoopTime;
    [SerializeField] private string idleAnimName;

    private int idleNameID;

    private bool foundPlayer;

    private int shootIndex;

    protected override void Awake()
    {
        base.Awake();

        if (isLoop)
        {
            idleNameID = Animator.StringToHash(idleAnimName);
        }
    }

    private void OnEnable()
    {
        foundPlayer = false;

        if (!isLoop)
        {
            StartCoroutine(nameof(ShootCor));
        }
        else
        {
            shootIndex = 0;
            StartCoroutine(nameof(LoopShootCor));
        }
        
    }

    public override void HitPlayer01()
    {
        if (isLoop)
        {
            shootIndex++;
        }
        
        var bulletGO = PoolManager.Instance.Release(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bulletGO.GetComponent<TrapBullet>().FlySpeed = bulletSpeed;
        bulletGO.GetComponent<TrapBullet>().Damage = trapDamage;
    }

    IEnumerator ShootCor()
    {
        while (!foundPlayer)
        {
            if (Physics2D.Raycast(shootPoint.position, -transform.right, shootRange, playerLayer))
            {
                anim.Play(animID);
                foundPlayer = true;
            }
            
            yield return null;
        }
    }

    IEnumerator LoopShootCor()
    {
        while (gameObject.activeSelf)
        {
            if (Physics2D.Raycast(shootPoint.position, -transform.right, shootRange, playerLayer))
            {
                anim.Play(animID);
                
                if (shootIndex >= shootLoopTime)
                {
                    anim.Play(idleNameID);
                    yield return new WaitForSeconds(1f);
                    shootIndex = 0;
                }
            }
            else
            {
                shootIndex = 0;
                anim.Play(idleAnimName);
            }
            
            yield return null;
        }
       
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootPoint.position,-transform.right * shootRange);
    }
}
