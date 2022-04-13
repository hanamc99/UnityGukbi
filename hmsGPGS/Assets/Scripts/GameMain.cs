using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public ZombieSpawnManager zombieSpawnMgr;

    void Start()
    {
        zombieSpawnMgr.Init();
    }
}
