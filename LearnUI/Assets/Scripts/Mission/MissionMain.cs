using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMain : MonoBehaviour
{

    void Start()
    {
        Debug.Log("����");
        DataManager.GetInstance().LoadDatas();
        DataManager.GetInstance().DiscernUserType();
        DataManager.GetInstance().SaveGameInfo();
    }
}
