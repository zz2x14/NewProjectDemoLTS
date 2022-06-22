using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallBullet : BulletBase
{
    [SerializeField] private float fallForce;

    protected override Vector2 GetBulletDir()
    {
        return Vector2.down * fallForce;
    }
}
