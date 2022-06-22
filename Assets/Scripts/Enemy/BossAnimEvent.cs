using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvent : MonoBehaviour
{
    private BossController bossController;
    private BossThreeMelee bossThreeMelee;
    private BossBeetle bossBeetle;
    private BossSummon bossSummon;
    
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
            case EnemyType.BeetleLike:
                bossBeetle = bossController as BossBeetle;
                break;
            case EnemyType.ToadKingLike:
                bossSummon = bossController as BossSummon;
                break;
        }
    }

    public void BTM_Melee1()
    {
        bossThreeMelee.Melee1();
    }
    
    public void BTM_Melee2()
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

    public void BossAttack1_Melee()
    {
        bossController.Attack1();
    }

    public void BeetleThrow()
    {
        bossBeetle.Throw();
    }

    public void Summon()
    {
        bossSummon.Summon();
    }

    public void SummonAttack2_Melee()
    {
        bossSummon.Attack2();
    }

}
