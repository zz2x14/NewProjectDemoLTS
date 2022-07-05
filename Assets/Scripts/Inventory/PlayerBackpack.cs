using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inventory/NewPlayerBackpack",fileName = "NewPlayerBackpack")]
public class PlayerBackpack : ScriptableObject
{
    public BackpackData backpack;
    public int curCapacity { get; set; } = 0;
    
    public int Capacity
    {
        get => backpack.backpackCapacity;
        set => backpack.backpackCapacity = value;
    }

    public int MaxCapacity => backpack.maxCapacity;

    public List<BackpackSlot> SlotList
    {
        get => backpack.itemSlotsInBackpack;
        set => backpack.itemSlotsInBackpack = value;
    }

    public List<Item> ItemList
    {
        get => backpack.itemsInBackpack;
        set => backpack.itemsInBackpack = value;
    }

    public void UpdateCapacity()
    {
        curCapacity = 0;
        
        for (int i = 0; i < backpack.itemSlotsInBackpack.Count; i++)
        {
            if (backpack.itemSlotsInBackpack[i].ItemHeld != null)
            {
                curCapacity++;
            }
        }
    }
    

    public void ExpandCapacity(int newCapacity)
    {
        Capacity = newCapacity;
    }
    
    public void UpdateItemList()
    {
        ItemList = new List<Item>(backpack.itemSlotsInBackpack.Count);
        
        for (int i = 0; i < backpack.itemSlotsInBackpack.Count; i++)
        {
            if (backpack.itemSlotsInBackpack[i].ItemHeld != null && backpack.itemSlotsInBackpack[i].HeldCount > 0 && !
                    ItemList.Contains(backpack.itemSlotsInBackpack[i].ItemHeld))
            {
                ItemList.Add(backpack.itemSlotsInBackpack[i].ItemHeld);
            }
        }
    }
}

[Serializable]
public class BackpackData
{
    public int backpackCapacity;
    public int maxCapacity = 30;
    public List<BackpackSlot> itemSlotsInBackpack = new List<BackpackSlot>(); 
    public List<Item> itemsInBackpack = new List<Item>();
}

[Serializable]
public class BackpackSlot
{
    [SerializeField] private Item itemHeld;
    [SerializeField] private int heldCount;

    public Item ItemHeld
    {
        get => itemHeld;
        set => itemHeld = value;
    }

    public int HeldCount
    {
        get => heldCount;
        set => heldCount = value;
    }
    
}

