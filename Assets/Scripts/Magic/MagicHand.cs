using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHand : Magic
{
    [SerializeField] private Vector2 throwEnemyForce;

    private ControlMagic controlMagic;

    protected override void Awake()
    {
        base.Awake();
        
        controlMagic = magic as ControlMagic;
    }

    private void OnEnable()
    {
        if(GameManager.Instance._BattleState != PlayerBattleState.InBattle) return;
        StartCoroutine(nameof(FollowCor));
    }

    IEnumerator FollowCor()
    {
        while (gameObject.activeSelf)
        {
            transform.position = GameManager.Instance.CurBattleTargetPos;
            
            yield return null;
        }
    }

    public void ThrowEnemy()
    {
        if(GameManager.Instance._BattleState != PlayerBattleState.InBattle) return;
        GameManager.Instance.CurBattleTarget.BeThrowed(throwEnemyForce);
    }
}
