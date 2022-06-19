using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAnimEvent : MonoBehaviour
{
    private BossController bossController;

    private BossThreeMelee bossThreeMelee;

    private void Awake()
    {
        bossController = GetComponentInParent<BossController>();

        GetDifferentBossController();
    }

    private void GetDifferentBossController()
    {
        switch (bossController.bossData.enemyType)
        {
            case EnemyType.TrollLike :
                bossThreeMelee = bossController as BossThreeMelee;
                break;
        }
    }

    public void BTM_Melee1()
    {
        bossThreeMelee.Melee1();
    }
    
    public void BTM_Melee1Stage2()
    {
        bossThreeMelee.Melee1Stage2();
    }

    public void BTM_Melee2PopPlayer()
    {
        bossThreeMelee.Melee2PopPlayer();
    }

    public void BTM_Melee3()
    {
        bossThreeMelee.Melee3();
    }
}
