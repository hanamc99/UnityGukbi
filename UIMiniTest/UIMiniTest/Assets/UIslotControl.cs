using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIslotControl : MonoBehaviour
{
    public int index;
    public System.Action isClicked;
    public GameObject goClicked;
    [SerializeField] Button btnClicked;
    [SerializeField] Image icon;
    [SerializeField] Text amountText;
    public int itemId;


    void Start()
    {
        btnClicked.onClick.AddListener(clickedAction);
    }

    public void IconInit(int id, Sprite spr, int amount)
    {
        if(id != 0)
        {
            this.itemId = id;
            this.icon.sprite = spr;
            this.icon.SetNativeSize();
            this.icon.gameObject.SetActive(true);
        }
        else
        {
            this.itemId = 0;
            this.icon.gameObject.SetActive(false);
        }
        if(amount > 1)
        {
            this.amountText.text = amount + "";
        } else
        {
            this.amountText.text = "";
        }
    }

    void clickedAction()
    {
        if (itemId != 0)
        {
            isClicked();
        }
    }
}
