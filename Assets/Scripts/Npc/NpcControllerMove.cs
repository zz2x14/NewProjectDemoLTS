using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcControllerMove : NpcController
{
    private Rigidbody2D rb;

    [SerializeField] private float walkWaitTime;
    [SerializeField] private Transform[] walkPoints;

    [SerializeField] private float defaultScaleX;

    public bool CloseToTargetPoint => Vector3.Distance(transform.position, walkPoints[WalkIndex].position) <= 0.1f;
    
    public int WalkIndex { get; set; }

    public float WalkWaitTime => walkWaitTime;

    private Vector3 moveDir;
    
    private Vector3 defaultScale;
    private Vector3 flipScale;

    protected override  void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();

        defaultScale = transform.localScale;
        flipScale = new Vector3(-defaultScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void MoveToNextPoint(float moveSpeed)
    {
        moveDir = (walkPoints[WalkIndex].position - transform.position).normalized;
        rb.velocity = new Vector2(  moveSpeed, rb.velocity.y) * moveDir;
    }

    public void FaceToTarget()
    {
        transform.localScale = transform.position.x > walkPoints[WalkIndex].position.x ? defaultScale : flipScale;
    }

    public void SetRbVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    
    
    
}
