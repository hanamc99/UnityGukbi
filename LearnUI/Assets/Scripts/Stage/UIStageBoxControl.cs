using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageBoxControl : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] List<UIStageSlotControl> slots;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] UIPageControl[] pages;
    [SerializeField] Button btnLeftPage;
    [SerializeField] Button btnRightPage;
    int pg = 0;

    void Start()
    {
        LoadItems();
        ChangeYellowPos();
        btnLeftPage.onClick.AddListener(() => {
            for (int h = 0; h < slots.Count; h++)
            {
                Destroy(slots[h].gameObject);
                slots.Remove(slots[h]);
                h--;
            }
            pg--;
            if (pg < 0)
            {
                pg = 1;
            }
            LoadItems();
            ChangeYellowPos();
        });
        btnRightPage.onClick.AddListener(() =>
        {
            for(int h = 0; h < slots.Count; h++)
            {
                Destroy(slots[h].gameObject);
                slots.Remove(slots[h]);
                h--;
            }
            pg++;
            if(pg > 1)
            {
                pg = 0;
            }
            LoadItems();
            ChangeYellowPos();
        });
    }

    void ChangeYellowPos()
    {
        foreach (UIPageControl page in pages)
        {
            page.goYellow.SetActive(false);
        }
        pages[pg].goYellow.SetActive(true);
    }

    void LoadItems()
    {
        Dictionary<int, StageData> dict = DataManager.GetInstance().GetDictStageData();

        int ed = 18 + (pg * 18);
        if (ed >= dict.Count)
        {
            ed = dict.Count;
        }

        for (int i = 0 + (pg * 18); i < ed; i++)
        {
            GameObject go = Instantiate(slotPrefab);
            go.transform.SetParent(grid.transform);
            UIStageSlotControl slot = go.GetComponent<UIStageSlotControl>();
            slot.InitStageNum(i + 1);
            slots.Add(slot);
        }
        foreach (UIStageSlotControl slot2 in slots)
        {
            Debug.Log(slot2.id);
        }
    }

    public void ShowSlots()
    {
        foreach(UIStageSlotControl slot in slots)
        {
            slot.OpenLock();
            slot.MarkStar();
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
