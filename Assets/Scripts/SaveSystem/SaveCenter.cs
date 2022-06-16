using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCenter
{
    public const string FILE_PLAYERDATA = "PlayerData.txt";
    
    public static void SavePlayerData(CharacterBaseData baseData,string fileName)
    {
        SaveSystemByJson.SaveDataByJson(baseData,fileName);
    }

    public static void LoadPlayerData(CharacterBaseData baseData, string fileName)
    {
        CharacterBaseData data = SaveSystemByJson.LoadDataFromJson<CharacterBaseData>(fileName);
        
        baseData.CurHealth = data.CurHealth;
        baseData.MaxHealth = data.MaxHealth;
        baseData.AttackDamage = data.AttackDamage;
    }
    
}
