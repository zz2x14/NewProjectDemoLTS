using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCarry : MonoBehaviour
{
    [SerializeField] private Achievement targetAchievement;

    public void UnlockThisAchievement()
    {
        AchievementSystem.Instance.UnlockAchievement(targetAchievement);
    }

}
