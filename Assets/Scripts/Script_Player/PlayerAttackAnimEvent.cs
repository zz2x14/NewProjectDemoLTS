using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimEvent : MonoBehaviour
{
    private PlayerController myself;

    private void Awake()
    {
        myself = GetComponentInParent<PlayerController>();
    }

    public void Attack1AnimationEvent()
    {
        myself.Attack1();
    }

    public void Attack2AnimationEvent()
    {
        myself.Attack2();
    }

    public void Attack3AnimationEvent()
    {
        myself.Attack3();
    }

    public void JumpAttackAnimationEvent()
    {
        myself.JumpAttack();
    }

  
}
