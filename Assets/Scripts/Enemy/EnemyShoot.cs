using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShoot : EnemyController
{
   [Header("远程检测")] 
   [SerializeField] private Transform detectorPoint;

   [Header("逃跑")] 
   [SerializeField] private Vector2 escapeDis;
   [SerializeField] private Transform[] escapePoints;

   private List<float> pointsDisList = new List<float>();
   private Dictionary<float, Vector3> pointsTable = new Dictionary<float, Vector3>();

   public Vector2 EscapePoint { get;set; }

   public override bool FoundPlayer =>
      Physics2D.Raycast(detectorPoint.position, -transform.right * transform.localScale.x, attackRange, playerLayer);
   public bool PlayerCloseTo => Physics2D.OverlapBox(detectorPoint.position, escapeDis, 0f, playerLayer);

   protected override void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(detectorPoint.position,- transform.right * transform.localScale.x * attackRange);

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireCube(detectorPoint.position,escapeDis);
   }

   public Vector2 GetEscapePoint()
   {
      pointsDisList.Clear();
      pointsTable.Clear();
      
      for (int i = 0; i < escapePoints.Length; i++)
      {
         var dis = Vector2.Distance(escapePoints[i].position, playerPos.position);
         pointsDisList.Add(Vector2.Distance(escapePoints[i].position, playerPos.position));
         pointsTable.Add(dis,escapePoints[i].position);
      }

      return pointsTable[pointsDisList.Max()];
      
      // Vector2 escapePoint = escapePoints[Random.Range(0,escapePoints.Length)].position;
      // return escapePoint;
   }
   public void EscapePlayer(Vector2 escapePoint)
   {
      MoveToDestination(moveSpeed,escapePoint);
   }

   public void FaceToPlayer()
   {
      transform.localScale = transform.position.x > playerPos.position.x ? Vector3.one : flipXScale;
   }

   public void DontCollidePlayerInEscaping()
   {
      gameObject.layer = 13;
   }
   public void RecoverNormalLayer()
   {
      gameObject.layer = 11;
   }
}
