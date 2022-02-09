using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class InfoManager : MonoBehaviour
{
    [SerializeField] UIHeroInfoControl uiHeroInfo;
    [SerializeField] UIInventoryControl uiInventory;


    void Start()
    {
        DataManager.GetInstance().LoadDatas();
        DataManager.GetInstance().DiscernUserType();
        uiHeroInfo.Init(DataManager.GetInstance().GetHeroInfo());
        uiInventory.Init(DataManager.GetInstance().GetDictItemData());
    }
}
