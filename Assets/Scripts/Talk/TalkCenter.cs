using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkCenter : PersistentSingletonTool<TalkCenter>
{
    [Header("对话UI")] 
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Image playerPortrait;
    [SerializeField] private TextMeshProUGUI talkingTargerName;
    [SerializeField] private Image talkingTargetPortrait01;
    [SerializeField] private Image talkingTargetPortrait02;
    
    public bool IsTalking { get; set; } = false;

    private StringBuilder curTalkContent;

}
