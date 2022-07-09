using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CharacterData/NpcData",fileName = "NewNpcData")]
public class NpcData : ScriptableObject
{
    public NpcBaseData npcBaseData;
    public List<NpcTalkDataContainer> containers = new List<NpcTalkDataContainer>();

    private void OnEnable()
    {
        for (int i = 0; i < containers.Count; i++)
        {
            if (containers[i].lockedTalkData != null)
            {
                containers[i].isTalkPrecondition = true;
                containers[i].lockedTalkData.locked = true;
            }
        }
    }
    
}

[Serializable]
public class NpcBaseData
{
    public string npcName;
    public int npcID;
    public Sprite npcPortrait;
}

[Serializable]
public class NpcTalkDataContainer
{
    [SerializeField] private string talkSceneName;
    public int talkID;
    public bool isTalked;
    public bool isForcedTalk;
    public TalkData thisTalkData;

    [Space] 
    public GameChapter matchingChapter;
    
    [Space]
    public bool isTalkPrecondition;
    public TalkData lockedTalkData;

    [Space] 
    public bool isScenePrecondtion;
    public int targetSceneID;

    [Space] 
    public bool willPushForwardGameChapter;
    public GameChapter targetChapter;

    [Space] 
    public bool willUpdateChapterGuide;
    [TextArea] public string updatedChapterGuideContent;

    public void UnlockTargetTalk()
    {
        lockedTalkData.locked = false;
        lockedTalkData = null;
    }
    
    public void UnlockTargerScene(UnlockSceneByTalk unlockSceneByTalk)
    {
        unlockSceneByTalk.UnlockPortalByTalk(targetSceneID);//TODO:优化或者说改进解锁场景的依赖关系
    }
    
    public void PushForwardChapter()
    {
        GameManager.Instance._GameChapter = targetChapter;
    }

    
}
