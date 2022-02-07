using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo
{
    public string userName;
    public int level;
    public float attack;
    public float defence;
    public float health;
    public int gold;
    public int gem;

    public void Init()
    {
        this.userName = "User Name";
        this.level = 1;
        this.attack = 1;
        this.defence = 1;
        this.health = 10;
        this.gold = 0;
        this.gem = 0;
    }
}
