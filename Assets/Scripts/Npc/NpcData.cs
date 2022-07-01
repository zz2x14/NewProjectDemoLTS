using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/NpcData",fileName = "NewNpcData")]
public class NpcData : ScriptableObject
{
    public NpcBaseData npcBaseData;
    public List<NpcTalkData> npcTalkDatas = new List<NpcTalkData>();
}

[System.Serializable]
public class NpcBaseData
{
    public string npcName;
    public int npcID;
    public Sprite npcPortrait;
}

[System.Serializable]
public class NpcTalkData
{
    public bool isForecedTalk;
    public bool isTalked;
    public int talkID;

    public GameChapter matchingChapter;
    [TextArea] public List<string> talkContentList = new List<string>();
}
