using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingMagic : Magic
{
    private CureMagic cureMagic;

    [SerializeField] private string healingIdleAnimName;
    [SerializeField] private ParticleSystem healingParticleSystem;

    private int healingIdleAnimID;

    protected override void Awake()
    {
        base.Awake();

        healingIdleAnimID = Animator.StringToHash(healingIdleAnimName);
        
        cureMagic = magic as CureMagic;
    }

    public void HealingAnimEvent()
    {
        ComponentProvider.Instance.PlayerAvatar.RecoverHealth(cureMagic.CureValue);
        
        anim.Play(healingIdleAnimID);
        
        healingParticleSystem.Play();
    }
    
}
