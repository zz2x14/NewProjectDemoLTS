using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossDeathState",fileName = "BossDeathState")]
public class BossDeathState : BossStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (boss.transform.parent != null)
            {
                boss.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                boss.transform.gameObject.SetActive(false);
            }
            
        }
    }
    
}
