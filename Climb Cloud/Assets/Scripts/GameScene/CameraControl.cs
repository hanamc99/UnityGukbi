using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 diff;

    void Start()
    {
        diff = player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float yDiff = player.transform.position.y - diff.y;
        this.transform.position = new Vector3(transform.position.x, yDiff, transform.position.z);
    }
}
