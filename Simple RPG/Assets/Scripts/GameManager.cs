using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private KnightControl knight;


    void Start()
    {
        knight = FindObjectOfType<KnightControl>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1.0f);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000f);
            Vector3 pos = new Vector3(hit.point.x, 0f, hit.point.z);
            knight.MoveKnight(pos);
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyControl>().DisplayStat();
            }
        }
    }
}
