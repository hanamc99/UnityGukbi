using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageControl : MonoBehaviour
{
    [SerializeField] GameObject[] slots;
    [SerializeField] GameObject grid;

    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            if (i < InfoAndDataManager.GetInstance().stageInfos.Count)
            {
                GameObject go = Instantiate(slots[2]);
                go.transform.SetParent(grid.transform);
                go.GetComponent<ClearSlotControl>().InitSlot(i + 1, InfoAndDataManager.GetInstance().stageInfos[i].stars);
            }
            else if (i == InfoAndDataManager.GetInstance().stageInfos.Count)
            {
                GameObject go = Instantiate(slots[1]);
                go.transform.SetParent(grid.transform);
                go.GetComponent<OpenSlotControl>().InitSlot(i + 1);
            }
            else
            {
                GameObject go = Instantiate(slots[0]);
                go.transform.SetParent(grid.transform);
            }
        }
    }
}
