using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHomeControl : MonoBehaviour
{
    public Button startBtn;
    [SerializeField] Button fbkBtn;
    public Button itemsBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button messagesBtn;
    [SerializeField] Button missionBtn;
    [SerializeField] Button rankingBtn;
    [SerializeField] Button settingsBtn;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI hpText;

    void Start()
    {
        startBtn.onClick.AddListener(() => Debug.Log("Hello World!"));
        fbkBtn.onClick.AddListener(() => Debug.Log("Hello Meta!"));
        itemsBtn.onClick.AddListener(() => Debug.Log("Hello Items!"));
        shopBtn.onClick.AddListener(() => Debug.Log("Hello Shop!"));
        messagesBtn.onClick.AddListener(() => Debug.Log("Hello Messages!"));
        missionBtn.onClick.AddListener(() => Debug.Log("Hello Missions!"));
        rankingBtn.onClick.AddListener(() => Debug.Log("Hello Ranking!"));
        settingsBtn.onClick.AddListener(() => Debug.Log("Hello Settings!"));
        WriteProfileBar();
    }

    void WriteProfileBar()
    {
        hpSlider.value = DataManager.GetInstance().GetHeroInfo().health / 1000;
        expSlider.value = DataManager.GetInstance().GetHeroInfo().exp;
        hpText.text = DataManager.GetInstance().GetHeroInfo().health + "/1000";
    }
}
