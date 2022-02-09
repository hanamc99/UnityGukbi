using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] UIHomeControl uiHome;
    [SerializeField] UILoginControl uiLogin;

    [SerializeField] UIHeroInfoControl uiHeroInfo;
    [SerializeField] UIInventoryControl uiInventory;

    void Start()
    {
        this.uiHome.startBtn.onClick.AddListener(() => this.uiLogin.gameObject.SetActive(true));
        this.uiLogin.loginBtn.onClick.AddListener(() => SceneManager.LoadScene(1));
        this.uiHome.itemsBtn.onClick.AddListener(() => this.uiHeroInfo.gameObject.SetActive(true));

        DataManager.GetInstance().LoadDatas();
        DataManager.GetInstance().DiscernUserType();
        uiHeroInfo.Init(DataManager.GetInstance().GetHeroInfo());
        uiInventory.Init(DataManager.GetInstance().GetDictItemData());
    }
}
