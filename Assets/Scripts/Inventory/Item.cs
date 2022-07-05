using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/NewItem",fileName = "NewItem")]
public class Item : ScriptableObject
{
    [SerializeField] private ItemData thisItem;
    public List<Channel> channels = new List<Channel>();
    public Color descriptionColor;
    
    public int ItemID => thisItem.itemID;
    public string ItemName => thisItem.itemName;
    public string ItemDes => thisItem.itemDescription;
    public bool Consumable => thisItem.consumable;
    public Sprite ItemIcon => thisItem.itemIcon;
    public int SellPrice => thisItem.sellPrice;

}

[System.Serializable]
public class ItemData
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    
    [TextArea] public string itemDescription;
    
    public bool consumable;

    public int sellPrice;
}

public enum Channel
{
    掉落,
    交易,
}

public class ItemType
{
    
}


