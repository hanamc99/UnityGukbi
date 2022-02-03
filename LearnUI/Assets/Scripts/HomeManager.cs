using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] UIHomeControl uiHome;
    [SerializeField] UILoginControl uiLogin;

    void Start()
    {
        this.uiHome.startBtn.onClick.AddListener(() => this.uiLogin.Open());
    }
    
    void Update()
    {
        
    }
}
