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
    private Canvas itemDesCanvas;
    private Scrollbar backpackScrollBar;
    
    [Header("Player背包")]
    [SerializeField] private PlayerBackpack playerBackpack;
    [SerializeField] private List<GameObject> itemSlotGOList;
    
    [Header("ItemDes出现位置偏差")]
    [SerializeField] private Vector2 itemDesOffset;
    
    private bool isOpen;
    private bool buttonClick;

    private PlayerInput playerInput;
    private bool sortOver;

    private Color havedColor;
    private Color nullColor;

    public int BackpackCapacity => playerBackpack.Capacity;

    public bool IsDropKeyPressed => playerInput.IsDropItemKeyPressed;

    public float OffsetX => itemDesOffset.x;
    public float OffsetY => itemDesOffset.y;
    
    public int coinGotCount { get; set; }
    

    protected override void Awake()
    {
        base.Awake();
        
        playerInput = GetComponent<PlayerInput>();
        
        playerMenuCanvas = GameObject.Find("PlayerMenuCanvas").GetComponent<Canvas>();
        itemDesCanvas = GameObject.Find("ItemDescriptionDynamicCanvas").GetComponent<Canvas>();
        backpackScrollBar = playerMenuCanvas.transform.GetComponentInChildren<Scrollbar>();

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
        
        //TODO:暂时不直接显示第一个物品的信息
        //ItemDescriptionUI.Instance.ShowTopItemDes(GetTopItemInBackpack());
        ItemDescriptionUI.Instance.ClearItemDes();
        
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
            //Pseudocode：
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

    public void GetItemMultiple(Item item)
    {
        for (int i = 0; i < coinGotCount; i++)
        {
            AddItemIntoBackPack(item);
        }
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
        
        if (playerBackpack.SlotList[targetIndex].ItemHeld == null)
        {
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

    public void EnableItemDesImage()
    {
        itemDesCanvas.enabled = true;
    }
    
    public void DisableItemDesImage()
    {
        itemDesCanvas.enabled = false;
    }

    public void SetItemDesImagePos(Vector3 targetPos)
    {
        itemDesCanvas.transform.position = targetPos;//Sign:是可以直接让transform转为RectTransform的
    }
}
