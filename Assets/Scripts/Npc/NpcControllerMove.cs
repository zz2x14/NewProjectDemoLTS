using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcControllerMove : NpcController
{
    private Rigidbody2D rb;

    [Header("移动")]
    [SerializeField] private float walkWaitTime;
    [SerializeField] private Transform[] walkPoints;
    [SerializeField] private float moveSpeed;

    public bool CloseToTargetPoint => Vector3.Distance(transform.position, walkPoints[WalkIndex].position) <= 0.1f;
    
    public int WalkIndex { get; set; }

    public float WalkWaitTime => walkWaitTime;

    private Vector3 moveDir;

    private float defaultScaleX = -1;
    private Vector3 defaultScale;
    private Vector3 flipScale;

    protected override  void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();

        defaultScale = transform.localScale;
        flipScale = new Vector3(-defaultScaleX, transform.localScale.y, transform.localScale.z);
    }

    protected override void OnEnable()
    {
        if (needName)
        {
            StartCoroutine(nameof(NameTextScaleFollowNpcCor));
        }
        
        base.OnEnable();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator NameTextScaleFollowNpcCor()
    {
        while (gameObject.activeSelf)
        {
            nameText.rectTransform.localScale = transform.localScale;

            yield return null;
        }
    }

    public void MoveToNextPoint()
    {
        moveDir = (walkPoints[WalkIndex].position - transform.position).normalized;
        rb.velocity = new Vector2( moveSpeed, rb.velocity.y) * moveDir;
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
