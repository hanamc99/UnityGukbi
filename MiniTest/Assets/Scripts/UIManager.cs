using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool isGo;
    [SerializeField] private Image hp;

    public void ChangeHp(float f)
    {
        hp.fillAmount = f;
    }

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
