using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam03_Classes
{
}

public class StageInfo
{
    public int id;
    public int stars;

    public StageInfo(int id, int star)
    {
        this.id = id;
        this.stars = star;
    }
}

public class ShopData
{
    public int id;
    public string name;
    public string spriteName;
    public string reward;
    public string price;
}
