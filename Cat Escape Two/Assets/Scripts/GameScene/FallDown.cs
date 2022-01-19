using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    private GameObject player;

    private float speed;
    private int damage;

    public void Init(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        player = GameObject.Find("player");
    }

    void Update()
    {
        if (!GameManager.isOver)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y <= -4f)
            {
                Destroy(gameObject);
            }

            float distance = (player.transform.position - transform.position).magnitude;
            if (distance <= 1.3f)
            {
                Destroy(gameObject);
                player.GetComponent<PlayerControl>().Hit(this.damage);
            }
        }
    }
}
