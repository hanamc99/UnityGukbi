using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;


public class InfoAndDataManager
{
    private static InfoAndDataManager instance;

    public List<StageInfo> stageInfos = new List<StageInfo>();

    public Dictionary<int, ShopData> dictShopData = new Dictionary<int, ShopData>();

    public void LoadShopData()
    {
        string json = Resources.Load<TextAsset>("Shop_data").text;
        dictShopData = JsonConvert.DeserializeObject<ShopData[]>(json).ToDictionary(x => x.id);
    }

    public void SaveStageInfo()
    {
        Debug.Log(Application.persistentDataPath);
        StageInfo info = new StageInfo(100, 3);
        stageInfos.Add(info);
        string json = JsonConvert.SerializeObject(stageInfos);
        File.WriteAllText(Application.persistentDataPath + "/Stage_infos.json", json);
    }

    public void LoadStageInfo()
    {
        if(File.Exists(Application.persistentDataPath + "/Stage_infos.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Stage_infos.json");
            stageInfos = JsonConvert.DeserializeObject<StageInfo[]>(json).ToList();
            Debug.Log(stageInfos[0].id);
        }
    }

    public static InfoAndDataManager GetInstance()
    {
        if(instance == null)
        {
            instance = new InfoAndDataManager();
        }
        return instance;
    }
}
