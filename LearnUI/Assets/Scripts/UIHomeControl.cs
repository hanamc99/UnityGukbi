using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHomeControl : MonoBehaviour
{
    public Button startBtn;
    [SerializeField] Button fbkBtn;
    [SerializeField] Button itemsBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button messagesBtn;
    [SerializeField] Button missionBtn;
    [SerializeField] Button rankingBtn;
    [SerializeField] Button settingsBtn;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] float hp;
    [Range(0f, 1f)]
    [SerializeField] float exp;

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
    }

    void Update()
    {
        this.hpSlider.value = this.hp / 1000;
        this.expSlider.value = this.exp;
        this.hpText.text = this.hp + "/1000";
    }
}
