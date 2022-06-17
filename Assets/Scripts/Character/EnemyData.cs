using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/EnemyData/GeneralEnemyData",fileName = "NewEnemyData")]
public class EnemyData : CharacterData
{
    public EnemySelfData enemySelfData;
    public EnemyType enemyType;
}

[System.Serializable]
public class EnemySelfData
{
    public int enemyID;
    public string enemyName;
}

public enum EnemyType
{
    SlimeLike,
    GolblinMeleeLike,
    GoblinRangeLike
}

