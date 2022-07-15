using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMagicSystem : SingletonTool<PlayerMagicSystem>
{ 
    private Canvas magicHotbarHUDCanvas;
    private Image damageTypeMagicHotbarIcon;
    private Image cureTypeMagicHotbarIcon;
    private Image controlTypeMagicHotbarIcon;

    [Header("UI")]
    [SerializeField] private List<GameObject> magicSlotListInPM = new List<GameObject>();
    
    [Header("法术")]
    [SerializeField] private List<Magic> magicListInPM = new List<Magic>();
    [SerializeField] private DamageMagic curDamageMagic;
    [SerializeField] private CureMagic curCureMagic;
    [SerializeField] private ControlMagic curControlMagic;

    private Color notMasterColor;
    private Color clickedColor;
    private Color notClickedColor;

    public bool IsMagicClicked { get; set; }
    public int MagicIndex { get; set; }

    private Dictionary<string, Magic> magicDic = new Dictionary<string, Magic>();

    protected override void Awake()
    {
        base.Awake();

        notMasterColor = new Color(0f, 0f, 0f, 0.5f);
        clickedColor = new Color(0f, 0f, 0f, 0.4f);
        notClickedColor = new Color(1f, 1f, 1f, 0.4f);

        magicHotbarHUDCanvas = GameObject.Find("PlayerMagicHotbarCanvas_Dynamic").GetComponent<Canvas>();

        damageTypeMagicHotbarIcon = GameObject.Find("CurDamageMagicIcon").GetComponent<Image>();
        cureTypeMagicHotbarIcon = GameObject.Find("CurCureMagicIcon").GetComponent<Image>();
        controlTypeMagicHotbarIcon = GameObject.Find("CurControlMagicIcon").GetComponent<Image>();

        InitializeMagicDic();
    }

    private void OnEnable()
    {
        UpdateMagicListInPM();

        if (magicListInPM[0].isMaster)
        {
            PlayerMenuSystem.Instance.UnlockMagicInterface();//TODO:正常逻辑下不需要该语句块
            UnlockMagicHotbars();
        }
    }

    private void Update()
    {
        if (IsMagicClicked && ComponentProvider.Instance.PlayerInputAvatar.IsMultiFunctionKeyPressed)
        {
            SettingMagicToHotbar();
            CancelMagicClick(MagicIndex);
        }

        if (ComponentProvider.Instance.PlayerInputAvatar.IsDamageMagicKeyReleased && curDamageMagic != null)
        {
            
        }

        if (ComponentProvider.Instance.PlayerInputAvatar.IsCureMagickeyReleased && curCureMagic != null)
        {
            
        }

        if (ComponentProvider.Instance.PlayerInputAvatar.IsControlMagicKeyReleased && curControlMagic != null)
        {
            
        }
    }

    public void InitializeMagicDic()
    {
        for (int i = 0; i < magicListInPM.Count; i++)
        {
            magicDic.Add(magicListInPM[i].Name,magicListInPM[i]);
        }
    }

    public void UpdateMagicListInPM()
    {
        if(magicSlotListInPM.Count != magicListInPM.Count) return;
        
        for (int i = 0; i < magicListInPM.Count; i++)
        {
            magicSlotListInPM[i].transform.GetChild(1).GetComponent<Image>().color = magicListInPM[i].isMaster ? Color.white : notMasterColor;

            magicSlotListInPM[i].transform.GetChild(1).GetComponent<Image>().sprite = magicListInPM[i].Icon;
            magicSlotListInPM[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = magicListInPM[i].Name;
            magicSlotListInPM[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = magicListInPM[i].Des;
        }
    }

    public void UnlockMagicHotbars()
    {
        magicHotbarHUDCanvas.enabled = true;
    }

    public void SettingMagicToHotbar()
    {
        if (!magicListInPM[MagicIndex].isMaster) return;
        
        switch (magicListInPM[MagicIndex].magicType)
        {
            case MagicType.Damage:
                var dMagic = magicListInPM[MagicIndex] as DamageMagic;
                curDamageMagic = dMagic;
                damageTypeMagicHotbarIcon.sprite = curDamageMagic.Icon;
                damageTypeMagicHotbarIcon.color = Color.white;
                break;
            case MagicType.Cure:
                var cMagic = magicListInPM[MagicIndex] as CureMagic;
                curCureMagic = cMagic;
                cureTypeMagicHotbarIcon.sprite = curCureMagic.Icon;
                cureTypeMagicHotbarIcon.color = Color.white;
                break;
            case MagicType.Control:
                var clMagic = magicListInPM[MagicIndex] as ControlMagic;
                curControlMagic = clMagic;
                controlTypeMagicHotbarIcon.sprite = clMagic.Icon;
                controlTypeMagicHotbarIcon.color = Color.white;
                break;         
        }
    }
    
    public void MagicOnClick(int index)
    {
        if (!magicListInPM[index].isMaster) return;
        
        for (int i = 0; i < magicSlotListInPM.Count; i++)
        {
            magicSlotListInPM[i].transform.GetChild(0).GetComponent<Image>().color = index == i ? clickedColor : notClickedColor;
        }
    }

    public void CancelMagicClick(int index)
    {
        magicSlotListInPM[index].transform.GetChild(0).GetComponent<Image>().color = notClickedColor;
    }
    
    
}
