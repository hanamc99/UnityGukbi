using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float xRange = 8f;

    void Start()
    {
        
    }

    void Update()
    {
        CheckPos();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * 1f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * 1f);
        }
    }
    
    private void CheckPos()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xRange, xRange), transform.position.y);
    }
}
