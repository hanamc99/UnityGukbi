using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class DataManage
{
    static DataManage instance;
    Dictionary<int, MonsterData> dictMonsterData;
    Dictionary<int, GameObject> dictMonsterGameObjects;

    DataManage()
    {
        this.dictMonsterData = new Dictionary<int, MonsterData>();
        this.dictMonsterGameObjects = new Dictionary<int, GameObject>();
    }

    public static DataManage GetInstance()
    {
        if(DataManage.instance == null)
        {
            DataManage.instance = new DataManage();
        }
        return DataManage.instance;
    }

    public GameObject GetMonsterObjectByName(string name)
    {
        foreach(KeyValuePair<int, MonsterData> data in dictMonsterData)
        {
            if(data.Value.name == name)
            {
                return FindMonsterObjectByID(data.Value.id);
            }
        }
        return null;
    }

    GameObject FindMonsterObjectByID(int id)
    {
        foreach(KeyValuePair<int, GameObject> obj in dictMonsterGameObjects)
        {
            if(obj.Key == id)
            {
                return obj.Value;
            }
        }
        return null;
    }

    public void LoadMonsterData()
    {
        string path = "Monster_data";
        string json = Resources.Load<TextAsset>(path).text;
        this.dictMonsterData = JsonConvert.DeserializeObject<MonsterData[]>(json).ToDictionary(x => x.id);
        LoadMonsterPrefabs();
    }

    void LoadMonsterPrefabs()
    {
        foreach(KeyValuePair<int, MonsterData> data in dictMonsterData)
        {
            GameObject go = Resources.Load<GameObject>(data.Value.name);
            go.GetComponent<Monster>().InitMonsterStat(data.Value.id, data.Value.name, data.Value.hp);
            dictMonsterGameObjects.Add(data.Key, go);
        }
    }
}
