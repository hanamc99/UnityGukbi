using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMain : MonoBehaviour
{
    void Awake()
    {
        InfoAndDataManager.GetInstance().LoadShopData();
    }
}
