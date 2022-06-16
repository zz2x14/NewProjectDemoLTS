using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnStairsDetector : MonoBehaviour
{
    [SerializeField] private Vector2 range;
    [SerializeField] private LayerMask stairsLayer;
    [SerializeField] private LayerMask stairsGroundLayer;
    [SerializeField] private Transform onPoint;
    [SerializeField] private float detectorDis;

    public bool IsInStairs => Physics2D.OverlapBox(transform.position, range, 0f, stairsLayer);

    public bool IsOnStairs => 
        Physics2D.Raycast(onPoint.position, Vector2.down, detectorDis, stairsGroundLayer);
    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,range);
        Gizmos.DrawRay(onPoint.position,Vector3.down * detectorDis);
    }
#endif

}
