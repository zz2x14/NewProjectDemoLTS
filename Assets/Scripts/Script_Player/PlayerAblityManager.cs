using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAblityManager : MonoBehaviour
{
    public bool DoubleJumpUnlocked { get; set; } = false;
    public bool ThirdAttackUnlocked{ get; set; } = false;
    public bool RollUnlocked{ get; set; } = false;
    public bool ShootUnloced{ get; set; } = false;

    private void Update()
    {
        if (Keyboard.current.capsLockKey.wasPressedThisFrame)
        {
            DoubleJumpUnlocked = !DoubleJumpUnlocked;
            ThirdAttackUnlocked = !ThirdAttackUnlocked;
            RollUnlocked = !RollUnlocked;
            ShootUnloced = !ShootUnloced;
        }
    }
}
