using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    protected TextMeshProUGUI nameText;
    
    [SerializeField] private NpcData npcData;
    [SerializeField] protected bool needName;
    
    [Header("是否移动")]
    [SerializeField] private bool isMover;
    
    [Header("是否产生对话")]
    [SerializeField] private bool willTalk;

    [Header("出现对应章节")] 
    [SerializeField] private bool isAppearing;
    [SerializeField] private GameChapter npcAppearingChapter;

    public bool IsMover => isMover;

    private List<string> curTalkList = new List<string>();
    private NpcTalkDataContainer curTalkContainer;

    private PlayerController player;
    private PlayerInput playerInput;
    public Transform playerPos { get; set; }
    
    private int matchingID;

    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playerPos = player.transform;
        playerInput = playerPos.GetComponent<PlayerInput>();

        npcData = Instantiate(npcData);
        //TODO:现在是为了方便调试 - 后期取消！！！ => 已改用新的对话data方式  => 将两种做法结合后暂定为现在的模式 再次启用调试

        if (needName)
        {
            nameText = GetComponentInChildren<TextMeshProUGUI>();
            nameText.text = npcData.npcBaseData.npcName;
        }
    }

    protected virtual void OnEnable()
    {
        if (isAppearing)
        {
            if (npcAppearingChapter != GameManager.Instance._GameChapter)
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!willTalk) return;
        if (npcData.containers.Count == 0) return;
    
        if (MatchCurChapterForcedTalk())//玩家被强制触发的对话
        {
            player.GoToTalk();
            transform.GetComponent<ITalk>().GoToTalk();
            
            TalkCenter.Instance.GetTalkingTargetInfo(npcData.npcBaseData.npcName,npcData.npcBaseData.npcPortrait);
            
            TalkCenter.Instance.StartTalk(player.GetCurPlayerTalkingContents(matchingID),
                curTalkList,this);
        }
    }
    
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (!willTalk) return;
        if (npcData.containers.Count == 0) return;
        
        if (playerInput.IsGameConfirmKeyPressed && MatchCurChapterTalk())//玩家主动触发的对话
        {
            player.GoToTalk();
            transform.GetComponent<ITalk>().GoToTalk();
            
            TalkCenter.Instance.GetTalkingTargetInfo(npcData.npcBaseData.npcName,npcData.npcBaseData.npcPortrait);
            
            TalkCenter.Instance.StartTalk(player.GetCurPlayerTalkingContents(matchingID),
                curTalkList,this);
        }
    }
    
    public bool MatchCurChapterForcedTalk()
    {
        foreach (var container in npcData.containers)
        {
            if (container.matchingChapter == GameManager.Instance._GameChapter && container.isForcedTalk && !container.isTalked 
                && !container.thisTalkData.locked)
            {
                
                curTalkContainer = container;
                
                matchingID = container.talkID;
                    
                curTalkList = container.thisTalkData.talkContentList;
                
                container.isTalked = true;
                    
                return true;
            }
        }
        return false;
    }
    public bool MatchCurChapterTalk()
    {
        foreach (var container in npcData.containers)
        {
            if (container.matchingChapter == GameManager.Instance._GameChapter && !container.isForcedTalk && !container.isTalked 
                && !container.thisTalkData.locked)
            {
                curTalkContainer = container;

                matchingID = container.talkID;
                    
                curTalkList = container.thisTalkData.talkContentList;
                
                container.isTalked = true;
                    
                return true;
            }
        }
        return false;
    }
    
    public void RemoveHasTalkedContent()
    {
        //Sign:在foreach中尽量避免使用Remove - （使用的情况下）可以使用break马上跳出 - 最好是只在移除单个目标的情况下使用
        //需要大量删除移除时直接使用for循环 （单个也是推荐使用for循环的）
    
        curTalkList = null;
        curTalkContainer = null;
        
        for (int i = 0; i < npcData.containers.Count; i++)
        {
            if (npcData.containers[i].isTalked)
            {
                npcData.containers.Remove(npcData.containers[i]);
            }
        }
    }
    
    public void UnlockTalk()
    {
        if (!curTalkContainer.isTalkPrecondition) return;
        
        curTalkContainer.UnlockTargetTalk();
    }

    public void UnlockScene()
    {
        if (!curTalkContainer.isScenePrecondtion) return;
        
        curTalkContainer.UnlockTargerScene(GetComponent<UnlockSceneByTalk>());
    }

    public void UpdateChapterContent()
    {
        if (!curTalkContainer.willUpdateChapterGuide) return;
        
         ChapterGuideContentUI.Instance.UpdateChapterGuideContent(curTalkContainer.updatedChapterGuideContent);
    }

    public void PushForwardGameChapter()
    {
        if(!curTalkContainer.willPushForwardGameChapter) return;
        
        curTalkContainer.PushForwardChapter();
    }
}
