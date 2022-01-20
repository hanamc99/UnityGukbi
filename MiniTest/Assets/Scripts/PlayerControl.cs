using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float max_hp;
    [HideInInspector] public float hp;
    [HideInInspector] public int gold;
    [HideInInspector] public System.Action OnHit;

    void Start()
    {
        hp = max_hp;
    }



    void Update()
    {
        if (UIManager.isGo)
        {
            transform.Translate(new Vector2(Vector2.right.x * speed * Time.deltaTime, 0f));
        }
    }
}
