using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float rotSpeed = 0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotSpeed = 25f;
        }
        this.transform.Rotate(0f, 0f, rotSpeed);
        rotSpeed *= 0.99f;
    }
}
