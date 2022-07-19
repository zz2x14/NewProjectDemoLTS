using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private List<MagicDataContainer> magicListInPM = new List<MagicDataContainer>();
    [SerializeField] private DamageMagic curDamageMagic;
    [SerializeField] private CureMagic curCureMagic;
    [SerializeField] private ControlMagic curControlMagic;

    [Space] [SerializeField] private float castMagicDelayTime;

    private Color notMasterColor;
    private Color clickedColor;
    private Color notClickedColor;

    private Color magicNotReadyHotbarColor;

    public bool IsMagicClicked { get; set; }
    public int MagicIndex { get; set; }

    private Dictionary<string, UnityAction<GameObject>> magicCastApproachDic = new Dictionary<string, UnityAction<GameObject>>();

    private  WaitForSeconds castMagicDelayWFS;//TODO:为什么waitUntil只有第一次才生效
    private Coroutine castMagicCor;

    protected override void Awake()
    {
        base.Awake();

        notMasterColor = new Color(0f, 0f, 0f, 0.5f);
        clickedColor = new Color(0f, 0f, 0f, 0.4f);
        notClickedColor = new Color(1f, 1f, 1f, 0.4f);
        magicNotReadyHotbarColor = new Color(1f, 1f, 1f, 0.5f);

        magicHotbarHUDCanvas = GameObject.Find("PlayerMagicHotbarCanvas_Dynamic").GetComponent<Canvas>();

        damageTypeMagicHotbarIcon = GameObject.Find("CurDamageMagicIcon").GetComponent<Image>();
        cureTypeMagicHotbarIcon = GameObject.Find("CurCureMagicIcon").GetComponent<Image>();
        controlTypeMagicHotbarIcon = GameObject.Find("CurControlMagicIcon").GetComponent<Image>();

        InitializeMagicDic();

        castMagicDelayWFS = new WaitForSeconds(castMagicDelayTime);
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (IsMagicClicked && ComponentProvider.Instance.PlayerInputAvatar.IsMultiFunctionKeyPressed)
        {
            SettingMagicToHotbar();
            CancelMagicClick(MagicIndex);
        }

        if (ComponentProvider.Instance.PlayerInputAvatar.IsDamageMagicKeyPressed && curDamageMagic != null && curDamageMagic.isReady)
        {
            if (ContainsMagic(curDamageMagic.Name))
            {
                ComponentProvider.Instance.PlayerAvatar.IsCastMagic = true;
                
                StartCastMagicCor(magicCastApproachDic[curDamageMagic.Name],curDamageMagic.MagicPrefab);
                
                StartMagicCDCor(damageTypeMagicHotbarIcon,curDamageMagic);
            }
        } 
        if (ComponentProvider.Instance.PlayerInputAvatar.IsCureMagickeyPressed && curCureMagic != null && curCureMagic.isReady)
        {
            if (ContainsMagic(curCureMagic.Name))
            {
                ComponentProvider.Instance.PlayerAvatar.IsCastMagic = true;
                
                StartCastMagicCor(magicCastApproachDic[curCureMagic.Name],curCureMagic.MagicPrefab);
                
                StartMagicCDCor(cureTypeMagicHotbarIcon,curCureMagic);
            }
        }
        if (ComponentProvider.Instance.PlayerInputAvatar.IsControlMagicKeyPressed && curControlMagic != null && curControlMagic.isReady)
        {
            if (ContainsMagic(curControlMagic.Name))
            {
                ComponentProvider.Instance.PlayerAvatar.IsCastMagic = true;
                
                StartCastMagicCor(magicCastApproachDic[curControlMagic.Name],curControlMagic.MagicPrefab);
                
                StartMagicCDCor(controlTypeMagicHotbarIcon,curControlMagic);
            }
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
    
    public void InitializeMagicDic()
    {
        foreach (var magic in magicListInPM)
        {
            switch (magic.magicApproach)
            {
                case MagicApproach.General:
                    magicCastApproachDic.Add(magic.Name,CastMagicGeneral);
                    break;
                case MagicApproach.OnPlayer:
                    magicCastApproachDic.Add(magic.Name,CastMagicOnPlayerPos);
                    break;
                case MagicApproach.OnEnemy:
                    magicCastApproachDic.Add(magic.Name,CastMagicOnEnemyPos);
                    break;
            }
        }
    }

    public bool ContainsMagic(string magicName)
    {
        return magicCastApproachDic.ContainsKey(magicName);
    }

    private void StartCastMagicCor(UnityAction<GameObject> castMagicFunc,GameObject magicPrefab)
    {
        if (castMagicCor != null)
        {
            StopCoroutine(castMagicCor);
        }

        castMagicCor = StartCoroutine(CastMagicCor(castMagicFunc, magicPrefab));
    }
    IEnumerator CastMagicCor(UnityAction<GameObject> castMagicFunc,GameObject magicPrefab)
    {
        yield return castMagicDelayWFS;
        
        castMagicFunc.Invoke(magicPrefab);
    }

    public void CastMagicGeneral(GameObject magicPrefab)
    {
        PoolManager.Instance.Release(magicPrefab,ComponentProvider.Instance.PlayerAvatar.CastMagicPoint);
    }
    public void CastMagicOnPlayerPos(GameObject magicPrefab)
    {
        PoolManager.Instance.Release(magicPrefab,ComponentProvider.Instance.PlayerAvatar.MagicEffectivePos);
    }
    public void CastMagicOnEnemyPos(GameObject magicPrefab)
    {
        PoolManager.Instance.Release(magicPrefab,
            GameManager.Instance._BattleState == PlayerBattleState.InBattle ? 
                GameManager.Instance.CurBattleTargetPosWithOffset:GameManager.Instance.FindOneTargetPosWithOffset());
    }

    private void StartMagicCDCor(Image curMagicHorbarIcon,MagicDataContainer magic)
    {
        StartCoroutine(WaitMagicCDCor(curMagicHorbarIcon,magic));
    }
    IEnumerator WaitMagicCDCor(Image curMagicHorbarIcon,MagicDataContainer magic)
    {
        magic.isReady = false;
        curMagicHorbarIcon.color = magicNotReadyHotbarColor;
        
        yield return new WaitForSeconds(magic.ColdDown);

        magic.isReady = true;
        curMagicHorbarIcon.color = Color.white;
    }
}
