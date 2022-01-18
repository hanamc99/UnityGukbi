using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xRange;
    private float yPos;
    public float hp;
    private bool moveLeft = false;
    private bool moveRight = false;

    void Start()
    {
        yPos = -3.5f;
        xRange = 8.5f;
        hp = 1f;
    }

    public void DecreaseHP()
    {
        hp -= 0.1f;
    }

    void FixedUpdate()
    {
        if(this.gameObject.transform.position.x > xRange)
        {
            this.gameObject.transform.position = new Vector2(xRange, yPos);
        }else if(this.gameObject.transform.position.x < -xRange)
        {
            this.gameObject.transform.position = new Vector2(-xRange, yPos);
        }

        if (moveLeft)
        {
            MoveLeft();
        }
        if (moveRight)
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.Translate(new Vector3(-0.2f, 0f, 0f));
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.Translate(new Vector3(0.2f, 0f, 0f));
        }
    }

    public void MoveLeft()
    {
        this.gameObject.transform.Translate(new Vector3(-10f * Time.fixedDeltaTime, 0f, 0f));
    }

    public void LeftPointerDown()
    {
        moveLeft = true;
    }

    public void LeftPointerUp()
    {
        moveLeft = false;
    }

    public void RightPointerDown()
    {
        moveRight = true;
    }

    public void RightPointerUp()
    {
        moveRight = false;
    }

    public void MoveRight()
    {
        this.gameObject.transform.Translate(new Vector3(10f * Time.fixedDeltaTime, 0f, 0f));
    }
}
