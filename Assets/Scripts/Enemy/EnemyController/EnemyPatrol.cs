using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPatrol : EnemyController
{
    [Header("巡逻")]
    [SerializeField] private Transform[] patrolPoints;
    public int PointsCount => patrolPoints.Length;
    public int PatrolIndex { get;set; }//Sign:通过状态要改变的变量 不要在状态内声明！

    public bool ArrivedPatrolPoint => CloseToTarget(patrolPoints[PatrolIndex].position, 0.1f);

    public void MoveToNextPoint()
    {
        MoveToTargetHorizontal(moveSpeed,patrolPoints[PatrolIndex].position);
        FaceToTarget(patrolPoints[PatrolIndex].position);
    }
}
