using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public int floor;
    public Weapon weapon;

    public void Init()
    {
        floor = 1;
        WeaponDataClass data = DataManage.instance.GetWeaponData(0);
        this.weapon = new Weapon(data.id, data.name, data.damage);
    }
}
