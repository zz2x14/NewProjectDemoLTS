using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AchievementData/CountAchievementData",fileName = "NewCountAchievementData")]
public class CountAchievement : Achievement
{
    public CountAchievementData countData;
    public int TargetCount => countData.targetCount;
}

[System.Serializable]
public class CountAchievementData
{
    public int targetCount;
}
