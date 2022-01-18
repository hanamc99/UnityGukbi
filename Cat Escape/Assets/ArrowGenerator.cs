using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    private float timer;
    private float gnr;
    public GameObject arrow;
    private float xRange;
    private float yPos;

    void Start()
    {
        yPos = 6f;
        xRange = 8.5f;
        timer = 0f;
        gnr = 0.1f;
    }

    void Update()
    {
        if (!GameManager.isOver)
        {
            timer += Time.deltaTime;

            if (timer > gnr)
            {
                timer = 0f;
                Vector2 pos = new Vector2(Random.Range(-xRange, xRange), yPos);
                Instantiate<GameObject>(arrow, pos, arrow.transform.rotation);
            }
        }
    }
}
