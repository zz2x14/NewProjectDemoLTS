using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/PlayerData",fileName = "NewPlayerData")]
public class PlayerData : CharacterData
{
    public PlayerSelfData selfData;
}

[System.Serializable]
public class PlayerSelfData
{
    public float shootDamage;
}