using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerControl player;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        player.OnHit = PlayerAndFlag;
        player.OnDie = ReLoadGameScene;
    }

    void ReLoadGameScene()
    {
        SceneManager.LoadScene(0);
    }

    void PlayerAndFlag()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        
    }
}
