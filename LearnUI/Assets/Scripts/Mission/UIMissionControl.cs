using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMissionControl : MonoBehaviour
{
    [SerializeField] GameObject goPopUp;
    [SerializeField] GameObject grid;
    [SerializeField] GameObject slotPrefab;
    bool wasOpened = false;

    void Start()
    {
        
    }

    public void ClosePopUp()
    {
        goPopUp.SetActive(false);
    }

    public void OpenPopUp()
    {
        if (!wasOpened)
        {
            LoadAchiSlot();
            wasOpened = true;
        }
        goPopUp.SetActive(true);
    }

    void LoadAchiSlot()
    {
        Dictionary<int, AchievementData> dict = DataManager.GetInstance().GetDictAchievementData();

        for(int i = 0; i < dict.Count; i++)
        {
            GameObject go = Instantiate(slotPrefab, grid.transform);
            go.GetComponent<UIAchiSlotControl>().InitSlot(10000 + i);
            Debug.Log(10000 + i);
        }
    }
}
