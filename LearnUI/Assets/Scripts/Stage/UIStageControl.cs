using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIStageControl : MonoBehaviour
{
    public Button btnHome;
    public TextMeshProUGUI curLevelText;
    [SerializeField] Text starAmountText;
    [SerializeField] Text goldText;
    [SerializeField] Text gemText;
    [SerializeField] Text heartText;
    [SerializeField] Text heartGenTimerText;

    int min = 0;
    float sec = 5f;

    void Start()
    {
        UpdateGold();
        UpdateGem();
        UpdateHeart();
    }

    void Update()
    {
        sec -= Time.deltaTime;
        heartGenTimerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

        if(sec <= 0f)
        {
            if(min <= 0)
            {
                min = 5;
                sec = 0.9f;
                DataManager.GetInstance().IncreaseHeart();
                UpdateHeart();
            }
            else
            {
                sec = 59.9f;
                min--;
            }
        }
    }

    public void UpdateHeart()
    {
        int h = DataManager.GetInstance().GetHeart();
        if(h <= 0)
        {
            heartText.text = "0";
            return;
        }
        heartText.text = h + "";
    }

    public void UpdateGold()
    {
        int g = DataManager.GetInstance().GetGold();
        if(g <= 0)
        {
            goldText.text = "0";
            return;
        }
        goldText.text = string.Format("{0:#,###}", g);
    }

    public void UpdateGem()
    {
        int g = DataManager.GetInstance().GetGem();
        if (g <= 0)
        {
            gemText.text = "0";
            return;
        }
        gemText.text = string.Format("{0:#,###}", g);
    }

    public void UpdateStarAmount()
    {
        int a = 0;
        foreach(StageInfo info in DataManager.GetInstance().GetListStageInfo())
        {
            a += info.star;
        }
        starAmountText.text = a + "";
    }

    public void MoveToLobbyScene()
    {
        SceneManager.LoadScene(0);
    }
}
