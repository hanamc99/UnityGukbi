using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILockedPopUpControl : MonoBehaviour
{
    public TextMeshProUGUI stageNameText;
    public TextMeshProUGUI minLevelText;
    [SerializeField] TextMeshProUGUI curLevelText;

    public void CloseLockedPopUp()
    {
        gameObject.SetActive(false);
    }

    public void ShowCurLevel()
    {
        curLevelText.text = "Your Lv." + DataManager.GetInstance().GetHeroInfo().level;
    }
}
