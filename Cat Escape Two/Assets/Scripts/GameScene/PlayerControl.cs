using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float max_hp;
    private float hp;
    public System.Action<float> OnHit;
    public System.Action OnDie;
    private float xRange = 8f;
    [SerializeField] private Sprite[] s = new Sprite[3];

    void Start()
    {
        hp = 1;
        GetComponent<SpriteRenderer>().sprite = s[ToGameScene.instance.GetIndex()];
    }

    public void Hit(int d)
    {
        this.hp -= d;
        this.hp = Mathf.Clamp(hp, 0f, max_hp);
        this.OnHit(GetPercentageOfHp());
        if (hp <= 0f)
        {
            this.OnDie();
        }
    }

    private float GetPercentageOfHp()
    {
        return hp / max_hp;
    }

    void Update()
    {
        if (!GameManager.isOver)
        {
            CheckPos();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.Translate(Vector2.left * 1f);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.Translate(Vector2.right * 1f);
            }
        }
    }
    
    private void CheckPos()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xRange, xRange), transform.position.y);
    }
}
