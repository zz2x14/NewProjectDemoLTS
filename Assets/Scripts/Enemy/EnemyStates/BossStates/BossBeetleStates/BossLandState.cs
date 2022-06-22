using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossBeetleState/BossLandState",fileName = "BossLandState")]
public class BossLandState : BossBeetleStateBase//Sign:因为到达一个飞行点后才会进入land状态，所以理论上是不存在着需要飞向一个点的操作的
{
    [SerializeField] private string landAnimName;

    private int landAnimID;

    protected override void OnEnable()
    {
        base.OnEnable();

        landAnimID = Animator.StringToHash(landAnimName);
    }

    public override void OnEnter()
    {
        base.OnEnter();

        bossBeetle.FlyIndex = 0;
        enemy.SetRbVelocity(Vector2.zero);
        enemy.FaceToTarget(bossBeetle.SkyTargetPoint);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (bossBeetle.CloseToFlyPoint)
        {
            enemy.FaceToPlayer();
            stateMachine.Anim.CrossFade(landAnimID,0.1f);
        }
        
        if (bossBeetle.IsOnGround)
        {
            stateMachine.SwitchState(typeof(BossMoveTransitionalState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (bossBeetle.CloseToFlyPoint)
        {
            enemy.MoveToTarget(bossBeetle.FlySpeed,enemy.OriginalPos);
        }
    }
}
