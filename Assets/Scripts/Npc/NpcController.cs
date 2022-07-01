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
    
    [SerializeField] private bool willTalk;
    
    [SerializeField] private bool isMover;
    
    public bool IsMover => isMover;

    private List<string> curTalkList = new List<string>();

    private PlayerController player;
    private PlayerInput playerInput;
    public Transform playerPos { get; set; }
    
    private int machingID;

    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playerPos = player.transform;
        playerInput = playerPos.GetComponent<PlayerInput>();

        npcData = Instantiate(npcData);
        //TODO:现在是为了方便调试 - 后期取消！！！

        if (needName)
        {
            nameText = GetComponentInChildren<TextMeshProUGUI>();
            nameText.text = npcData.npcBaseData.npcName;
        }
    }
   
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!willTalk) return;
        if (npcData.npcTalkDatas.Count == 0) return;
 
        if (MatchCurChapterForcedTalk())//玩家被强制触发的对话
        {
            player.GoToTalk();
            transform.GetComponent<ITalk>().GoToTalk();
            
            TalkCenter.Instance.GetTalkingTargetInfo(npcData.npcBaseData.npcName,npcData.npcBaseData.npcPortrait);
            TalkCenter.Instance.StartTalk(player.GetCurPlayerTalkingContents(machingID),
                curTalkList,GetComponent<TalkTarget>());
        }
    }

    public  void OnTriggerStay2D(Collider2D other)
    {
        if (playerInput.IsGameConfirmKeyPressed && MatchCurChapterTalk())//玩家主动触发的对话
        {
            player.GoToTalk();
            transform.GetComponent<ITalk>().GoToTalk();
            
            TalkCenter.Instance.GetTalkingTargetInfo(npcData.npcBaseData.npcName,npcData.npcBaseData.npcPortrait);
            TalkCenter.Instance.StartTalk(player.GetCurPlayerTalkingContents(machingID),
                curTalkList,GetComponent<TalkTarget>());
        }
    }
    
    public bool MatchCurChapterForcedTalk()
    {
        foreach (var npcTalk in npcData.npcTalkDatas)
        {
            if (npcTalk.matchingChapter == GameManager.Instance._GameChapter && npcTalk.isForecedTalk)
            {
                curTalkList = npcTalk.talkContentList;

                if (!npcTalk.isTalked)
                {
                    machingID = npcTalk.talkID;
                    npcTalk.isTalked = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool MatchCurChapterTalk()
    {
        foreach (var npcTalk in npcData.npcTalkDatas)
        {
            if (npcTalk.matchingChapter == GameManager.Instance._GameChapter && !npcTalk.isForecedTalk)
            {
                curTalkList = npcTalk.talkContentList;

                if (!npcTalk.isTalked)
                {
                    machingID = npcTalk.talkID;
                    npcTalk.isTalked = true;
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveHasTalkedContent()
    {
        //Sign:在foreach中尽量避免使用Remove - （使用的情况下）可以使用break马上跳出 - 最好是只在移除单个目标的情况下使用
        //需要大量删除移除时直接使用for循环 （单个也是推荐使用for循环的）
        
        for (int i = 0; i < npcData.npcTalkDatas.Count; i++)
        {
            if (npcData.npcTalkDatas[i].isTalked)
            {
                npcData.npcTalkDatas.Remove(npcData.npcTalkDatas[i]);
            }
        }
        
        // foreach (var npcTalk in npcData.npcTalkDatas)
        // {
        //     if (npcTalk.isTalked)
        //     {
        //         npcData.npcTalkDatas.Remove(npcTalk);
        //         break;
        //     }
        // }
    }
}
