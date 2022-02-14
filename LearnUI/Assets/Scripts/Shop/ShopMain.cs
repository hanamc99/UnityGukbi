using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMain : MonoBehaviour
{
    void Start()
    {
        DataManager.GetInstance().LoadDatas();
    }
}
