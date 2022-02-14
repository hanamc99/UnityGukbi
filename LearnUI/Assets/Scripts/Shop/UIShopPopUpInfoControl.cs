using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopPopUpInfoControl : MonoBehaviour
{
    int type;
    [SerializeField] GameObject goShopSlot;
    [SerializeField] GameObject grid;

    void Start()
    {
        
    }

    public void InitPopUp(int type)
    {
        this.type = type;
        LoadShopSlot();
    }

    void LoadShopSlot()
    {
        Dictionary<int, ShopData> dict = DataManager.GetInstance().GetDictShopData();
        List<ShopData> datas = new List<ShopData>();

        foreach(ShopData data in dict.Values)
        {
            if(data.type == type)
            {
                datas.Add(data);
            }
        }

        for(int i = 0; i < datas.Count; i++)
        {
            GameObject go = Instantiate(goShopSlot);
            go.transform.SetParent(grid.transform);
            go.GetComponent<UIShopSlotControl>().InitShopSlot(datas[i].id);
        }
    }
}
