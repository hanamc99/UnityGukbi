using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class DataManage : MonoBehaviour
{
    public static DataManage instance;

    public GameInfo gi;
    private const string GAME_INFO_PATH = "Assets/Resources/GameInfo.json";

    private Dictionary<int, WeaponDataClass> dictWeaponData = new Dictionary<int, WeaponDataClass>();
    //public GameObject[] monsters = new GameObject[2];

    void Awake()
    {
        MakeInstance();
        LoadDatas();
        DiscernUserType();
    }

    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void DiscernUserType()
    {
        if (File.Exists(GAME_INFO_PATH))
        {
            Debug.Log("기존 유저입니다.");
            string getJson = File.ReadAllText(GAME_INFO_PATH);
            this.gi = JsonConvert.DeserializeObject<GameInfo>(getJson);
        }
        else
        {
            Debug.Log("신규 유저입니다.");
            this.gi = new GameInfo();
            this.gi.Init();
            SaveData();
        }
    }

    private void LoadDatas()
    {
        TextAsset data = Resources.Load<TextAsset>("Weapon_data");
        string json = data.text;
        dictWeaponData = JsonConvert.DeserializeObject<WeaponDataClass[]>(json).ToDictionary(x => x.id);
    }

    public void SaveData()
    {
        Debug.Log("데이터를 저장했습니다.");
        string saveJson = JsonConvert.SerializeObject(this.gi);
        File.WriteAllText(GAME_INFO_PATH, saveJson);
    }

    public WeaponDataClass GetWeaponData(int i)
    {
        return dictWeaponData[i];
    }

}
