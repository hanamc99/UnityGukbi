using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class MonsterData
{
    public int id;
    public int hp;
}

public class useJson : MonoBehaviour
{
    public static useJson instance;

    public GameInfo gi;

    private Dictionary<int, MonsterData> dictMonsterData = new Dictionary<int, MonsterData>();

    void Awake()
    {
        instance = this;
        //TextAsset data = Resources.Load<TextAsset>("monster_data");
        //string json = data.text;
        //Debug.Log(json);
        //dictMonsterData = JsonConvert.DeserializeObject<MonsterData[]>(json).ToDictionary(x => x.id);
        //Debug.Log(dictMonsterData[0].hp);
        TextAsset data2 = Resources.Load<TextAsset>("monster_data3");
        string json2 = data2.text;
        gi = new GameInfo();
        gi = JsonConvert.DeserializeObject<GameInfo>(json2);
        //gi.InitMonsterInfo();
    }

    public void SaveData()
    {
        Debug.Log("데이터를 저장했습니다.");
        string saveJson = JsonConvert.SerializeObject(this.gi);
        File.WriteAllText("Assets/Resources/monster_data3.json", saveJson);
    }

    public MonsterData GetMonsterData(int i)
    {
        return dictMonsterData[i];
    }

}
