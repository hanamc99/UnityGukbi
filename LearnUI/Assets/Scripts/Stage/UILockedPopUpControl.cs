using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILockedPopUpControl : MonoBehaviour
{
    public TextMeshProUGUI stageNameText;
    public TextMeshProUGUI minLevelText;

    public void CloseLockedPopUp()
    {
        gameObject.SetActive(false);
    }
}
