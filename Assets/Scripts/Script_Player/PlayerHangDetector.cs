using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHangDetector : MonoBehaviour
{
   [SerializeField] private Transform downwardPoint;
   [SerializeField] private Transform secondaryDetectorPoint;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private float horizontalDetectorDis;
   [SerializeField] private float verticalDetectorDis;
   
   [SerializeField] private Transform parentTransform;
  
   public bool CanHang => HangCondition();

   public void ReturnDefaultDis()
   {
      horizontalDetectorDis = 0.4f;
      verticalDetectorDis = 0.15f;//TODO:后续优化Hang
   }

   public void DisableDis()
   {
      horizontalDetectorDis = 0f;
      verticalDetectorDis = 0f;
   }

   private bool HangCondition()
   {
      //NOTE：2d射线检测返回值可以转化为bool值
      return Physics2D.Raycast(transform.position, 
                transform.right * parentTransform.transform.localScale.x , horizontalDetectorDis, groundLayer) &&
             
             Physics2D.Raycast(downwardPoint.position, Vector2.down, verticalDetectorDis, groundLayer) &&
             
             !Physics2D.Raycast(secondaryDetectorPoint.position, 
                transform.right * parentTransform.transform.localScale.x / 5, 
                horizontalDetectorDis, groundLayer);
   }

#if UNITY_EDITOR
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.blue;
      
      Gizmos.DrawRay(transform.position,transform.right * parentTransform.transform.localScale.x  * horizontalDetectorDis);
      
      Gizmos.DrawRay(secondaryDetectorPoint.position,transform.right * parentTransform.transform.localScale.x * horizontalDetectorDis);
      
      Gizmos.DrawRay(downwardPoint.position,Vector3.down * verticalDetectorDis);
   }
#endif
}
