using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AchievementData/CountAchievementData",fileName = "NewCountAchievementData")]
public class CountAchievement : Achievement
{
    public CountAchievementData countData;
    public int TargetCount => countData.targetCount;
    public CountAcType CountAcType => countData.countAcType;
}

[System.Serializable]
public class CountAchievementData
{
    public int targetCount;
    public CountAcType countAcType;
}

public enum CountAcType
{
    Killed,
    Coin
}
