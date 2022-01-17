using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public float rotSpeed;
    public Vector2 thrSpeed;
    private Vector2 startPos;
    private Vector2 endPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
            this.gameObject.transform.position = new Vector2(0f, -4f);
            rotSpeed = 0f;
            thrSpeed = Vector2.zero;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.endPos = Input.mousePosition;
            Vector2 dir = endPos - startPos;
            
            if(dir.y > 0)
            {
                thrSpeed = new Vector2(dir.x, dir.y);
                rotSpeed = dir.magnitude;
            }
        }
        this.transform.Rotate(0f, 0f, rotSpeed / 8f);
        this.transform.Translate(new Vector2(thrSpeed.x, thrSpeed.y) * 0.22f * Time.deltaTime, Space.World);
    }
}
