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
    Dictionary<int, ShopData> dictShopData = new Dictionary<int, ShopData>();
    Dictionary<int, BudgetData> dictBudgetData = new Dictionary<int, BudgetData>();
    Dictionary<int, AchievementData> dictAchievementData = new Dictionary<int, AchievementData>();
    Dictionary<int, AchievementProgressData> dictAchievementProgressData = new Dictionary<int, AchievementProgressData>();

    public Dictionary<int, StageData> GetDictStageData()
    {
        return dictStageData;
    }

    public Dictionary<int, AchievementData> GetDictAchievementData()
    {
        return dictAchievementData;
    }

    public AchievementData GetAchievementData(int id)
    {
        return dictAchievementData[id];
    }

    public AchievementProgressData GetAchiProgressData(int id)
    {
        return dictAchievementProgressData[id];
    }

    public Dictionary<int, ShopData> GetDictShopData()
    {
        return dictShopData;
    }

    public ShopData GetShopData(int id)
    {
        return dictShopData[id];
    }

    public BudgetData GetBudgetData(int id)
    {
        return dictBudgetData[id];
    }

    public Dictionary<int, ItemData> GetDictItemData()
    {
        return dictItemData;
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

    public List<ItemInfo> GetListItemInfo()
    {
        return gi.inventory;
    }

    public List<StageInfo> GetListStageInfo()
    {
        return gi.stageInfos;
    }

    public HeroInfo GetHeroInfo()
    {
        return gi.hero;
    }

    public int GetGold()
    {
        return gi.gold;
    }

    public int GetGem()
    {
        return gi.gem;
    }

    public int GetHeart()
    {
        return gi.heart;
    }

    public AchievementInfo GetAchiInfo(int id)
    {
        foreach(AchievementInfo info in gi.achievementBook)
        {
            if(info.id == id)
            {
                return info;
            }
        }
        return null;
    }


    public int GetSliderReference(int typeIndex)
    {
        switch (typeIndex)
        {
            case 0:
                return gi.collectedGoldSum;
            case 1:
                return gi.killedMonsterSum;
        }
        return 0;
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

        string json4 = Resources.Load<TextAsset>("Data/Shop_data").text;
        dictShopData = JsonConvert.DeserializeObject<ShopData[]>(json4).ToDictionary(x => x.id);

        string json5 = Resources.Load<TextAsset>("Data/Budget_data").text;
        dictBudgetData = JsonConvert.DeserializeObject<BudgetData[]>(json5).ToDictionary(x => x.id);

        string json6 = Resources.Load<TextAsset>("Data/Achievement_data").text;
        dictAchievementData = JsonConvert.DeserializeObject<AchievementData[]>(json6).ToDictionary(x => x.id);

        string json7 = Resources.Load<TextAsset>("Data/Achievement_progress_data").text;
        dictAchievementProgressData = JsonConvert.DeserializeObject<AchievementProgressData[]>(json7).ToDictionary(x => x.id);
    }

    public void DiscernUserType()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameInfo.json"))
        {
            gi = new GameInfo();
            gi.InitGameInfo();
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
        item.count = 0;
        gi.inventory.Add(item);
    }

    public void UseItem(int id)
    {
        foreach (ItemInfo info in gi.inventory)
        {
            if (info.id == id)
            {
                if (info.count > 0)
                {
                    info.count--;
                }
                return;
            }
        }
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

    public void AddUpGold(int amount)
    {
        gi.gold += amount;
        gi.collectedGoldSum += amount;
    }

    public void AddUpKilledMonster(int amount)
    {
        gi.killedMonsterSum += amount;
    }
    
    public void IncreaseHeart(int amount)
    {
        gi.heart += amount;
    }

    public void PressBtnClaim(int budgetId, string rewardText)
    {
        int amount = int.Parse(rewardText);
        switch (budgetId)
        {
            case 1000:
                IncreaseHeart(amount);
                break;
            case 1001:
                AddUpGold(amount);
                break;
        }
    }
}
