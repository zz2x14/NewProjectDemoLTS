using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyShoot : EnemyController
{
   [Header("远程检测")] 
   [SerializeField] private Transform detectorPoint;

   [Header("子弹")] 
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private float bulletSpeed;

   [Header("逃跑")] 
   [SerializeField] private Vector2 escapeDis;
   [SerializeField] private Transform[] escapePoints;

   // private List<float> pointsDisList = new List<float>();
   // private Dictionary<float, Vector3> pointsTable = new Dictionary<float, Vector3>();
 
   public override bool FoundPlayer =>
      Physics2D.Raycast(attackPoint.position, -transform.right * transform.localScale.x, attackRange, playerLayer);
   public bool PlayerCloseTo => Physics2D.OverlapBox(detectorPoint.position, escapeDis, 0f, playerLayer);

   protected override void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(attackPoint.position,- transform.right * transform.localScale.x * attackRange);

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireCube(detectorPoint.position,escapeDis);
   }

   // public Vector2 GetEscapePoint()
   // {
   //    pointsDisList.Clear();
   //    pointsTable.Clear();
   //    
   //    for (int i = 0; i < escapePoints.Length; i++)
   //    {
   //       var dis = Vector2.Distance(escapePoints[i].position, playerPos.position);
   //       pointsDisList.Add(Vector2.Distance(escapePoints[i].position, playerPos.position));
   //       pointsTable.Add(dis,escapePoints[i].position);
   //    }
   //
   //    return pointsTable[pointsDisList.Max()];
   // }

   public Vector2 GetRandomEscapaPoint()//TODO：暂时先用该随机方案
   {
      return escapePoints[Random.Range(0, escapePoints.Length)].position;
   }
   public void EscapePlayer(Vector2 escapePoint)
   {
      MoveToDestination(moveSpeed,escapePoint);
      FaceToPlayer();
   }

   public override void Attack()
   {
      GameObject bulletGO =  PoolManager.Instance.Release(bulletPrefab.gameObject,attackPoint.position,attackPoint.rotation);
      var enemybullet = bulletGO.GetComponent<EnemyBullet>();
      enemybullet.FlyDir = transform.localScale.x;
      enemybullet.FlySpeed = bulletSpeed;
      enemybullet.Damage = enemyData.baseData.attackDamage;
   }
  

 
}
