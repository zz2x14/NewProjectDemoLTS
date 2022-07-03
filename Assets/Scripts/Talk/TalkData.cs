using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//只负责提供对话内容和是否上锁功能
[CreateAssetMenu(menuName = "TalkData/NpcTalkData",fileName = "NewNpcTalkData")]
public class TalkData : ScriptableObject
{
    public bool locked;
    
    [TextArea] public List<string> talkContentList = new List<string>();
}
