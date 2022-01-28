using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y <= -1f)
        {
            Destroy(gameObject);
        }
    }
}
