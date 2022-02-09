using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class DataManager
{
    private static DataManager instance;

    GameInfo gi;

    Dictionary<int, ItemData> dictItemData = new Dictionary<int, ItemData>();
    Dictionary<int, StageData> dictStageData = new Dictionary<int, StageData>();
    Dictionary<int, MissionData> dictMissionData = new Dictionary<int, MissionData>();

    public Dictionary<int, StageData> GetDictStageData()
    {
        return dictStageData;
    }

    public MissionData GetMissionData(int id)
    {
        return dictMissionData[id];
    }

    public ItemData GetItemData(int id)
    {
        if (id == 0)
        {
            return null;
        }
        return dictItemData[id];
    }

    public int GetItemInfoAmount(int id)
    {
        foreach(ItemInfo info in gi.inventory)
        {
            if(info.id == id)
            {
                return info.count;
            }
        }
        return 0;
    }

    public List<StageInfo> GetListStageInfo()
    {
        return gi.stageInfos;
    }

    public static DataManager GetInstance()
    {
        if(DataManager.instance == null)
        {
            DataManager.instance = new DataManager();
        }
        return DataManager.instance;
    }

    public void LoadDatas()
    {
        string json = Resources.Load<TextAsset>("Data/Item_data").text;
        dictItemData = JsonConvert.DeserializeObject<ItemData[]>(json).ToDictionary(x => x.id);

        string json2 = Resources.Load<TextAsset>("Data/Stage_data").text;
        dictStageData = JsonConvert.DeserializeObject<StageData[]>(json2).ToDictionary(x => x.id);

        string json3 = Resources.Load<TextAsset>("Data/Mission_data").text;
        dictMissionData = JsonConvert.DeserializeObject<MissionData[]>(json3).ToDictionary(x => x.id);
    }

    public void DiscernUserType()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameInfo.json"))
        {
            gi = new GameInfo();
            gi.inventory = new List<ItemInfo>();
            gi.stageInfos = new List<StageInfo>();
            SaveGameInfo();
        }
        else
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/GameInfo.json");
            gi = JsonConvert.DeserializeObject<GameInfo>(json);
        }
    }

    public void SaveGameInfo()
    {
        string json = JsonConvert.SerializeObject(gi);
        File.WriteAllText(Application.persistentDataPath + "/GameInfo.json", json);
    }

    public void GetItem(int id)
    {
        foreach (ItemInfo info in gi.inventory)
        {
            if (info.id == id)
            {
                info.count++;
                return;
            }
        }
        ItemInfo item = new ItemInfo(id);
        item.count = 1;
        gi.inventory.Add(item);
    }

    public void ClearStage(int id, int star)
    {
        if(star < 1)
        {
            return;
        }
        foreach(StageInfo info in gi.stageInfos)
        {
            if(info.id == id)
            {
                if(info.star < star)
                {
                    info.star = star;
                }
                return;
            }
        }
        gi.stageInfos.Add(new StageInfo(id, star));
    }
}
