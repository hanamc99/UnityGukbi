using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float key = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpSpeed));
        }

        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1f, 1f);
        }

        float speedX = Mathf.Abs(rb.velocity.x);

        if (speedX < maxSpeed)
        {
            rb.AddForce(Vector2.right * key * speed);
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.75f, rb.velocity.y);
        }



        this.anim.speed = Mathf.Abs(speedX * key) / 2f;
    }
}
