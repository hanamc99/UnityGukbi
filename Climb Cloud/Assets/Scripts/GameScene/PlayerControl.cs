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
    private float xRange = 2.6f;
    public System.Action OnHit;
    public System.Action OnDie;
    private bool isLand = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(transform.position.y <= -8f) { OnDie(); }

        if (transform.position.x > xRange){ transform.position = new Vector2(xRange, transform.position.y); }
        else if (transform.position.x < -xRange) { transform.position = new Vector2(-xRange, transform.position.y); }

        float key = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) { key = -1f; }
        if (Input.GetKey(KeyCode.RightArrow)) { key = 1f; }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0f && isLand)
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

        anim.SetFloat("moveSpd", speedX);
        anim.SetBool("isLand", isLand);

        if(speedX > 2f)
        {
            anim.speed = speedX / 2.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            isLand = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isLand = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit();
    }
}
