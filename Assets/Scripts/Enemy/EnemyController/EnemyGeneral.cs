using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : EnemyController
{
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,detectorRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
    
  
    
}
