using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    private EnemyController myself;

    private EnemyFly enemyFly;

    private void Awake()
    {
        myself = GetComponentInParent<EnemyController>();
        
        GetDifferentController();
    }

    private void GetDifferentController()
    {
        switch (myself.enemyData.enemyType)
        {
            case EnemyType.WaspLike:
                enemyFly = myself as EnemyFly;
                break;
        }
    }

    public void AttackAnimationEvent()
    {
        myself.Attack1();
    }

    public void FlyAttackAnimationEvent()
    {
        enemyFly.FlyAttack();
    }
}
