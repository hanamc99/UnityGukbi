using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUpInfoControl : MonoBehaviour
{
    public Text stageNameText;
    public Text missionNameText;
    public Image[] availItemImage = new Image[3];
    public Text[] availItemAmountText = new Text[3];

    public void ClosePopUpInfo()
    {
        gameObject.SetActive(false);
    }
}
