using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;
    [SerializeField] GameObject zombiePrefab;
    List<ZombieControl> zombies = new List<ZombieControl>();

    int totalZombieAmount = 5;

    public void Init()
    {
        SpawnZombie();
    }

    void SpawnZombie()
    {
        for(int i = 0; i < totalZombieAmount; i++)
        {
            GameObject zombieGo = Instantiate(zombiePrefab);
            ZombieControl zombie = zombieGo.GetComponent<ZombieControl>();
            zombies.Add(zombie);
            zombieGo.SetActive(false);
        }
        StartSpawn();
    }

    public void StartSpawn()
    {
        int j = 0;

        foreach (ZombieControl zombie in zombies)
        {
            ZombieControl zomb = zombie;
            zomb.transform.parent = null;
            zomb.gameObject.SetActive(true);
            zomb.transform.position = spawnPos[j].position;
        }
    }

}
