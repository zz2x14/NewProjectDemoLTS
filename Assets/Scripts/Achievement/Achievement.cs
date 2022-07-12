using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AchievementType
{
    Count,
    Place
}
public class Achievement : ScriptableObject
{
    public AchievementBaseData baseInfo;
    
    public int AchievementID => baseInfo.achievementID;
    public string AchievementName => baseInfo.achievementName;
    public string AchievementDes => baseInfo.achievementDescription;
    public string AchievementTargetDes => baseInfo.achievementTargetDes;
    public Sprite AchievemetnIcon => baseInfo.achievementIcon;
    public Color AchievementIconBgColor => baseInfo.achievementIconBgColor;
    public AchievementType AchievementType => baseInfo.achievementType;
    public bool IsUnlocked
    {
        get => baseInfo.isUnlocked;
        set => baseInfo.isUnlocked = value;
    }
}

[System.Serializable]
public class AchievementBaseData
{
    public int achievementID;
    public string achievementName;
    [TextArea] public string achievementDescription;
    [TextArea] public string achievementTargetDes;
    public Sprite achievementIcon;
    public Color achievementIconBgColor;
    public AchievementType achievementType;
    public bool isUnlocked;
}
