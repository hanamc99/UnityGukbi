using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DataManage dm;

    void Start()
    {
        dm = DataManage.GetInstance();
        dm.LoadMonsterData();
        GameObject go = Instantiate(dm.GetMonsterObjectByName("Slime"));
        go.transform.position = new Vector3(0f, 2f, 0f);
        GameObject go2 = Instantiate(dm.GetMonsterObjectByName("Rammus"));
        go2.transform.position = new Vector3(3f, 2f, 0f);
    }

    void Update()
    {
        
    }
}
