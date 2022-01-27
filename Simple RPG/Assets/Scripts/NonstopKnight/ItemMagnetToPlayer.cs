using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnetToPlayer : MonoBehaviour
{
    GameObject player;
    bool canGet = false;

    void Start()
    {
        player = FindObjectOfType<PlayerControlNK>().gameObject;
        StartCoroutine(CanGetItem());
    }

    IEnumerator CanGetItem()
    {
        yield return new WaitForSeconds(3.5f);
        canGet = true;
    }

    void Update()
    {
        if (canGet)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            transform.Translate(dir * 20f * Time.deltaTime);
        }
    }
}
