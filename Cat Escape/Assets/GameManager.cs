using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private GameObject text;
    public static bool isOver = false;
    [SerializeField] private float timer;
    public static int score;
    [SerializeField] private Image hp_gauge;

    void Start()
    {
        score = 0;
        player = FindObjectOfType<PlayerController>();
        text = GameObject.Find("Text");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        hp_gauge.fillAmount = player.hp;

        if(timer <= 0f || player.hp <= 0f)
        {
            isOver = true;
        }
        if (isOver)
        {
            text.GetComponent<Text>().text = "���� ����" + "\n���� : " + score;
            player.gameObject.SetActive(false);
        } else
        {
            text.GetComponent<Text>().text = "���� �ð� : " + (int)timer + "\n���� : " + score;
        }
    }
}
