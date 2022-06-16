using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/GeneralEnemyData",fileName = "NewEnemyData")]
public class EnemyData : CharacterData
{
    public EnemySelfData enemySelfData;
}

[System.Serializable]
public class EnemySelfData
{
    public int enemyID;
    public string enemyName;
}

