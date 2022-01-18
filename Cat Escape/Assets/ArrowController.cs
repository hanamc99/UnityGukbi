using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("player");
    }

    void Update()
    {
        if (!GameManager.isOver)
        {
            this.transform.Translate(Vector3.down * 10f * Time.deltaTime);

            if (this.transform.position.y <= -4.5f)
            {
                GameManager.score += 10;
                Destroy(this.gameObject);
            }

            Vector2 p1 = this.transform.position;
            Vector2 p2 = this.player.transform.position;

            float di = Vector2.Distance(p1, p2);

            if (di < 1.5f)
            {
                Destroy(this.gameObject);
                player.GetComponent<PlayerController>().DecreaseHP();
            }
        }
    }
}
