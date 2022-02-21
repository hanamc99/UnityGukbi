using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UISettingsPopUpControl : MonoBehaviour
{
    [SerializeField] Button BtnClose;

    [SerializeField] Toggle[] togDiff;
    [SerializeField] Toggle togNotice;
    [SerializeField] GameObject[] goNotice;
    [SerializeField] Toggle togSavePower;
    [SerializeField] GameObject[] goSavePower;
    [SerializeField] Slider sldBgm;
    Coroutine bgmRoutine;
    [SerializeField] Slider sldSfx;
    Coroutine sfxRoutine;
    [SerializeField] Button btnLanguage;
    [SerializeField] Button btnCloseLanguage;
    [SerializeField] Image icnLanguage;
    [SerializeField] Text txtLanguage;
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] GameObject popUpLanguage;
    [SerializeField] Toggle[] togLanguage;
    [SerializeField] GameObject icnCheckLanguage;

    void Start()
    {
        InitTogLanguage();
        btnCloseLanguage.onClick.AddListener(ClosePopUpLanguage);
        btnLanguage.onClick.AddListener(OpenPopUpLanguage);
        togDiff[0].onValueChanged.AddListener(CheckDiffToggle);
        togDiff[1].onValueChanged.AddListener(CheckDiffToggle);
        togDiff[2].onValueChanged.AddListener(CheckDiffToggle);
        BtnClose.onClick.AddListener(ClosePopUp);
        togNotice.onValueChanged.AddListener(OnNoticeChanged);
        togSavePower.onValueChanged.AddListener(OnSavePowerChanged);
        sldBgm.onValueChanged.AddListener(OnBgmChanged);
        sldSfx.onValueChanged.AddListener(OnSfxChanged);
        popUpLanguage.SetActive(false);
    }

    void InitTogLanguage()
    {
        for(int t = 0; t < togLanguage.Length; t++)
        {
            togLanguage[t].onValueChanged.AddListener(CheckLanguageToggle);
        }
    }

    void CheckLanguageToggle(bool active)
    {
        for(int i = 0; i < togLanguage.Length; i++)
        {
            if (togLanguage[i].isOn)
            {
                PlayerPrefs.SetInt("languageId", i);
            }
        }

        icnLanguage.sprite = atlas.GetSprite(DataManager.GetInstance().GetLanguageData(PlayerPrefs.GetInt("languageId")).spriteName);
        txtLanguage.text = DataManager.GetInstance().GetLanguageData(PlayerPrefs.GetInt("languageId")).country;

        icnCheckLanguage.transform.SetParent(togLanguage[PlayerPrefs.GetInt("languageId")].gameObject.transform);
        icnCheckLanguage.transform.localPosition = new Vector3(60, -53, 0);
    }

    void CheckDiffToggle(bool active)
    {
        for(int j = 0; j < 3; j++)
        {
            if (togDiff[j].isOn)
            {
                PlayerPrefs.SetInt("difficulty", j);
            }
        }
    }
    
    void OnSfxChanged(float value)
    {
        if(sfxRoutine != null)
        {
            StopCoroutine(sfxRoutine);
        }
        sfxRoutine = StartCoroutine(ChangeSfxPrefs(value));
    }

    IEnumerator ChangeSfxPrefs(float value)
    {
        yield return new WaitForSeconds(0.3f);
        PlayerPrefs.SetFloat("sfx", value);
    }

    void OnBgmChanged(float value)
    {
        if(bgmRoutine != null)
        {
            StopCoroutine(bgmRoutine);
        }
        bgmRoutine = StartCoroutine(ChangeBgmPrefs(value));
    }

    IEnumerator ChangeBgmPrefs(float value)
    {
        yield return new WaitForSeconds(0.3f);
        PlayerPrefs.SetFloat("bgm", value);
    }

    void OnNoticeChanged(bool active)
    {
        if (active)
        {
            PlayerPrefs.SetInt("notice", 1);
        }
        else
        {
            PlayerPrefs.SetInt("notice", 0);
        }

        foreach (GameObject go in goNotice)
        {
            go.SetActive(false);
        }
        goNotice[PlayerPrefs.GetInt("notice")].SetActive(true);
    }

    void OnSavePowerChanged(bool active)
    {
        if (active)
        {
            PlayerPrefs.SetInt("savePower", 1);
        }
        else
        {
            PlayerPrefs.SetInt("savePower", 0);
        }

        foreach (GameObject go in goSavePower)
        {
            go.SetActive(false);
        }
        goSavePower[PlayerPrefs.GetInt("savePower")].SetActive(true);
    }

    public void InitSettingsPopUp()
    {
        CheckPlayerSettings();
        togDiff[PlayerPrefs.GetInt("difficulty")].isOn = true;
        
        foreach(GameObject go in goNotice)
        {
            go.SetActive(false);
        }
        goNotice[PlayerPrefs.GetInt("notice")].SetActive(true);
        if(PlayerPrefs.GetInt("notice") == 1)
        {
            togNotice.isOn = true;
        }
        else
        {
            togNotice.isOn = false;
        }

        foreach (GameObject go in goSavePower)
        {
            go.SetActive(false);
        }
        goSavePower[PlayerPrefs.GetInt("savePower")].SetActive(true);
        if (PlayerPrefs.GetInt("savePower") == 1)
        {
            togSavePower.isOn = true;
        }
        else
        {
            togSavePower.isOn = false;
        }

        sldBgm.value = PlayerPrefs.GetFloat("bgm");
        sldSfx.value = PlayerPrefs.GetFloat("sfx");

        icnLanguage.sprite = atlas.GetSprite(DataManager.GetInstance().GetLanguageData(PlayerPrefs.GetInt("languageId")).spriteName);
        txtLanguage.text = DataManager.GetInstance().GetLanguageData(PlayerPrefs.GetInt("languageId")).country;

        icnCheckLanguage.transform.SetParent(togLanguage[PlayerPrefs.GetInt("languageId")].gameObject.transform);
        icnCheckLanguage.transform.localPosition = new Vector3(60, -53, 0);
    }

    void CheckPlayerSettings()
    {
        if (!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetInt("difficulty", 0);
        }
        if (!PlayerPrefs.HasKey("notice"))
        {
            PlayerPrefs.SetInt("notice", 1);
        }
        if (!PlayerPrefs.HasKey("savePower"))
        {
            PlayerPrefs.SetInt("savePower", 0);
        }
        if (!PlayerPrefs.HasKey("languageId"))
        {
            PlayerPrefs.SetInt("languageId", 4);
        }
        if (!PlayerPrefs.HasKey("bgm"))
        {
            PlayerPrefs.SetFloat("bgm", 0.75f);
        }
        if (!PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetFloat("sfx", 0.75f);
        }
    }

    void OpenPopUpLanguage()
    {
        popUpLanguage.SetActive(true);
    }

    void ClosePopUpLanguage()
    {
        popUpLanguage.SetActive(false);
    }

    void ClosePopUp()
    {
        this.gameObject.SetActive(false);
    }
}
