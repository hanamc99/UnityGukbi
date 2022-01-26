using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    Vector3 diff;

    void Start()
    {
        diff = player.transform.position - transform.position;    
    }

    void LateUpdate()
    {
        transform.position = player.transform.position - diff;
    }
}
