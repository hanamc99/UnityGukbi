using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public int id;
    public string monName;
    public int hp;

    public void InitMonsterStat(int id, string name, int hp)
    {
        this.id = id;
        this.monName = name;
        this.hp = hp;
    }

    void Start()
    {
        Debug.Log(this.id + "/" + this.monName + "/" + this.hp);
    }

    void Update()
    {
        
    }
}
