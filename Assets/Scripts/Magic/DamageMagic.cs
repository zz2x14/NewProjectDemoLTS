using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicData/DamageMagicData",fileName = "NewDamageMagicData")]
public class DamageMagic : Magic
{
    [SerializeField] private DamageMagicData damageMagicData;
    public float Damage => damageMagicData.damageValue;
}

[System.Serializable]
public class DamageMagicData
{
    public float damageValue;
}
