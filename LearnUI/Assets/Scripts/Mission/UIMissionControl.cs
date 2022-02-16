using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionControl : MonoBehaviour
{
    [SerializeField] GameObject goPopUp;
    [SerializeField] GameObject grid;
    [SerializeField] GameObject slotPrefab;

    List<int> finishAchiIndex = new List<int>();
    List<int> undergoingAchiIndex = new List<int>();
    List<int> isFullAchiIndex = new List<int>();
    List<GameObject> genedSlots = new List<GameObject>();

    [SerializeField] Text goldText;
    [SerializeField] Text heartText;
    [SerializeField] Text collectedGoldText;
    [SerializeField] Text killedMonstersText;

    void Update()
    {
        goldText.text = DataManager.GetInstance().GetGold() + "";
        heartText.text = DataManager.GetInstance().GetHeart() + "";
        collectedGoldText.text = DataManager.GetInstance().GetSliderReference(0) + "";
        killedMonstersText.text = DataManager.GetInstance().GetSliderReference(1) + "";
    }

    public void BtnTest()
    {
        DataManager.GetInstance().AddUpGold(500);
        DataManager.GetInstance().AddUpKilledMonster(1000);
        LoadAchiSlot();
    }

    public void ClosePopUp()
    {
        goPopUp.SetActive(false);
    }

    public void OpenPopUp()
    {
        LoadAchiSlot();
        goPopUp.SetActive(true);
    }

    void LoadAchiSlot()
    {
        foreach(GameObject go in genedSlots)
        {
            Destroy(go);
        }
        genedSlots.RemoveRange(0, genedSlots.Count);
        finishAchiIndex.RemoveRange(0, finishAchiIndex.Count);
        isFullAchiIndex.RemoveRange(0, isFullAchiIndex.Count);
        undergoingAchiIndex.RemoveRange(0, undergoingAchiIndex.Count);


        Dictionary<int, AchievementData> dict = DataManager.GetInstance().GetDictAchievementData();

        foreach(AchievementData data in dict.Values)
        {
            AchievementInfo info = DataManager.GetInstance().GetAchiInfo(data.id);
            AchievementProgressData data2 = DataManager.GetInstance().GetAchiProgressData(data.progressId + (int)Mathf.Clamp(info.star, 0, 4));

            if((float)DataManager.GetInstance().GetSliderReference(data.typeIndex) / (float)data2.numForPercentage >= 1f)
            {
                info.isFull = true;
            }

            if (info.star >= 5)
            {
                finishAchiIndex.Add(info.id);
            }
            else if (info.isFull)
            {
                isFullAchiIndex.Add(info.id);
            }
            else if (!info.isFull)
            {
                undergoingAchiIndex.Add(info.id);
            }
        }

        foreach(int index in isFullAchiIndex)
        {
            GameObject go = Instantiate(slotPrefab, grid.transform);
            UIAchiSlotControl slot = go.GetComponent<UIAchiSlotControl>();
            slot.InitSlot(index);
            slot.whenPressBtnClaim = LoadAchiSlot;
            genedSlots.Add(go);
        }
        foreach(int index in undergoingAchiIndex)
        {
            GameObject go = Instantiate(slotPrefab, grid.transform);
            UIAchiSlotControl slot = go.GetComponent<UIAchiSlotControl>();
            slot.InitSlot(index);
            slot.whenPressBtnClaim = LoadAchiSlot;
            genedSlots.Add(go);
        }
        foreach (int index in finishAchiIndex)
        {
            GameObject go = Instantiate(slotPrefab, grid.transform);
            UIAchiSlotControl slot = go.GetComponent<UIAchiSlotControl>();
            slot.InitSlot(index);
            slot.whenPressBtnClaim = LoadAchiSlot;
            genedSlots.Add(go);
        }
    }
}
