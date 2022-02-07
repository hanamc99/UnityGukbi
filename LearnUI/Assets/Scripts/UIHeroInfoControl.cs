using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroInfoControl : MonoBehaviour
{
    [SerializeField] Text userName;
    [SerializeField] Text level;
    [SerializeField] Text attack;
    [SerializeField] Text defence;
    [SerializeField] Text health;
    [SerializeField] Text gold;
    [SerializeField] Text gem;

    public void Init(HeroInfo info)
    {
        this.userName.text = info.userName;
        this.level.text = "Lv." + info.level;
        this.attack.text = info.attack + "";
        this.defence.text = info.defence + "";
        this.health.text = info.health + "";
        this.gold.text = info.gold + "";
        this.gem.text = info.gem + "";
    }
}
