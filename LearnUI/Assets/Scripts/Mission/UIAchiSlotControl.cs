using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIAchiSlotControl : MonoBehaviour
{
    int id;
    [SerializeField] Image icon;
    [SerializeField] Slider slider;
    [SerializeField] Text nameText;
    [SerializeField] Image budgetIcon;
    [SerializeField] Text rewardText;
    [SerializeField] GameObject[] BtnClaim;
    [SerializeField] GameObject[] star;
    [SerializeField] SpriteAtlas atlas;

    public void InitSlot(int id)
    {
        this.id = id;
        LoadSlotInfo();
    }

    void LoadSlotInfo()
    {
        AchievementData data = DataManager.GetInstance().GetAchievementData(id);
        AchievementInfo info = DataManager.GetInstance().GetAchiInfo(data.id);
        AchievementProgressData data2 = DataManager.GetInstance().GetAchiProgressData(data.progressId + info.star);
        BudgetData data3 = DataManager.GetInstance().GetBudgetData(data.budgetId);

        this.icon.sprite = atlas.GetSprite(data.spriteName);
        this.slider.value = DataManager.GetInstance().GetSliderReference(data.typeIndex) / data2.numForPercentage;
        this.nameText.text = data2.name;
        this.budgetIcon.sprite = atlas.GetSprite(data3.spriteName);
        this.rewardText.text = data3.textColor + data2.rewardText + "</color>";
        ChooseClaimBtnFrame(info);
        FillStar(info);
    }

    void ChooseClaimBtnFrame(AchievementInfo info)
    {
        foreach (GameObject go in BtnClaim)
        {
            go.SetActive(false);
        }
        if (info.star >= 5)
        {
            BtnClaim[2].SetActive(true);
        }
        else if(info.isFull)
        {
            BtnClaim[1].SetActive(true);
        } else if (!info.isFull)
        {
            BtnClaim[0].SetActive(true);
        }
    }

    void FillStar(AchievementInfo info)
    {
        for(int s = 0; s < info.star; s++)
        {
            star[s].SetActive(true);
        }
    }
}
