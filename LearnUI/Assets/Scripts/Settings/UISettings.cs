using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    bool isOpened = false;
    public Button BtnOpen;
    [SerializeField] UISettingsPopUpControl popUp;

    void Start()
    {
    }

    public void OpenPopUp()
    {
        if (!isOpened)
        {
            popUp.InitSettingsPopUp();
            isOpened = true;
        }
        popUp.gameObject.SetActive(true);
    }
}
