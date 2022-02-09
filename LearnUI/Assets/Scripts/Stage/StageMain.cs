using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class StageMain : MonoBehaviour
{
    void Awake()
    {
        DataManager.GetInstance().LoadDatas();
        DataManager.GetInstance().DiscernUserType();
        //DataManager.GetInstance().ClearStage(101, 3);
        //DataManager.GetInstance().GetItem(108);
        DataManager.GetInstance().SaveGameInfo();
    }

    public void ShowGetItem()
    {
        DataManager.GetInstance().GetItem(100);
        DataManager.GetInstance().GetItem(101);
        DataManager.GetInstance().GetItem(102);
        DataManager.GetInstance().GetItem(103);
        DataManager.GetInstance().GetItem(104);
        DataManager.GetInstance().GetItem(105);
        DataManager.GetInstance().GetItem(106);
    }

    public void ShowClearStage()
    {
        DataManager.GetInstance().ClearStage(100, Random.Range(1,4));
    }
    public void ShowClearStage2()
    {
        DataManager.GetInstance().ClearStage(101, Random.Range(1, 4));
    }
    public void ShowClearStage3()
    {
        DataManager.GetInstance().ClearStage(102, Random.Range(1, 4));
    }
}
