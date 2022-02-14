using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIShopSlotControl : MonoBehaviour
{
    int id;
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] Image icon;
    [SerializeField] Text rewardText;
    [SerializeField] Text bonusText;
    [SerializeField] Image purchaseIcon;
    [SerializeField] Text purchaseText;

    void Start()
    {
        
    }

    public void InitShopSlot(int id)
    {
        this.id = id;
        LoadSlotInfo();
    }

    void LoadSlotInfo()
    {
        ShopData data = DataManager.GetInstance().GetShopData(id);
        this.icon.sprite = atlas.GetSprite(data.spriteName);
        this.icon.SetNativeSize();
        this.rewardText.text = string.Format("{0:#,###}", int.Parse(data.name));
        if (data.bonus > 0)
        {
            this.bonusText.text = "bonus +" + data.bonus + "%";
        }
        else
        {
            this.bonusText.gameObject.SetActive(false);
        }
        this.purchaseIcon.sprite = atlas.GetSprite(DataManager.GetInstance().GetBudgetData(data.budget_id).spriteName);
        this.purchaseIcon.SetNativeSize();
        this.purchaseText.text = data.price;
    }
}
