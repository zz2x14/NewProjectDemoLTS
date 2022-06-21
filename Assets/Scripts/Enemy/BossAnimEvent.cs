using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvent : MonoBehaviour
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
        switch (bossController.enemyData.enemyType)
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
        bossThreeMelee.Melee2();
    }

    public void BTM_Melee2PopPlayer()
    {
        bossThreeMelee.Melee2PopPlayer();
    }

    public void BTM_Melee3()
    {
        bossThreeMelee.Melee3();
    }

    public void Beetle_Attack()
    {
        bossController.Attack1();
    }
}
