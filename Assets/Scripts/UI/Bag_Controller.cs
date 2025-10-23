using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// 背包系统数据控制器
/// 负责物品管理、NPC交互等背包数据逻辑

public class Bag_Controller : MonoBehaviour
{
    [Header("背包UI组件")]
    public GameObject Bag_UI;  // 背包UI对象（只读引用，由UI_Controller控制显示）
    public Transform itemGrid; // 物品显示网格
    public GameObject itemSlotPrefab; // 物品槽预制体
    
    [Header("分类按钮")]
    public Button btnAll;
    public Button btnImportant;
    public Button btnConsumable;
    public Button btnOther;

    [Header("物品详情面板")]
    public GameObject itemDetailPanel;
    // public TextMeshProUGUI detailItemDescription;
    public Text detailItemDescription;
    public Button useButton;

    [Header("背包数据")]
    public Bag playerBag; // 玩家背包引用

    // 当前显示类型
    private Item.ItemType currentDisplayType = Item.ItemType.Other;
    private bool showAllItems = true;
    private Item selectedItem; // 当前选中的物品

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 处理玩家拾取物品
        if (other.gameObject.CompareTag("Player"))
        {
            Bag_on_Map itemOnMap = other.GetComponent<Bag_on_Map>();
            if (itemOnMap != null)
            {
                AddNewItem(itemOnMap.thisItem);
                Destroy(itemOnMap.gameObject);
            }
        }
        
        // 处理NPC交互
        if (other.gameObject.CompareTag("NPC"))
        {
            Bag_on_Map itemOnMap = other.GetComponent<Bag_on_Map>();
            if (itemOnMap != null)
            {
                TryGiveItemToNPC(itemOnMap.thisItem, other.gameObject);
                Destroy(itemOnMap.gameObject);
            }
        }
    }

    
    /// 添加新物品到背包
    public void AddNewItem(Item item)
    {
        if (!playerBag.itemList.Contains(item))
        {
            playerBag.itemList.Add(item);
            Debug.Log("获得新物品：" + item.itemName);
        }
        else
        {
            item.itemQuantity++;
            Debug.Log("物品数量增加：" + item.itemName + " x" + item.itemQuantity);
        }
        
        // 刷新背包显示
        ForceRefreshBag();
    }

    
    /// 尝试将物品送给 NPC
    public void TryGiveItemToNPC(Item item, GameObject npc)
    {
        NPC_NeedItem npcNeed = npc.GetComponent<NPC_NeedItem>();
        if (npcNeed != null && npcNeed.requiredItem == item)
        {
            if (playerBag.itemList.Contains(item))
            {
                if (item.itemQuantity > 1)
                {
                    item.itemQuantity--;
                }
                else
                {
                    playerBag.itemList.Remove(item);
                }
                Debug.Log("物品已送给 NPC：" + item.itemName);
                ForceRefreshBag();
            }
        }
        else
        {
            Debug.Log("NPC 不需要这个物品");
            // 不匹配，物品不退回到背包（已被销毁）
        }
    }

    
    /// 初始化背包分类按钮
    private void InitializeBagButtons()
    {
        if (btnAll != null) 
        {
            btnAll.onClick.RemoveAllListeners();
            // 修复：ALL按钮应该显示所有物品，不限制类型
            btnAll.onClick.AddListener(() => ShowAllItems());
        }
        
        if (btnImportant != null) 
        {
            btnImportant.onClick.RemoveAllListeners();
            btnImportant.onClick.AddListener(() => ShowItemsByCategory(Item.ItemType.Important));
        }
        
        if (btnConsumable != null) 
        {
            btnConsumable.onClick.RemoveAllListeners();
            btnConsumable.onClick.AddListener(() => ShowItemsByCategory(Item.ItemType.Consumable));
        }
        
        if (btnOther != null) 
        {
            btnOther.onClick.RemoveAllListeners();
            btnOther.onClick.AddListener(() => ShowItemsByCategory(Item.ItemType.Other));
        }
    }

    
    /// 显示所有物品（ALL分类）
    public void ShowAllItems()
    {
        showAllItems = true;
        currentDisplayType = Item.ItemType.Other; // 这个值在showAllItems为true时不会被使用
        RefreshBagDisplay();
        HideItemDetail(); // 切换分类时自动关闭详情面板
    }

    
    /// 按分类显示物品
    public void ShowItemsByCategory(Item.ItemType type, bool showAll = false)
    {
        currentDisplayType = type;
        showAllItems = showAll;
        RefreshBagDisplay();
        HideItemDetail(); // 切换分类时自动关闭详情面板
    }

    
    /// 刷新背包显示
    public void RefreshBagDisplay()
    {
        if (itemGrid == null || playerBag == null) return;
        ClearItemGrid();

        foreach (var item in playerBag.itemList)
        {
            if (showAllItems || item.itemType == currentDisplayType)
            {
                CreateItemSlot(item);
            }
        }
        
        // 刷新后如果选中的物品不在当前分类中，则关闭详情面板
        if (selectedItem != null && !IsItemInCurrentCategory(selectedItem))
        {
            HideItemDetail();
        }
    }

    
    /// 检查物品是否在当前显示分类中
    private bool IsItemInCurrentCategory(Item item)
    {
        return showAllItems || item.itemType == currentDisplayType;
    }


    /// 清空物品网格
    private void ClearItemGrid()
    {
        if (itemGrid != null)
        {
            foreach (Transform child in itemGrid)
            {
                Destroy(child.gameObject);
            }
        }
    }

    
    /// 创建物品槽
    private void CreateItemSlot(Item item)
    {
        if (itemSlotPrefab == null || itemGrid == null) return;

        GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
        var slotController = slot.GetComponent<BagItemSlot>();
        if (slotController != null)
        {
            slotController.SetItem(item);
            
            // 添加点击事件
            Button slotButton = slot.GetComponent<Button>();
            if (slotButton == null)
            {
                slotButton = slot.AddComponent<Button>();
            }
            
            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(() => OnItemSlotClicked(item));
        }
    }
    
    /// 物品槽点击事件处理
    private void OnItemSlotClicked(Item item)
    {
        // 如果点击的是当前已选中的物品，则切换详情面板显示状态
        if (selectedItem == item && itemDetailPanel.activeSelf)
        {
            HideItemDetail();
        }
        else
        {
            ShowItemDetail(item);
        }
    }

    
    /// 显示物品详情
    public void ShowItemDetail(Item item)
    {
        selectedItem = item;
        
        if (itemDetailPanel != null)
        {
            itemDetailPanel.SetActive(true);
                
            if (detailItemDescription != null)
                detailItemDescription.text = item.itemInformation;
            
            // 根据物品类型显示或隐藏使用按钮
            if (useButton != null)
            {
                useButton.gameObject.SetActive(item.itemType == Item.ItemType.Important);
                
                // 设置使用按钮的点击事件
                useButton.onClick.RemoveAllListeners();
                useButton.onClick.AddListener(UseSelectedItem);
            }
        }
    }

    
    /// 隐藏物品详情
    public void HideItemDetail()
    {
        if (itemDetailPanel != null)
        {
            itemDetailPanel.SetActive(false);
            selectedItem = null;
        }
    }

    
    /// 使用选中的物品
    public void UseSelectedItem()
    {
        if (selectedItem != null && selectedItem.itemType == Item.ItemType.Important)
        {
            Debug.Log("使用重要物品: " + selectedItem.itemName);
            // 这里可以添加物品使用的具体逻辑
            
            // 使用后隐藏详情面板
            HideItemDetail();
            // 刷新背包显示
            RefreshBagDisplay();
        }
    }

    
    /// 强制刷新背包显示
    public void ForceRefreshBag()
    {
        RefreshBagDisplay();
    }

    void Start()
    {
        InitializeBagButtons();
        
        // 初始化时隐藏详情面板
        HideItemDetail();
        
        // 默认显示所有物品
        ShowAllItems();
    }
}