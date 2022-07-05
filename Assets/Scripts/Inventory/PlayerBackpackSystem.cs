using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class PlayerBackpackSystem : PersistentSingletonTool<PlayerBackpackSystem>
{
    private Canvas playerMenuCanvas;
    private Scrollbar backpackScrollBar;
    private Canvas dropButtonCanvas;
    private Image dropButtonImage;

    [SerializeField] private PlayerBackpack playerBackpack;
    [SerializeField] private List<GameObject> itemSlotGOList;
    [SerializeField] private LayerMask uiLayer;
    
    private bool isOpen;
    private bool buttonClick;

    private PlayerInput playerInput;
    private bool sortOver;

    private Color havedColor;
    private Color nullColor;

    public int BackpackCapacity => playerBackpack.Capacity;

    protected override void Awake()
    {
        base.Awake();
        
        playerInput = GetComponent<PlayerInput>();
        
        playerMenuCanvas = GameObject.Find("PlayerMenuCanvas").GetComponent<Canvas>();
        backpackScrollBar = playerMenuCanvas.transform.GetComponentInChildren<Scrollbar>();
        dropButtonCanvas = GameObject.Find("ItemToolButton").GetComponent<Canvas>();
        dropButtonImage = GameObject.Find("DropItemButton").GetComponent<Image>();

        havedColor = Color.white;
        nullColor = new Color(1, 1, 1, 0);
        
        InitializeSlotItemsGO();
        
        playerBackpack.UpdateItemList();
        playerBackpack.UpdateCapacity();
    }

    private void Update()
    {
        if (playerInput.IsMenuSwitchKeyPressed)
        {
            OpenAndCloseBackpack();
        }

        
        // if (isOpen && EventSystem.current.IsPointerOverGameObject())
        // {
        //     Debug.Log(EventSystem.current.gameObject);
        //     if (playerInput.IsRightMousePressed)
        //     {
        //         dropButtonImage.rectTransform.position = EventSystem.current.GetComponent<RectTransform>().position;
        //         buttonClick = !buttonClick;
        //         dropButtonCanvas.enabled = buttonClick;
        //     }
        // }
       
    }
    
    private void InitializeSlotItemsGO()
    {
        itemSlotGOList = new List<GameObject>(GameObject.Find("BackpackGrid").transform.childCount);
        for (int i = 0; i < GameObject.Find("BackpackGrid").transform.childCount; i++)
        { 
            itemSlotGOList.Add(GameObject.Find("BackpackGrid").transform.GetChild(i).gameObject);
        }
    }

    public void OpenAndCloseBackpack()
    {
        isOpen = !isOpen;
        playerMenuCanvas.enabled = isOpen;
        
        ItemDescriptionUI.Instance.ShowTopItemDes(GetTopItemInBackpack());
        
        if (isOpen)
        {
            backpackScrollBar.value = 1;
            
            playerInput.DisableGamePlayInput();
            
            Time.timeScale = 0;
            GameManager.Instance._GameState = GameState.Paused;
        }
        else
        {
            playerInput.EnableGameplayInput();

            Time.timeScale = 1;
            GameManager.Instance._GameState = GameState.Playing;
        }
        
        UpdatePlayerBackpack();
        playerBackpack.UpdateCapacity();
    }

    public void UpdatePlayerBackpack()
    {
        for (int i = 0; i < itemSlotGOList.Count; i++)
        {
            if (i >= playerBackpack.Capacity)
            {
                itemSlotGOList[i].SetActive(false);
            }
        }
        
        for (int i = 0; i < playerBackpack.SlotList.Count; i++)
        {
            if (playerBackpack.SlotList[i].ItemHeld != null && playerBackpack.SlotList[i].HeldCount > 0)
            {
                itemSlotGOList[i].transform.GetChild(0).GetComponent<Image>().sprite = playerBackpack.SlotList[i].ItemHeld.ItemIcon;
                itemSlotGOList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerBackpack.SlotList[i].HeldCount.ToString();

                itemSlotGOList[i].transform.GetChild(0).GetComponent<Image>().color = havedColor;
                itemSlotGOList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = havedColor;
            }
            else
            {
                itemSlotGOList[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                itemSlotGOList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;

                itemSlotGOList[i].transform.GetChild(0).GetComponent<Image>().color = nullColor;
                itemSlotGOList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = nullColor;
            }
        }
    }

    public void AddItemIntoBackPack(Item itemGot)
    {
        if (playerBackpack.curCapacity > playerBackpack.Capacity)
        {
            //当背包容量已经满了时
            //提醒背包已满
        }
        else
        {
            if (playerBackpack.ItemList.Contains(itemGot))
            {
                for (int i = 0; i < playerBackpack.SlotList.Count; i++)
                {
                    if (playerBackpack.SlotList[i].ItemHeld == itemGot)
                    {
                        playerBackpack.SlotList[i].HeldCount++;
                    }
                }
            }
            else
            {//TODO：更好的逻辑？
            
                for (int i = 0; i < playerBackpack.SlotList.Count; i++)
                {
                    if (playerBackpack.SlotList[i].ItemHeld == null)
                    {
                        playerBackpack.SlotList[i].ItemHeld = itemGot;
                        playerBackpack.SlotList[i].HeldCount++;
                        break;
                    }
                }
            }
        }
       
      
        UpdatePlayerBackpack();
        playerBackpack.UpdateItemList();
        playerBackpack.UpdateCapacity();
    }

    public void RemoveItemFromBackpack(Item itemGot)
    {
        for (int i = 0; i < playerBackpack.SlotList.Count;i++)
        {
            if (playerBackpack.SlotList[i].ItemHeld == itemGot)
            {
                playerBackpack.SlotList[i].HeldCount--;
                
                if (playerBackpack.SlotList[i].HeldCount == 0)
                {
                    playerBackpack.SlotList[i].ItemHeld = null;
                }
            }
        }
         
        UpdatePlayerBackpack();
        playerBackpack.UpdateItemList();
        playerBackpack.UpdateCapacity();
    }

    public Item GetItemInBackpack(int index)
    {
        if (playerBackpack.SlotList[index].ItemHeld != null)
            return playerBackpack.SlotList[index].ItemHeld;
        
        return null;
    }

    public Item GetTopItemInBackpack()
    {
        for (int i = 0; i < playerBackpack.SlotList.Count; i++)
        {
            if (playerBackpack.SlotList[i].ItemHeld != null)
            {
                return playerBackpack.SlotList[i].ItemHeld;
            }
        }
        
        return null;
    }

    public void SwitchItem(int curIndex,int targetIndex)
    {
        Item temp = playerBackpack.SlotList[curIndex].ItemHeld;
        int tempNum = playerBackpack.SlotList[curIndex].HeldCount;
        
        Debug.Log(targetIndex);
        if (playerBackpack.SlotList[targetIndex].ItemHeld == null)
        {
            Debug.Log("NoItemInSlot");
            playerBackpack.SlotList[curIndex].ItemHeld = null;
            playerBackpack.SlotList[curIndex].HeldCount = 0;
            
            playerBackpack.SlotList[targetIndex].ItemHeld = temp;
            playerBackpack.SlotList[targetIndex].HeldCount = tempNum;
        }
        else
        {
            playerBackpack.SlotList[curIndex].ItemHeld = playerBackpack.SlotList[targetIndex].ItemHeld;
            playerBackpack.SlotList[curIndex].HeldCount = playerBackpack.SlotList[targetIndex].HeldCount;

            playerBackpack.SlotList[targetIndex].ItemHeld = temp;
            playerBackpack.SlotList[targetIndex].HeldCount = tempNum;
        }
        
        UpdatePlayerBackpack();
        playerBackpack.UpdateItemList();
        
    }
}
