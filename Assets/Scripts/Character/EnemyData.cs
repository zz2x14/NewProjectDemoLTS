using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/EnemyData/GeneralEnemyData",fileName = "NewEnemyData")]
public class EnemyData : CharacterData
{
    public EnemySelfData enemySelfData;
    
    [SerializeField] private bool isBoss = false;
    public BossData bossData;
    
    public EnemyType enemyType;
    
    public bool IsBoss => isBoss;
}

[System.Serializable]
public class EnemySelfData
{
    public int enemyID;
    public string enemyName;
}

[System.Serializable]
public class BossData
{
    public float attack2Damage;
    public float attack3Damage;
}

public enum EnemyType
{
    SlimeLike,
    GolblinMeleeLike,
    GoblinRangeLike,
    WaspLike,
    TrollLike,
    BeetleLike
}

