using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class StageMain : MonoBehaviour
{
    void Awake()
    {
        DataManager.GetInstance().DiscernUserType();
        DataManager.GetInstance().LoadDatas();
        //DataManager.GetInstance().ClearStage(100, 1);
        //DataManager.GetInstance().ClearStage(101, 3);
        //DataManager.GetInstance().GetItem(108);
        DataManager.GetInstance().SaveGameInfo();
    }
}
