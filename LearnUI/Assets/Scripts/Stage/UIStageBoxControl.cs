using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageBoxControl : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] List<UIStageSlotControl> slots;
    [SerializeField] GameObject slotPrefab;

    void Start()
    {
        Dictionary<int, StageData> dict = DataManager.GetInstance().GetDictStageData();

        for(int i = 0; i < dict.Count; i++)
        {
            GameObject go = Instantiate(slotPrefab);
            go.transform.SetParent(grid.transform);
            UIStageSlotControl slot = go.GetComponent<UIStageSlotControl>();
            slot.InitStageNum(i + 1);
            slots.Add(slot);
        }
    }

    void OpenOrLock()
    {
        List<StageInfo> list = DataManager.GetInstance().GetListStageInfo();
        if (list == null)
        {
            slots[0].OpenLock();
            return;
        }

        int index = list[list.Count - 1].id - 100;
        slots[index].OpenLock();
    }
}
