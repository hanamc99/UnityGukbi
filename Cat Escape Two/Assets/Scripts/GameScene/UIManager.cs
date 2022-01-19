using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image hp;

    public void ChangeHpGauge(float chg)
    {
        hp.fillAmount = chg;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
