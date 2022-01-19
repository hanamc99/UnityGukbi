using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSelectScene : MonoBehaviour
{
    public void MoveToSelectScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
