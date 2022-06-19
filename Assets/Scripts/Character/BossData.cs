using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/EnemyData/GeneralBossData",fileName = "NewBossData")]
public class BossData : EnemyData
{
    public BossSelfData bossSelfData;
}

[System.Serializable]
public class BossSelfData
{
    public float attack2Damage;
    public float attack3Damage;
}
