using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSlotControl : MonoBehaviour
{
    [SerializeField] Button btnPay;
    [SerializeField] Text priceText;
    [SerializeField] Text nameText;
    [SerializeField] Text rewardText;
    [SerializeField] Image icon;
    int id;

    private void Start()
    {
        btnPay.onClick.AddListener(() => Debug.Log("이 상품의 id는 " + this.id + "이고, 가격은 " + this.priceText.text + "입니다."));
    }

    public void InitShopSlot(int id, Sprite spr)
    {
        this.id = id;
        this.icon.sprite = spr;

        ShopData data = InfoAndDataManager.GetInstance().dictShopData[id];

        priceText.text = data.price;
        nameText.text = data.name;
        rewardText.text = data.reward;
    }
}
