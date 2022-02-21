using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIShopControl : MonoBehaviour
{
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] GameObject shopSlotPrefab;
    [SerializeField] GameObject grid;

    void Start()
    {
        Dictionary<int, ShopData> dict = InfoAndDataManager.GetInstance().dictShopData;
        for(int i = 0; i < dict.Count; i++)
        {
            Sprite spr = atlas.GetSprite(dict[i + 1000].spriteName);

            GameObject go = Instantiate(shopSlotPrefab);
            go.transform.SetParent(grid.transform);

            go.GetComponent<UIShopSlotControl>().InitShopSlot(i + 1000, spr);
        }
    }
}
