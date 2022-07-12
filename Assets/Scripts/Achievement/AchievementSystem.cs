using System;
using System.Collections.Generic;
using UnityEngine;
using MyEventSpace;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class AchievementSystem : PersistentSingletonTool<AchievementSystem>
{
    private GameObject uiContainer;
    private const string ANIMNAME_UIENABLE = "AchievementShowUIEnable";
    private int uiEnableAnimID;

    private const string NAME_ACCONTAINERGO = "AchievementListContentContainer";
    [SerializeField] private List<Transform> achievementSlotList = new List<Transform>();
    private RectTransform acListContainer;
    private float unitHeight;
    private float unitSpacing;

    [SerializeField] private List<Achievement> allAchievementList = new List<Achievement>();

    [Header("成就UI")] 
    [SerializeField] private Canvas UIShowCanvas;
    [SerializeField] private Image achievementIconBg;
    [SerializeField] private Image achievementIcon;
    [SerializeField] private TextMeshProUGUI curUINameText;
    [SerializeField] private TextMeshProUGUI curUIDesText;
    
    [Header("敌人击杀数量成就")] 
    [SerializeField] private int totalEnemyKilledCount;
    [SerializeField] private List<CountAchievement> killedAcList = new List<CountAchievement>();

    [Header("累计获得金币数量成就")] 
    [SerializeField] private int totalCoinGainedCount;
    [SerializeField] private List<CountAchievement> coinAcList = new List<CountAchievement>();

    private float killedProgressValue;
    private float coinProgressValue;
   
    protected override void Awake()
    {
        base.Awake();

        uiContainer = UIShowCanvas.transform.GetChild(0).gameObject;

        uiEnableAnimID = Animator.StringToHash(ANIMNAME_UIENABLE);

        InitializeAcSlotContainer();
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEventHandlerListener(EventName.OnEnemyDeath,EnemyKilledCountIncrease);
        EventManager.Instance.AddEventHandlerListener(EventName.OnPlayerMenuOpen,UpdateAchievementList);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventHandlerListener(EventName.OnEnemyDeath,EnemyKilledCountIncrease);
        EventManager.Instance.RemoveEventHandlerListener(EventName.OnPlayerMenuOpen,UpdateAchievementList);
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            UnlockAchievement(killedAcList[0]);
        }
        
    }

    public void InitializeAcSlotContainer()
    {
        acListContainer = GameObject.Find(NAME_ACCONTAINERGO).GetComponent<RectTransform>();
        unitHeight = acListContainer.GetComponent<GridLayoutGroup>().cellSize.y;
        unitSpacing = acListContainer.GetComponent<GridLayoutGroup>().spacing.y;
        
        for (int i = 0; i < acListContainer.childCount; i++)
        {
            achievementSlotList.Add(acListContainer.GetChild(i));
        }
        
        acListContainer.sizeDelta = new Vector2(acListContainer.rect.width, acListContainer.childCount * (unitHeight + unitSpacing));
        //Sign:修改RectTransform相关的直接得到物体上其组件即可，不用从UI身上获得；修改大小直接通过sizeDelta修改
    }

    private void EnemyKilledCountIncrease(object sender,EventArgs e)
    {
        totalEnemyKilledCount++;

        CheckCountAchievement(totalEnemyKilledCount,killedAcList);
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
        achievementIconBg.color = color;
        achievementIcon.sprite = icon;
        curUINameText.text = acName;
        curUIDesText.text = acDes;
        
        uiContainer.GetComponent<Animator>().Play(uiEnableAnimID);
        UIShowCanvas.GetComponent<AutomaticDisableCanvasTool>().StartAutomaticCor();
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

    private void UpdateAchievementList(object sender,EventArgs e)
    {
        if (achievementSlotList.Count != allAchievementList.Count)
        {
#if UNITY_EDITOR
            Debug.LogWarning("成就数量和成就列表数量不对等");
#endif
            return;
        }
        
        for (int i = 0; i < allAchievementList.Count; i++)
        {
            achievementSlotList[i].GetChild(8).gameObject.SetActive(allAchievementList[i].IsUnlocked);

            if (allAchievementList[i].AchievementType == AchievementType.Count)
            {
                achievementSlotList[i].GetChild(5).gameObject.SetActive(true);
                achievementSlotList[i].GetChild(6).gameObject.SetActive(true);
                achievementSlotList[i].GetChild(7).gameObject.SetActive(true);

                var countAc = allAchievementList[i] as CountAchievement;
                
                switch (countAc.CountAcType)
                {
                    case CountAcType.Killed:
                        killedProgressValue = totalEnemyKilledCount / countAc.TargetCount;
                        achievementSlotList[i].GetChild(6).GetComponent<Image>().fillAmount = killedProgressValue;
                        achievementSlotList[i].GetChild(7).GetComponent<TextMeshProUGUI>().text = killedProgressValue.ToString("P0");
                        break;
                    case CountAcType.Coin:
                        coinProgressValue = totalCoinGainedCount / countAc.TargetCount;
                        achievementSlotList[i].GetChild(6).GetComponent<Image>().fillAmount = coinProgressValue;
                        achievementSlotList[i].GetChild(7).GetComponent<TextMeshProUGUI>().text = coinProgressValue.ToString("P0");
                        break;
                }
            }
            else
            {
                achievementSlotList[i].GetChild(5).gameObject.SetActive(false);
                achievementSlotList[i].GetChild(6).gameObject.SetActive(false);
                achievementSlotList[i].GetChild(7).gameObject.SetActive(false);
            }
            
            achievementSlotList[i].GetChild(0).GetComponent<Image>().color = allAchievementList[i].AchievementIconBgColor;
            achievementSlotList[i].GetChild(1).GetComponent<Image>().sprite = allAchievementList[i].AchievemetnIcon;
            achievementSlotList[i].GetChild(2).GetComponent<TextMeshProUGUI>().text = allAchievementList[i].AchievementName;
            achievementSlotList[i].GetChild(3).GetComponent<TextMeshProUGUI>().text = allAchievementList[i].AchievementDes;
            achievementSlotList[i].GetChild(4).GetComponent<TextMeshProUGUI>().text = allAchievementList[i].AchievementTargetDes;
            
            
        }
    }

}

