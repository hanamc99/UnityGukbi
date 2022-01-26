using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClasses
{
}

public class WeaponDataClass
{
    public int id;
    public string name;
    public int damage;
}

public class Weapon
{
    public int id;
    public string name;
    public int damage;

    public Weapon(int id, string name, int damage)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
    }
}
