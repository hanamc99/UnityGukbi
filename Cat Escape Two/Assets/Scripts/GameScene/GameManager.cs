using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uim;
    [SerializeField] private PlayerControl player;
    [HideInInspector] public static bool isOver;

    void Start()
    {
        isOver = false;
        player.OnHit = (float f) => uim.ChangeHpGauge(f);
        player.OnDie = () => { uim.ShowGameOverPanel(); isOver = true; };
    }

    void Update()
    {
        if (isOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("LobbyScene");
            }
        }
    }
}
