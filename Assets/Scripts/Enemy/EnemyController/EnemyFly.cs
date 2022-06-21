using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : EnemyController
{
    private Vector2 randomPoint;
    private float randomPointX;
    private float randomPointY;
    
    public bool FlyAttackedPlayer { get; set; }
    
    public Vector2 GetRandomPointAroundSth(Transform target,Vector2 min,Vector2 max)
    {
       
        randomPointX = Random.Range(min.x, max.x);
        randomPointY = Random.Range(min.y, max.y);
        
        randomPoint = new Vector2(target.position.x + randomPointX, target.position.y + randomPointY);
      
        return randomPoint;
    }
    
    public void FlyAttack()
    {
        FlyAttackedPlayer = true;
    }
    
}
