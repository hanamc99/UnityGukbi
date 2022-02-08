using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData 
{
    public int id;
    public string name;
    public string spriteName;
}


public class ItemInfo
{
    public int id;
    public int amount;

    public ItemInfo(int id)
    {
        this.id = id;
    }
}



public class GameInfo
{
    public List<ItemInfo> inventory;

    public void InventoryInit()
    {
        this.inventory = new List<ItemInfo>();
    }

    public ItemInfo SearchItem(int id)
    {
        foreach(ItemInfo item in inventory)
        {
            if(item.id == id)
            {
                return item;
            }
        }
        return null;
    }

    public void DisplayAllItems()
    {
        foreach(ItemInfo item in inventory)
        {
            Debug.Log(DataManager.instance.GetItemData(item.id).name);
            Debug.Log(item.amount);
        }
    }

    public void PutItemToInven(int id)
    {
        foreach(ItemInfo item in inventory)
        {
            if(item.id == id)
            {
                item.amount++;
                return;
            }
        }
        ItemInfo item2 = new ItemInfo(id);
        item2.amount = 1;
        inventory.Add(item2);
    }
}
