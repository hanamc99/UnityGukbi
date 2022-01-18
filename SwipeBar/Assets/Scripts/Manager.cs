using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject car;
    public GameObject flag;
    public Text text;
    private float distance;
    private bool isOver = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = car.transform.position;
    }

    void Update()
    {
        distance = car.transform.position.x - flag.transform.position.x;
        int length = (int)Mathf.Abs(distance);

        if (distance >= 1f)
        {
            this.text.text = "Game Over";
            isOver = true;
        }
        else if (length == 0)
        {
            this.text.text = "Clear!";
            isOver = true;
        }
        if (!isOver)
        {
            this.text.text = length + "m";
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isOver = false;
                this.car.transform.position = startPos;
            }
        }


    }
}
