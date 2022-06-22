using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeetle : BossController
{
    [Header("飞行")] 
    [SerializeField] private Transform[] flyPoints;
    [SerializeField] private float flySpeed;

    [Header("投掷")] 
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject throwItemPrefab;
    [SerializeField] private int maxThrowCount;

    [Header("地面检测")] 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private float groundDetectorRange;
    
    public int FlyIndex { get; set; }
    public int ThrowCount { get; set; }
    public Vector3 SkyTargetPoint => flyPoints[FlyIndex].position;
    public int PointsCount => flyPoints.Length;
    public float FlySpeed => flySpeed;

    public bool CloseToFlyPoint => CloseToTarget(flyPoints[FlyIndex].position, 0.1f);
    public bool ThrowOver => ThrowCount >= maxThrowCount;

    public bool IsOnGround => Physics2D.OverlapCircle(groundDetectorPoint.position, groundDetectorRange, groundLayer);
    

    protected override void OnEnable()
    {
        base.OnEnable();

        ThrowCount = 0;
    }

    public void FlyToNextPoint()
    {
        MoveToTarget(flySpeed,SkyTargetPoint);
        FaceToTarget(SkyTargetPoint);
    }

    public void Throw()
    {
        PoolManager.Instance.Release(throwItemPrefab,throwPoint.position,throwPoint.rotation);
        ThrowCount++;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundDetectorPoint.position,groundDetectorRange);
    }
}
