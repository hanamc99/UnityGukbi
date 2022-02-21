using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMain : MonoBehaviour
{
    [SerializeField] UISettings settings;

    void Start()
    {
        DataManager.GetInstance().LoadDatas();
        settings.BtnOpen.onClick.AddListener(settings.OpenPopUp);

    }
}
