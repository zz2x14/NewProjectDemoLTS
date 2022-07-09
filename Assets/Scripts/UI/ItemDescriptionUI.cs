using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ItemDescriptionUI : SingletonTool<ItemDescriptionUI>
{
    [SerializeField] private TextMeshProUGUI curItemNameText;
    [SerializeField] private TextMeshProUGUI curItemDesText;
    [SerializeField] private TextMeshProUGUI curItemPriceText;
    [SerializeField] private TextMeshProUGUI curItemChannelText;

    private StringBuilder channelSB = new StringBuilder();

    public void ShowCurItemDescription(Item curItem)
    {
        curItemNameText.text = curItem.ItemName;
        curItemNameText.color = curItem.descriptionColor;
        curItemDesText.text = curItem.ItemDes;
        curItemPriceText.text = curItem.ItemID != 0 ? $"价值{curItem.SellPrice}金币！" : "1金币就是1金币。";
        curItemChannelText.text = "来源途径：";
        
        for (int i = 0; i < curItem.channels.Count; i++)
        {
            channelSB.Clear();
           
            if (i == curItem.channels.Count - 1)
            {
                channelSB.Append(curItem.channels[i]);
            }
            else
            {
                channelSB.Append(curItem.channels[i]  + "、");
            }
            
            curItemChannelText.text += channelSB;
        }
    }

    // public void ShowTopItemDes(Item topItemInBackpack)
    // {
    //     ShowCurItemDescription(topItemInBackpack);
    // }

    public void ClearItemDes()
    {
        curItemNameText.text = null;
        curItemDesText.text = null;
        curItemPriceText.text = null;
        curItemChannelText.text = null;
    }

}
