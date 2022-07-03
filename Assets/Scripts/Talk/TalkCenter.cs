
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkCenter : PersistentSingletonTool<TalkCenter>
{
    [Header("对话UI")] 
    [SerializeField] private TextMeshProUGUI talkingContentText;
    [SerializeField] private Image playerPortrait;
    [SerializeField] private TextMeshProUGUI talkingTargetName;
    [SerializeField] private Image talkingTargetPortrait01;
    [SerializeField] private Image talkingTargetPortrait02;
    
    [Header("文字显现速度")]
    [SerializeField] private float charInterval;
    [SerializeField] private float charSpeedUpInterval;
    [SerializeField] private float talkInterval;

    // [Header("当前对话对象")]
    // [SerializeField] private TalkTarget talkTarget;
    
    public bool IsTalking { get; set; }

    //前者为接收传入的当前对话内容，后者对text直接赋值
    private StringBuilder curTalkContent = new StringBuilder();
    private StringBuilder talkContent = new StringBuilder();//Sign:SB是要初始化的

    private int npcTalkIndex;//Npc"说话"时所用
    private int playerTalkIndex = 1;//玩家"说话"时所用
    private int totalTalkIndex;//计算当前对话总共的次数
    private int wordsNum;//当前一个对话内容的文字数量

    private WaitForSeconds talkIntervalWFS;
    private WaitForSeconds charIntervalWFS;
    private WaitForSeconds charSpeedUpWFS;
    private WaitUntil curTalkContentOver;

    private PlayerInput playerInput;

    private float charSpeed;

    private bool isSpeedUp = false;
    private bool isSkip = false;


    protected override void Awake()
    {
        base.Awake();

        talkIntervalWFS = new WaitForSeconds(talkInterval);
        charIntervalWFS = new WaitForSeconds(charInterval);
        charSpeedUpWFS = new WaitForSeconds(charSpeedUpInterval);

        curTalkContentOver = new WaitUntil(() => talkingContentText.text == talkContent.ToString()); 
        
        playerInput = FindObjectOfType<PlayerInput>();
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        
      
    }

    public void GetTalkingTargetInfo(string targetName,Sprite targetPortrait)
    {
        talkingTargetName.text = targetName;
        talkingTargetPortrait01.sprite = targetPortrait;
    }

    public void GetNpcTalkingContent(List<string> talkingContents)
    {
        curTalkContent.Clear();
        curTalkContent.Append(talkingContents[npcTalkIndex]);
        npcTalkIndex++;
    }

    public void GetPlayerTalkingContent(List<string> talkingContents)
    {
        curTalkContent.Clear();
        curTalkContent.Append(talkingContents[playerTalkIndex - npcTalkIndex - 1]);
    }

    public void SetPlayerTalkingPortrait(Sprite playerMathcingPortrait)
    {
        playerPortrait.sprite = playerMathcingPortrait;
    }

    public void StartTalk(List<string> playerTalkingContents,List<string> talkingContents,NpcController npc)
    {
        IsTalking = true;
        
        StartCoroutine(TalkCor(playerTalkingContents,talkingContents,npc));
    }

    private void Update()
    {
        if (playerInput.IsSpeedUpKeyPerformed)
        {
            isSpeedUp = true;
        }
        else if (playerInput.IsSpeedUpKeyReleased && isSpeedUp)
        {
            isSpeedUp = false;
        }

        if (playerInput.IsSkipTalkKeyPressed)
        {
            isSkip = true;
        }
    }

    //后续考虑以TalkTarget作为参数（敌人和Npc）
    IEnumerator TalkCor(List<string> playerTalkingContents,List<string> npcTalkingContents,NpcController npc)
    {
        talkingTargetPortrait01.color = Color.white;
        playerPortrait.color = Color.white;
        
        npcTalkIndex = 0;
        playerTalkIndex = 1;
        wordsNum = 0;
        //talkTarget = curTalkTarget;//TODO:暂时没有实际意义
        
        while (totalTalkIndex < npcTalkingContents.Count + playerTalkingContents.Count)
        {
            //辨别该Npc说话还是玩家
            if (playerTalkIndex % 2 != 0)
            {
                talkingTargetPortrait01.color = new Color(1, 1, 1, 1f);
                playerPortrait.color = new Color(1, 1 ,1, 0.5f);
                
                GetNpcTalkingContent(npcTalkingContents);
            }
            else if(playerTalkIndex % 2 == 0)
            {
                talkingTargetPortrait01.color = new Color(1, 1 ,1, 0.5f);
                playerPortrait.color = new Color(1, 1, 1, 1f);
                
                GetPlayerTalkingContent(playerTalkingContents);
            }
            
            talkContent.Clear();
            
            while (wordsNum < curTalkContent.Length && !isSkip )
            {
                if (!isSpeedUp)
                {
                    yield return charIntervalWFS;
                }
                else
                {
                    yield return charSpeedUpWFS;
                }
              
                talkContent.Append(curTalkContent[wordsNum]);
                talkingContentText.text = talkContent.ToString();
                wordsNum++;
            }

            //跳过对话设定为直接显示完整当前单个的对话内容
            talkContent.Clear();
            talkContent.Append(curTalkContent);
            talkingContentText.text = talkContent.ToString();
            
            yield return curTalkContentOver;
            yield return talkIntervalWFS;

            talkContent.Clear();
            
            wordsNum = 0;
            playerTalkIndex++;
            totalTalkIndex++;
           
            isSkip = false;
        }

        IsTalking = false;
        npc.GetComponent<ITalk>().TalkOver();
        npc.UnlockTalk();
        npc.UnlockScene();
        npc.PushForwardGameChapter();
        npc.RemoveHasTalkedContent();
        
        talkingContentText.text = null;
        talkingTargetPortrait01.sprite = null;
        playerPortrait.sprite = null;
        totalTalkIndex = 0;
        
        StopAllCoroutines();
    }

}
