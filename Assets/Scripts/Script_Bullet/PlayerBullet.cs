using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase
{
    protected override Vector2 GetBulletDir()
    {
        return Vector2.right * FlyDir * FlySpeed;
    }
}
