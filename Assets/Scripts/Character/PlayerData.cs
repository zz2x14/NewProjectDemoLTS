using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CharacterData/PlayerData",fileName = "NewPlayerData")]
public class PlayerData : CharacterData
{
    public PlayerSelfData selfData; 
    
    [Header("Player不同情绪形象")]
    public Sprite playerCalmPortrait;
    public Sprite playerHappyPortrait;
    public Sprite playerSadPortrait;
    public Sprite playerAmazedPortrait;
    public Sprite playerStunningPortrait;
    public Sprite playerAngryPortrait;
    
    [Space]
    public List<PlayerTalkData> playerTalkDatas = new List<PlayerTalkData>();

    private Dictionary<PlayerTalkingEmotion, Sprite> matchingSpriteTable = new Dictionary<PlayerTalkingEmotion, Sprite>(6);
    
    private void OnEnable()
    {
        InitializePlayerTalkPortrait();
    }

    private void InitializePlayerTalkPortrait()
    {
        matchingSpriteTable.Add(PlayerTalkingEmotion.Calm,playerCalmPortrait);
        matchingSpriteTable.Add(PlayerTalkingEmotion.Happy,playerHappyPortrait);
        matchingSpriteTable.Add(PlayerTalkingEmotion.Sad,playerSadPortrait);
        matchingSpriteTable.Add(PlayerTalkingEmotion.Amazed,playerAmazedPortrait);
        matchingSpriteTable.Add(PlayerTalkingEmotion.Stunning,playerStunningPortrait);
        matchingSpriteTable.Add(PlayerTalkingEmotion.Angry,playerAngryPortrait);

        for (int i = 0; i < playerTalkDatas.Count; i++)
        {
            playerTalkDatas[i].playerTalkingPortrait = matchingSpriteTable[playerTalkDatas[i].playerTalkingEmotion];
        }
    }

}

[Serializable]
public class PlayerSelfData
{
    public float shootDamage;
}

[Serializable]
public class PlayerTalkData
{
    public string talkSceneName;
    public bool isTalked;
    
    public GameChapter talkingMatchingChapter;
    public int talkID;

    public PlayerTalkingEmotion playerTalkingEmotion;
    public Sprite playerTalkingPortrait;
    
    [TextArea]public List<string> talkList = new List<string>();
}

public enum PlayerTalkingEmotion
{
    Calm,
    Happy,
    Sad,
    Amazed,
    Stunning,
    Angry
}