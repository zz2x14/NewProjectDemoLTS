using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyDeathState",fileName = "EnemyDeathState")]
public class EnemyDeathState : EnemyStateBase
{
    [SerializeField] private float flyEnemyDeathFallForce;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if (enemy.enemyData.enemyType == EnemyType.WaspLike)
        { 
            enemy.SetRbVelocityY(flyEnemyDeathFallForce);      
        }
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (enemy.transform.parent != null)
            {
                enemy.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                enemy.transform.gameObject.SetActive(false);
            }
        }
    }
}
