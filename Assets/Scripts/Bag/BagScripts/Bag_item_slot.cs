using UnityEngine;
using UnityEngine.UI;
using TMPro;


// 背包物品槽控制器
// 用于在背包UI中显示单个物品的信息

public class BagItemSlot : MonoBehaviour
{
    [Header("UI组件引用")]
    [Tooltip("物品图标显示组件")]
    public Image itemImage;
    [Tooltip("物品数量文本组件")]
    public TextMeshProUGUI quantityText;


    // 设置物品槽显示的内容
    public void SetItem(Item item)
    {
        // 设置物品图标，如果组件存在且物品有图标
        if (itemImage != null) 
            itemImage.sprite = item.itemImage;
        
        // 设置物品数量，格式为"x数量"，如果组件存在
        if (quantityText != null) 
            quantityText.text = "x" + item.itemQuantity.ToString();
    }
}