using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radius;

    private Collider2D[] colliders = new Collider2D[1];//NOTE：需要初始化

    public bool IsOnGround => Physics2D.OverlapCircleNonAlloc(transform.position, radius, colliders, groundLayer) > 0;


    // private void Update()
    // {
    //     Debug.Log(IsOnGround);
    // }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
