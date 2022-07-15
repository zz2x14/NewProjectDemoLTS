using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NpcMagic : NpcController
{
    [Header("UI")]
    [SerializeField] private Canvas confirmCanvas;
    [SerializeField] private Canvas magicCanvas;
    [SerializeField] private GameObject confirmMasterMagicFrameGO;
    [SerializeField] private GameObject tipGO;
    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private Button confirmMasterButton;
    [SerializeField] private Button cancelMasterButton;
    [SerializeField] private List<GameObject> magicSlotGOList = new List<GameObject>();
    [SerializeField] private List<Button> magicSlotButtonList = new List<Button>(); 

    [Header("法术")] 
    [SerializeField] private List<Magic> magicList = new List<Magic>();

    private Button closeButton;

    public int CurMagicIndex { get; set; }
    public List<Button> MagicSlotButtons => magicSlotButtonList;

    private const string TIP_COINNOTENOUGH = "金币不足！";
    private const string TIP_MASTERSUCCESSFULLY = "成功精通法术！";

    protected override void Awake()
    {
        base.Awake();

        closeButton = magicCanvas.transform.GetChild(5).GetComponent<Button>();
    }

    private void OnEnable()
    {
         StartCoroutine(nameof(OpenMagicInterfaceCor));

        UpdateMagicSlot();
        
        closeButton.onClick.AddListener(DisableMagicCanvas);
        confirmMasterButton.onClick.AddListener(ConfirmMasterMagic);
        cancelMasterButton.onClick.AddListener(DisableConfirmMasterGO);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        confirmMasterButton.onClick.RemoveAllListeners();
        cancelMasterButton.onClick.RemoveAllListeners();
    }

    IEnumerator OpenMagicInterfaceCor()
    {
        while (gameObject.activeSelf)
        {
            if (confirmCanvas.enabled && ComponentProvider.Instance.PlayerInputAvatar.IsGameConfirmKeyPressed)
            {
                magicCanvas.enabled = true;
                GameManager.Instance.ShoppingStateOpreation(true);
            }

            yield return null;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        
        confirmCanvas.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        confirmCanvas.enabled = false;
    }
    
    public void UpdateMagicSlot()
    {
        if(magicList.Count != magicSlotGOList.Count) return;
        
        for (var i = 0; i < magicList.Count; i++)
        {
            magicSlotGOList[i].transform.GetChild(1).GetComponent<Image>().sprite = magicList[i].Icon;
            magicSlotGOList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = magicList[i].Name;
            magicSlotGOList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = magicList[i].Des;
            
            if (magicList[i].isMaster)
            {
                magicSlotGOList[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                magicSlotGOList[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = null;
                
                magicSlotButtonList[i].GetComponent<MagicSlotButton>().enabled = false;
                magicSlotButtonList[i].enabled = false;
                
            }
            else
            {
                magicSlotGOList[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = magicList[i].Cost + "G";
            }
           
        }
    }

    private void DisableMagicCanvas()
    {
        confirmCanvas.enabled = false;
        magicCanvas.enabled = false;
        confirmMasterMagicFrameGO.SetActive(false);
        GameManager.Instance.ShoppingStateOpreation(false);
    }

    public void EnableConfirmMasterGO()
    {
        confirmMasterMagicFrameGO.gameObject.SetActive(true);
    }

    private void DisableConfirmMasterGO()
    {
        confirmMasterMagicFrameGO.gameObject.SetActive(false);
    }

    private void ConfirmMasterMagic()
    {
        confirmMasterMagicFrameGO.SetActive(false);
        if (magicList[CurMagicIndex].Cost > PlayerBackpackSystem.Instance.CurCoinCount)
        {
            tipText.text = TIP_COINNOTENOUGH;
            tipGO.SetActive(true);
            return;
        }

        tipText.text = TIP_MASTERSUCCESSFULLY;
        tipGO.SetActive(true);
        
        magicList[CurMagicIndex].isMaster = true;
        
        UpdateMagicSlot();

        if (CurMagicIndex == 0)
        {
            PlayerMenuSystem.Instance.UnlockMagicInterface();
            PlayerMagicSystem.Instance.UnlockMagicHotbars();
            PlayerMagicSystem.Instance.UpdateMagicListInPM();
        }
    }
    
}
