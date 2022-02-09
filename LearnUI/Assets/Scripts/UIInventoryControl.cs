using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIInventoryControl : MonoBehaviour
{
    [SerializeField] UIItem[] uiItems;
    [SerializeField] SpriteAtlas atlas;

    public void Init(Dictionary<int, ItemData> dic)
    {
        for (int i = 0; i < uiItems.Length; i++)
        {
            if (i > dic.Count - 1)
            {
                uiItems[i].Init(null);
            }
            else
            {
                Sprite spr = atlas.GetSprite(dic[i + 101].spriteName);
                uiItems[i].Init(spr);
            }
        }
    }

    public void UpdataCount(List<ItemInfo> items)
    {
        for(int i = 0; i < uiItems.Length; i++)
        {
            if(i < items.Count)
            {
                uiItems[i].UpdateCount(items[i].count);
            }
        }
    }
}
