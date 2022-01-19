using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    private float timer;
    [SerializeField] GameObject arrow;


    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.4f)
        {
            timer = 0f;
            FallDown fd = Instantiate(arrow, new Vector2(Random.Range(-8f, 8f), 9f), arrow.transform.rotation).GetComponent<FallDown>();
            fd.Init(Random.Range(2f, 8f));
        }
    }
}
