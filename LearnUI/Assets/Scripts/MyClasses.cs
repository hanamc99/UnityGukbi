using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClasses
{
}

public class GameInfo
{
    public int score;
    public int heart;
    public int gold;
    public int gem;
    public HeroInfo hero;
    public List<ItemInfo> inventory;
    public List<StageInfo> stageInfos;

    public void InitGameInfo()
    {
        hero = new HeroInfo();
        hero.Init();
        inventory = new List<ItemInfo>();
        InitInventory();
        stageInfos = new List<StageInfo>();
    }

    void InitInventory()
    {
        Dictionary<int, ItemData> dict = DataManager.GetInstance().GetDictItemData();
        for (int i = 0; i < dict.Count; i++)
        {
            DataManager.GetInstance().GetItem(dict[100 + i].id);
        }
    }
}


public class StageInfo
{
    public int id;
    public int star;

    public StageInfo(int id, int star)
    {
        this.id = id;
        this.star = star;
    }
}

public class ItemInfo
{
    public int id;
    public int count;

    public ItemInfo(int id)
    {
        this.id = id;
    }
}

public class HeroInfo
{
    public string userName;
    public int level;
    public float exp;
    public float health;
    public float attack;
    public float defence;

    public void Init()
    {
        this.userName = "User Name";
        this.level = 1;
        this.exp = 0.3f;
        this.health = 1000;
        this.attack = 1;
        this.defence = 1;
    }
}

public class StageData
{
    public int id;
    public string name;
    public int requireLevel;
    public int stage_mission_id;
    public int availableItem_id_0;
    public int availableItem_id_1;
    public int availableItem_id_2;
}

public class ItemData
{
    public int id;
    public string name;
    public string spriteName;
}

public class MissionData
{
    public int id;
    public string name;
}

public class ShopData
{
    public int id;
    public int type;
    public string name;
    public int bonus;
    public string spriteName;
    public int budget_id;
    public string price;
}

public class BudgetData
{
    public int id;
    public string name;
    public string spriteName;
}
