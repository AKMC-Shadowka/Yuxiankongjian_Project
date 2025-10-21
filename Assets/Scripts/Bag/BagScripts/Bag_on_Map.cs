using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag_on_Map : MonoBehaviour
{
    public Item thisItem;
    public Bag playerBag;

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         AddNewItem();
    //     }

    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"物品 {thisItem.itemName} 与 {other.gameObject.name} 发生碰撞，标签：{other.gameObject.tag}");
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("触发玩家拾取逻辑");
            AddNewItem();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log($"碰撞对象不是玩家，标签：{other.gameObject.tag}");
        }
    }


    //添加物品
    public void AddNewItem()
    {
        if (!playerBag.itemList.Contains(thisItem))
        {  //如果背包中没有该物品
            playerBag.itemList.Add(thisItem);  //添加物品
            thisItem.itemQuantity++;  //物品数量加1
            Destroy(this.gameObject);  //销毁物品
        }

        else
        {
            thisItem.itemQuantity++;  //物品数量加1
            Destroy(this.gameObject);  //销毁物品
        }
    }


    // 尝试将物品送给 NPC
    public void TryGiveItemToNPC(GameObject npc)
    {
        // 这里假设 NPC 身上有一个脚本，定义了它需要的物品
        NPC_NeedItem npcNeed = npc.GetComponent<NPC_NeedItem>();
        if (npcNeed != null && npcNeed.requiredItem == thisItem)
        {
            // 匹配成功，从背包中移除物品
            if (playerBag.itemList.Contains(thisItem))
            {
                if (thisItem.itemQuantity > 1)
                {
                    thisItem.itemQuantity--;
                }
                else
                {
                    playerBag.itemList.Remove(thisItem);
                }
                Debug.Log("物品已送给 NPC：" + thisItem.itemName);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Debug.Log("NPC 不需要这个物品");
            // 不匹配，物品不消失
        }
    }
    
}
