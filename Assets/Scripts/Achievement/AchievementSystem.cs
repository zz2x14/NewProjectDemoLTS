using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AchievementSystem : PersistentSingletonTool<AchievementSystem>
{
    public Achievement firstAchievement;
    [SerializeField] private Achievement enemyKilled50;
    [SerializeField] private int enemyKilledCount;

    private void UnlockNewAchievenment(Achievement newAch)
    {
        
    }
    
}

[System.Serializable]
public class Achievement
{
    public string name;
    [TextArea] public string description;
    public bool unlocked = false;
}