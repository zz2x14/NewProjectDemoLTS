using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnimEvent : MonoBehaviour
{
    private EnemyController myself;

    private void Awake()
    {
        myself = GetComponentInParent<EnemyController>();
    }

    public void AttackAnimationEvent()
    {
        myself.Attack();
    }

}
