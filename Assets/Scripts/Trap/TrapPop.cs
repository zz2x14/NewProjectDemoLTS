using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPop : TrapBase
{
    [SerializeField] private Vector2 popForce;
    
    private PlayerController player;
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        player = col.GetComponent<PlayerController>();
        anim.Play(animID);
    }

    public override void HitPlayer01()
    {
        if (player != null)
        {
            player.Forced(popForce);
        }
        
    }
}
