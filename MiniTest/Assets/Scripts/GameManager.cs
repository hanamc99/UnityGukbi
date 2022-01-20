using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerControl player;
    [SerializeField] private List<GameObject> objs = new List<GameObject>(4);
    [SerializeField] private Text text;

    void Start()
    {
        
    }

    void Update()
    {
        if(UIManager.isGo)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                float length = Mathf.Abs(player.transform.position.x - objs[i].transform.position.x);
                if (length <= 0.5f)
                {
                    WhatObject(objs[i].tag);
                    if(objs[i].tag == "gold" || objs[i].tag == "trap")
                    {
                        Destroy(objs[i]);
                        objs.RemoveAt(i);
                    }
                }
            }
        }

        float flagLength = Mathf.Abs(player.transform.position.x - objs[0].transform.position.x);
        text.text = (int)flagLength + "m\n" + player.hp + "hp\n" + player.gold + "gold";
    }

    private void WhatObject(string tagName)
    {
        switch (tagName)
        {
            case "flag":
                player.OnHit = PlayerAndFlag;
                break;
            case "trap":
                player.OnHit = PlayerAndTrap;
                break;
            case "gold":
                player.OnHit = PlayerAndGold;
                break;
        }
        player.OnHit();
    }

    private void PlayerAndFlag()
    {
        UIManager.isGo = false;
    }

    private void PlayerAndTrap()
    {
        player.hp -= 5f;
    }

    private void PlayerAndGold()
    {
        player.gold += 100;
    }
}
