using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBullet : BulletBase
{
    protected override Vector2 GetBulletDir()
    {
        return -Vector2.right * FlySpeed;
    }
}
