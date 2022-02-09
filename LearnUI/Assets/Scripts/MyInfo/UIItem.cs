using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Text countText;

    public void Init(Sprite spr)
    {
        if(spr != null)
        {
            this.img.sprite = spr;
            this.img.SetNativeSize();
            this.img.gameObject.SetActive(true);
        } else
        {
            this.img.gameObject.SetActive(false);
        }
    }

    public void UpdateCount(int count)
    {
        this.countText.text = count + "";
    }
}
