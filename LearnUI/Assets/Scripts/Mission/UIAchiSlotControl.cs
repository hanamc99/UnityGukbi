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
    public GameObject[] BtnClaim;
    [SerializeField] GameObject[] star;
    [SerializeField] SpriteAtlas atlas;
    public System.Action whenPressBtnClaim;

    private void Start()
    {
        BtnClaim[1].GetComponent<Button>().onClick.AddListener(InitBtnClaim);
    }

    public void InitSlot(int id)
    {
        this.id = id;
        LoadSlotInfo();
    }

    void InitBtnClaim()
    {
        AchievementData data = DataManager.GetInstance().GetAchievementData(id);
        AchievementInfo info = DataManager.GetInstance().GetAchiInfo(data.id);
        AchievementProgressData data2 = DataManager.GetInstance().GetAchiProgressData(data.progressId + (int)Mathf.Clamp(info.star, 0, 4));
        BudgetData data3 = DataManager.GetInstance().GetBudgetData(data.budgetId);

        info.isFull = false;
        info.star++;
        if (info.star >= 5)
        {
            info.star = 5;
            this.slider.value = 1f;
        }
        else
        {
            this.slider.value = 0f;
        }
        DataManager.GetInstance().PressBtnClaim(data.budgetId, data2.rewardText);
        whenPressBtnClaim();
    }

    void LoadSlotInfo()
    {
        AchievementData data = DataManager.GetInstance().GetAchievementData(id);
        AchievementInfo info = DataManager.GetInstance().GetAchiInfo(data.id);
        AchievementProgressData data2 = DataManager.GetInstance().GetAchiProgressData(data.progressId + (int)Mathf.Clamp(info.star, 0, 4));
        BudgetData data3 = DataManager.GetInstance().GetBudgetData(data.budgetId);

        this.icon.sprite = atlas.GetSprite(data.spriteName);
        this.slider.value = (float)DataManager.GetInstance().GetSliderReference(data.typeIndex) / (float)data2.numForPercentage;
        //IsSliderFull(info);
        this.nameText.text = data2.name;
        this.budgetIcon.sprite = atlas.GetSprite(data3.spriteName);
        this.rewardText.text = data3.textColor + data2.rewardText + "</color>";
        ChooseClaimBtnFrame(info, data, data2);
        FillStar(info);
    }

    void IsSliderFull(AchievementInfo info)
    {
        if(this.slider.value >= 1f)
        {
            this.slider.value = 1f;
            info.isFull = true;
        }
    }

    void ChooseClaimBtnFrame(AchievementInfo info, AchievementData data, AchievementProgressData data2)
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
