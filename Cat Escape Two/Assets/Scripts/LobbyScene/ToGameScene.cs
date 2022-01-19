using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameScene : MonoBehaviour
{
    public static ToGameScene instance;
    private int characterIndex;


    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetIndex(int i)
    {
        this.characterIndex = i;
    }
    
    public int GetIndex()
    {
        return characterIndex;
    }

    public void MoveToGameScene()
    {
        SceneManager.LoadScene("GameScene1");
    }
}
