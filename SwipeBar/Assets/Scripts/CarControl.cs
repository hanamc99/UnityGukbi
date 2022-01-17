using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    float speed = 0;

    void Start()
    {

    }

    private Vector3 startPos;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var endPos = Input.mousePosition;
            float swipeLength = endPos.x - this.startPos.x;
            this.speed = swipeLength * Time.deltaTime;
        }

        this.transform.Translate(this.speed, 0, 0); //x축으로 이동한다 
        this.speed *= 0.96f;    //감속한다 
    }
}
