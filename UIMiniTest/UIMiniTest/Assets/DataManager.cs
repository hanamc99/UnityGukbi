using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

public class DataManager
{
    public static DataManager instance;

    GameInfo gi;

    Dictionary<int, ItemData> dictItemData = new Dictionary<int, ItemData>();

    public static void MakeInstance()
    {
        DataManager.instance = new DataManager();
    }

    public void LoadDictData()
    {
        string json = Resources.Load<TextAsset>("Item_data").text;
        dictItemData = JsonConvert.DeserializeObject<ItemData[]>(json).ToDictionary(x => x.id);
        Debug.Log("딕트데이타 완");
    }

    public void LoadGameInfo()
    {
        if(File.Exists(Application.persistentDataPath + "GameInfo.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "GameInfo.json");
            gi = JsonConvert.DeserializeObject<GameInfo>(json);
        } else
        {
            gi = new GameInfo();
            gi.InventoryInit();
        }
        Debug.Log("게임인포 완");
    }

    public void SaveGameInfo()
    {
        string json = JsonConvert.SerializeObject(gi);
        File.WriteAllText(Application.persistentDataPath + "GameInfo.json", json);
    }

    public void GetItem(int id)
    {
        gi.PutItemToInven(id);
    }

    public ItemData GetItemData(int id)
    {
        return dictItemData[id];
    }

    public void ShowInventory()
    {
        gi.DisplayAllItems();
    }

    public List<ItemInfo> GetInventory()
    {
        return gi.inventory;
    } 

    public ItemInfo LetSearchItem(int id)
    {
        return gi.SearchItem(id);
    }
}
