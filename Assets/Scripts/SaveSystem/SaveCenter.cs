using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveCenter
{
    private const string FILE_PLAYERBASEDATA = "PlayerBaseData.txt";
    private const string FILE_PLAYERSELFDATA = "PlayerSelfData.txt";
    
    public static void SavePlayerData(PlayerData playerData)
    {
        SaveSystemByJson.SaveDataByJson(playerData.baseData,FILE_PLAYERBASEDATA);
        SaveSystemByJson.SaveDataByJson(playerData.selfData,FILE_PLAYERSELFDATA);
    }

    public static CharacterBaseData GetPlayerBaseData()
    {
        CharacterBaseData baseData = SaveSystemByJson.LoadDataFromJson<CharacterBaseData>(FILE_PLAYERBASEDATA);
        return baseData;
    }
    public static PlayerSelfData GetPlayerSelfData()
    {
        PlayerSelfData selfData = SaveSystemByJson.LoadDataFromJson<PlayerSelfData>(FILE_PLAYERSELFDATA);
        return selfData;
    }

   


    
}
