using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class InfoManager : MonoBehaviour
{
    [SerializeField] UIHeroInfoControl uiHeroInfo;
    [SerializeField] UIInventoryControl uiInventory;

    HeroInfo hero;
    List<Item> itemlist;

    Dictionary<int, ItemData> dictItemData = new Dictionary<int, ItemData>();

    void Start()
    {
        LoadData();
        
        ReadHeroInfoJson();
        ReadItemInfoJson();

        GetItem(110);
        GetItem(108);
        GetItem(108);
        GetItem(110);
    }

    void LoadData()
    {
        string json = Resources.Load<TextAsset>("Item_data").text;
        ItemData[] datas = JsonConvert.DeserializeObject<ItemData[]>(json);
        foreach (ItemData data in datas)
        {
            dictItemData.Add(data.id, data);
        }
    }

    void ReadHeroInfoJson()
    {
        if(File.Exists(Application.persistentDataPath + "/Hero_info.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Hero_info.json");
            this.hero = JsonConvert.DeserializeObject<HeroInfo>(json);
        } else
        {
            this.hero = new HeroInfo();
            this.hero.Init();
            WriteHeroInfoJson();
        }
        this.uiHeroInfo.Init(this.hero);
    }

    void WriteHeroInfoJson()
    {
        string json = JsonConvert.SerializeObject(hero);
        File.WriteAllText(Application.persistentDataPath + "/Hero_info.json", json);
    }

    void ReadItemInfoJson()
    {
        if (File.Exists(Application.persistentDataPath + "/Item_info.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Item_info.json");
            Item[] items = JsonConvert.DeserializeObject<Item[]>(json);
            this.itemlist = items.ToList();
        } else
        {
            this.itemlist = new List<Item>();
            for(int i = 0; i < dictItemData.Count; i++)
            {
                GetItem(dictItemData[i + 101].id);
            }
            WriteItemInfoJson();
        }

        this.uiInventory.Init(dictItemData);
    }

    public void UpdateCount()
    {
        this.uiInventory.UpdataCount(itemlist);
    }

    void WriteItemInfoJson()
    {
        string saveItemsJson = JsonConvert.SerializeObject(itemlist);

        File.WriteAllText(Application.persistentDataPath + "/Item_info.json", saveItemsJson);
    }

    void GetItem(int id)
    {
        foreach(Item item in itemlist)
        {
            if (item.id == id)
            {
                item.count++;
                Debug.Log("아이템이 추가되었다." + item.count);
                return;
            } 
        }
        Item item1 = new Item(id);
        item1.count = 0;
        itemlist.Add(item1);
    }
}
