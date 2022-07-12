using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapIdle : TrapBase
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<ITakenDamage>().TakenDamage(trapDamage);
    }
    
    private void OnCollisionStay2D(Collision2D col)
    {
        col.gameObject.GetComponent<ITakenDamage>().TakenDamage(trapDamage);
    }
    
}
