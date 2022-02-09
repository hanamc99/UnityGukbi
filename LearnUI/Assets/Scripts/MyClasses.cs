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
    public List<ItemInfo> inventory;
    public List<StageInfo> stageInfos;
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
