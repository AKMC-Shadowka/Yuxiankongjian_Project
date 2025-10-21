using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Bag/New Item")]
public class Item : ScriptableObject
{
    public string itemName;  //物品名称
    public Sprite itemImage;
    public int itemQuantity;  //物品数量
    [TextArea]  //存放多行文本
    public string itemInformation; //物品信息

    public bool equip;  //是否装备

    // 物品类型枚举
    public enum ItemType
    {
        Important,  // 重要物品
        Consumable, // 消耗物品
        Other       // 其他物品
    }
    public ItemType itemType; // 物品类型

}
