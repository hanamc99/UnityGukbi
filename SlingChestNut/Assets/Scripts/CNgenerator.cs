using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNgenerator : MonoBehaviour
{
    [SerializeField] private GameObject chestNut;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChestNutControl cn = Instantiate(chestNut, chestNut.transform.position, chestNut.transform.rotation).GetComponent<ChestNutControl>();
            //cn.Shoot(new Vector3(0f, 200f, 2000f));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1.0f);
            Vector3 worldDir = ray.direction;
            cn.Shoot(worldDir.normalized * 2000f);
        }
    }
}
