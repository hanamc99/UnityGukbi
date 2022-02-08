using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIinventoryControl : MonoBehaviour
{
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] UIslotControl[] slots = new UIslotControl[6];
    int clickedIndex = 0;

    void Start()
    {
        DataManager.MakeInstance();
        DataManager.instance.LoadDictData();
        DataManager.instance.LoadGameInfo();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            ClickedInit(i);
        }
    }

    public void InitSlots()
    {
        List<ItemInfo> inven = DataManager.instance.GetInventory();

        for (int i = 0; i < slots.Length; i++)
        {
            if(inven.Count - 1 < i)
            {
                slots[i].IconInit(0, null, 0);
            }
            else
            {
                int amount = DataManager.instance.LetSearchItem(inven[i].id).amount;
                if(amount > 0)
                {
                    Sprite spr = atlas.GetSprite(DataManager.instance.GetItemData(inven[i].id).spriteName);
                    slots[i].IconInit(inven[i].id, spr, amount);
                }
                else
                {
                    slots[i].IconInit(0, null, 0);
                }
            }
        }
    }

    public void TestGetItem()
    {
        DataManager.instance.GetItem(101);
        DataManager.instance.GetItem(102);
        DataManager.instance.GetItem(106);
    }

    void UseItem(int id)
    {
        ItemInfo item = DataManager.instance.LetSearchItem(id);
        item.amount--;
        if(item.amount <= 0)
        {
            DataManager.instance.GetInventory().Remove(item);
        }
        InitSlots();
    }

    public void DisplayInventory()
    {
        DataManager.instance.ShowInventory();
    }


    void ClickedInit(int i)
    {
        slots[i].isClicked += () =>
        {
            if (slots[this.clickedIndex].goClicked.activeSelf)
            {
                slots[this.clickedIndex].goClicked.SetActive(false);
            }
            else if(this.clickedIndex == slots[i].index)
            {
                slots[i].goClicked.SetActive(true);
                UseItem(slots[i].itemId);
            }

            if (this.clickedIndex != slots[i].index)
            {
                this.clickedIndex = slots[i].index;
                slots[i].goClicked.SetActive(true);
                UseItem(slots[i].itemId);
            }
        };
    }
}
