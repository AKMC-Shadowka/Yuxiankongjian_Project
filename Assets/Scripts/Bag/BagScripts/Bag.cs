using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bag", menuName = "Bag/New Bag")]
public class Bag : ScriptableObject
{
    public List<Item> itemList = new List<Item>();

    public bool Find(string str)
    {
        for(int i=0;i<itemList.Count;i++)
        {
            if(itemList[i].itemName==str)
            {
                return true;
            }

        }

        return false;
    }

}
