using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDragonDropWeapon : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    EnemyControlNK monster;
    bool dropOnce = false;

    void Start()
    {
        monster = GetComponent<EnemyControlNK>();
        monster.OnDie += DropWeapon;
    }

    void DropWeapon()
    {
        if (!dropOnce)
        {
            dropOnce = true;
            Instantiate(weapon, this.transform.position, this.transform.rotation);
        }
    }
}
