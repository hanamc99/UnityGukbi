using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static bool isGo;

    public void Go()
    {
        isGo = true;
    }

    void Start()
    {
        isGo = false;
    }

    void Update()
    {
        
    }
}
