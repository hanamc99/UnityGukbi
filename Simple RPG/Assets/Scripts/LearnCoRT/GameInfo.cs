using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    public int id;
    public int hp;

    public Monster(int id, int hp)
    {
        this.id = id;
        this.hp = hp;
    }
}

public class GameInfo
{
    public Monster monsterInfo;

    public void InitMonsterInfo()
    {
        int id = useJson.instance.GetMonsterData(0).id;
        int hp = useJson.instance.GetMonsterData(0).hp;
        monsterInfo = new Monster(id, hp);
    }
}
