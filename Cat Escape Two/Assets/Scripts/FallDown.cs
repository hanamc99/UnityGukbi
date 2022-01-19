using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    private GameObject player;

    private float speed;

    public void Init(float speed)
    {
        this.speed = speed;
    }

    void Start()
    {
        player = GameObject.Find("player");
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if(transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }

        float distance = (player.transform.position - transform.position).magnitude;
        if (distance <= 1.3f)
        {
            Destroy(player);
            Destroy(gameObject);
        }
    }
}
