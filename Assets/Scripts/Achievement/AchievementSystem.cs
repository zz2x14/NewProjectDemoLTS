using System;
using System.Collections.Generic;
using UnityEngine;
using MyEventSpace;
using TMPro;
using UnityEngine.UI;


public class AchievementSystem : PersistentSingletonTool<AchievementSystem>
{
    private GameObject uiContainer;
    private const string ANIMNAME_UIENABLE = "AchievementShowUIEnable";
    private const string ANIMNAME_UIDISABLE = "AchievementShowUIDisable";
    private int uiEnableAnimID;
    private int uiDisableAnimID;
    
    [Header("成就UI")] 
    [SerializeField] private Canvas UIShowCanvas;
    [SerializeField] private Image achievementIconBg;
    [SerializeField] private Image achievementIcon;
    [SerializeField] private TextMeshProUGUI curUINameText;
    [SerializeField] private TextMeshProUGUI curUIDesText;
    
    [Header("敌人击杀数量成就")] 
    [SerializeField] private int curEnemyKilledCount;
    [SerializeField] private List<CountAchievement> killedAcList = new List<CountAchievement>();

    [Header("累计获得金币数量成就")] 
    [SerializeField] private int totalCoinGainedCount;
    [SerializeField] private List<CountAchievement> coinAcList = new List<CountAchievement>();

    protected override void Awake()
    {
        base.Awake();

        uiContainer = UIShowCanvas.transform.GetChild(0).gameObject;

        uiEnableAnimID = Animator.StringToHash(ANIMNAME_UIENABLE);
        uiDisableAnimID = Animator.StringToHash(ANIMNAME_UIDISABLE);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEventHandlerListener(EventName.OnEnemyDeath,EnemyKilledCountIncrease);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventHandlerListener(EventName.OnEnemyDeath,EnemyKilledCountIncrease);
    }

    private void EnemyKilledCountIncrease(object sender,EventArgs e)
    {
        curEnemyKilledCount++;

        CheckCountAchievement(curEnemyKilledCount,killedAcList);
    }

    public void TotalCoinCountIncrease()//此处为1对1，就暂没有使用订阅
    {
        totalCoinGainedCount++;
        
        CheckCountAchievement(totalCoinGainedCount,coinAcList);
    }
    
    
    public void UnlockAchievement(Achievement achievement)
    {
        achievement.IsUnlocked = true;
        
        UpdateAchievementShowUI(achievement.AchievementName,achievement.AchievementDes,
            achievement.AchievementIconBgColor,achievement.AchievemetnIcon);
    }

    private void UpdateAchievementShowUI(string acName,string acDes,Color color,Sprite icon)
    {
        UIShowCanvas.GetComponent<AutomaticDisableCanvasTool>().StartAutomaticCor();
        uiContainer.GetComponent<Animator>().Play(uiEnableAnimID);
        
        achievementIconBg.color = color;
        achievementIcon.sprite = icon;
        curUINameText.text = acName;
        curUIDesText.text = acDes;
    }

    public void CheckCountAchievement(int targetCount,List<CountAchievement> countAchievements)
    {
        for (int i = 0; i < countAchievements.Count; i++)
        {
            if (!countAchievements[i].IsUnlocked)
            {
                if (targetCount == countAchievements[i].TargetCount)
                {
                    UnlockAchievement(countAchievements[i]);
                }
            }
        }
    }

}

